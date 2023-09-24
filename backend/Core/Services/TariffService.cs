using System.Text.RegularExpressions;
using AutoMapper;
using Contracts.Tariff;
using Domain;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Services.Abstractions.Admin;
using Services.Exceptions;

namespace Services;

public partial class TariffService : IAdminTariffService
{
    private readonly CarsharingContext _ctx;
    private readonly IMapper _mapper;

    [GeneratedRegex(@"^(\d*\s*\d*)*$")]
    private static partial Regex TariffGeneratedRegex();

    public TariffService(CarsharingContext context, IMapper mapper)
    {
        _mapper = mapper;
        _ctx = context;
    }

    public async Task CreateAsync(CreateTariffDto info)
    {
        ThrowIfInvalid(info);
        try
        {
            await _ctx.Tariffs.AddAsync(new Tariff()
            {
                Name = info.Name!,
                Description = info.Description!,
                Price = info.PriceInRubles,
                IsActive = true,
                MaxMileage = info?.MaxMileage,
            });
            await _ctx.SaveChangesAsync();
        }
        catch(DbUpdateException)
        {
            throw new AlreadyExistsException();
        }
    }

    public async Task EditAsync(int id, CreateTariffDto update)
    {
        ThrowIfInvalid(update);
        var tariff = await _ctx.Tariffs.FindAsync(id);
        if(tariff == null) return;

        var sameTariff = await _ctx.Tariffs.FirstOrDefaultAsync(x => x.Name == update.Name && x.TariffId != id);
        if (sameTariff != null) throw new AlreadyExistsException();
        tariff.Description = update.Description!;
        tariff.Name = update.Name!;
        tariff.Price = update.PriceInRubles;
        tariff.MaxMileage = update.MaxMileage;

        await UpdateTariffAsync(tariff);
    }

    public async Task TurnOnAsync(int id) => await SwitchTariffStateAsync(id, true);

    public async Task TurnOffAsync(int id) => await SwitchTariffStateAsync(id, false);
    
    public async Task DeleteAsync(int id)
    {
        var tariff = await _ctx.Tariffs.FindAsync(id);
        if(tariff == null) return;

        var modelsReferencingToTariff = await _ctx.CarModels.Where(x => x.TariffId == id).ToListAsync();
        if (modelsReferencingToTariff.Any())
            throw new InvalidOperationException($"Can not delete {nameof(Tariff)} since {modelsReferencingToTariff.Count} entities are referencing to it. Delete them first.");
        _ctx.Tariffs.Remove(tariff);
        await _ctx.SaveChangesAsync();
    }

    public async Task<IEnumerable<AdminTariffDto>> GetAllAsync()
    {
        var tariffs = await _ctx.Tariffs.ToListAsync();
        return tariffs.Select(x => new AdminTariffDto
        {
            Id = x.TariffId,
            Description = x.Description,
            Name = x.Name,
            MaxMileage = x!.MaxMileage,
            PriceInRubles = x!.Price,
            IsActive = x.IsActive
        });
    }

    public async Task<AdminTariffDto> GetTariffByIdAsync(int id)
    {
        var tariff = await _ctx.Tariffs.FirstOrDefaultAsync(tariff => tariff.TariffId == id);

        return _mapper.Map<AdminTariffDto>(tariff);
    }

    private static void ThrowIfInvalid(CreateTariffDto tariffDto)
    {
        if (tariffDto.MaxMileage <= 0)
            throw new ArgumentException($"{nameof(tariffDto.MaxMileage)} <= 0");
        if (tariffDto.PriceInRubles <= 0)
            throw new ArgumentException("Invalid price");
        if (string.IsNullOrEmpty(tariffDto.Description)) throw new ArgumentNullException(nameof(tariffDto.Description));
        if (TariffGeneratedRegex().IsMatch(tariffDto.Description))
            throw new ArgumentException($"{nameof(tariffDto.Description)} must contain letters");
    }

    public async Task<IEnumerable<TariffDto>> GetAllActiveAsync()
    {
        var tariffs = await _ctx.Tariffs.Where(x => x.IsActive).ToListAsync();
        return tariffs.Select(MapToTariffDto);
    }

    private static TariffDto MapToTariffDto(Tariff tariff)
    {
        return new TariffDto
        {
            Id = tariff.TariffId,
            Description = tariff.Description,
            Name = tariff.Name,
            MaxMileage = tariff.MaxMileage,
            PriceInRubles = tariff.Price,
            Image = $"/tariffs/{tariff.Name}.png"
        };
    }
    
    public async Task<TariffDto> GetActiveTariffById(int id)
    {
        var tariff = await _ctx.Tariffs.FindAsync(id);
        if (tariff == null || !tariff.IsActive) throw new ObjectNotFoundException(nameof(Tariff));
        return MapToTariffDto(tariff);
    }

    private async Task UpdateTariffAsync(Tariff tariff)
    {
        _ctx.Tariffs.Update(tariff);
        await _ctx.SaveChangesAsync();
    }

    private async Task SwitchTariffStateAsync(int id, bool state)
    {
        var tariff = await _ctx.Tariffs.FindAsync(id);
        if(tariff == null) return;
        tariff.IsActive = state;
        await UpdateTariffAsync(tariff);
    }
}