using Features.Users.Commands.CreateUser;
using GraphQL.API.ViewModels.Account;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GraphQL.API.Schema.Mutations;

public partial class Mutations
{

	[GraphQLName("registerUser")]
	public async Task<bool> RegisterUser(
        [FromServices] IMediator mediator,
        RegistrationVm vm)
	{
		var result = await mediator.Send(new CreateUserCommand()
		{
			Email = vm.Email,
			Birthdate = vm.Birthdate,
			LastName = vm.Surname!,
			Name = vm.Name!,
			Password = vm.Password,
		});

		if (!result.IsSuccess)
			throw new GraphQLException(result.ErrorMessage ?? string.Empty);

		return true;
	}
}