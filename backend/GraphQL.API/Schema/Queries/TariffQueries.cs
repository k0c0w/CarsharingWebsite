using Contracts.Tariff;
using Features.Tariffs;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GraphQL.API.Schema.Queries;

public partial class Queries
{
	public async Task<IEnumerable<TariffDto>> GetTariff([FromServices] IMediator mediator)
	{
		var tariffsResult = await mediator.Send(new GetActiveTariffsQuery());
		
		if (!tariffsResult)
			throw new GraphQLException(new Error(tariffsResult.ErrorMessage!));

		return tariffsResult.Value!;
	}
	
	public async Task<TariffDto> GetTariffById(int tariffId, [FromServices] IMediator mediator)
	{
		if (tariffId <= 0)
			throw new GraphQLException(new Error("Not found."));
		
		var tariffResult = await mediator.Send(new GetActiveTariffsQuery(tariffId));

		if (!tariffResult)
			throw new GraphQLException(new Error(tariffResult.ErrorMessage!));

		return tariffResult.Value!.FirstOrDefault() ?? throw new GraphQLException(new Error("Not found."));
	}
}