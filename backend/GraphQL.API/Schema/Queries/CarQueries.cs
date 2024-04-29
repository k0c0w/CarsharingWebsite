using Carsharing.ViewModels;
using Entities.Repository;
using Features.CarManagement;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GraphQL.API.Schema.Queries;

public partial class Queries
{
	public async Task<IEnumerable<CarVM>> GetFreeCars(
		FindCarsVM carSearch, 
		[FromServices] IMediator mediator,
		[FromServices] ICarRepository carRepository) 
	{
		var carsResult = await mediator.Send(new GetAvailableCarsByLocationQuery()
		{
			Latitude = carSearch.Latitude,
			Longitude = carSearch.Longitude,
			Radius = carSearch.Radius,
			CarModelId = carSearch.CarModelId
		});

		if (!carsResult.IsSuccess)
			throw new GraphQLException(new Error(carsResult.ErrorMessage ?? string.Empty));

		var result = new List<CarVM>();
		
		foreach (var elem in carsResult.Value!)
		{
			var car = await carRepository.GetByIdAsync(elem.CarId);
			
			result.Add(new CarVM
			{
				Id = elem.CarId,
				ParkingLatitude = elem.Location!.Latitude,
				ParkingLongitude = elem.Location!.Longitude,
				LicensePlate = elem.Plate,
				Description = car?.CarModel?.Description,
				ImageUrl = car?.CarModel?.ImageUrl
			});
		}

		return result;
	}
	
	public async Task<ExpandedCarModelVM> GetCarModelByTariff([FromRoute] int id, [FromServices] IMediator mediator)
	{
		var result = await mediator.Send(new GetModelByIdQuery(id));

		if (!result.IsSuccess)
			throw new GraphQLException(new Error(result.ErrorMessage ?? string.Empty));

		var model = result.Value;
        
		return new ExpandedCarModelVM
		{
			Brand = model!.Brand,
			Description = model.Description,
			Model = model.Model,
			Url = model.ImageUrl,
			TariffId = model.TariffId,
			Id = model.Id,
			Price = model.Price,
			MaxMilage = model.Restrictions,
			TariffName = model.TariffName
		};
	}
	
	public async Task<IEnumerable<CarModelVM>> GetCarModelsByTariff(
		[FromRoute] int id, 
		[FromServices] IMediator mediator)
	{
		var models = ( await mediator.Send(new GetModelsByTariffIdQuery(id)) ).Value;

		if (models == null || !models.Any())
			throw new GraphQLException(new Error("No cars found."));
			
		return models.Select(x => new CarModelVM
		{
			Id = x.Id,
			Brand = x.Brand,
			Description = x.Description,
			Model = x.Model,
			Url = x.ImageUrl,
			TariffId = id
		});
	}
}