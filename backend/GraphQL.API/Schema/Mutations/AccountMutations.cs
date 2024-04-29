using Carsharing.Forms;
using Carsharing.Helpers;
using Carsharing.Helpers.Authorization;
using Carsharing.ViewModels;
using Domain.Entities;
using Features.Users.Commands.CreateUser;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Migrations.CarsharingApp;

namespace GraphQL.API.Schema.Mutations;

public class AccountMutations
{
	private readonly IMediator _mediator;
	private readonly UserManager<User> _userManager;
	private readonly SignInManager<User> _signInManager;
	private readonly CarsharingContext _carsharingContext;
	private readonly IJwtGenerator _jwtGenerator;

	public AccountMutations(IMediator mediator, UserManager<User> userManager, SignInManager<User> signInManager, CarsharingContext carsharingContext, IJwtGenerator jwtGenerator)
	{
		_mediator = mediator;
		_userManager = userManager;
		_signInManager = signInManager;
		_carsharingContext = carsharingContext;
		_jwtGenerator = jwtGenerator;
	}
	
	[GraphQLName("registerUser")]
	public async Task<bool> RegisterUser(RegistrationVm vm)
	{
		var result = await _mediator.Send(new CreateUserCommand()
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
    
	[GraphQLName("login")]
	public async Task<string> Login([FromBody] LoginVM vm)
	{
		var user = await _userManager.FindByEmailAsync(vm.Email);

		if (user == null)
			throw new GraphQLException("User not found.");

		var resultSignIn = await _signInManager.CheckPasswordSignInAsync(user, vm.Password, false);
		
		if (!resultSignIn.Succeeded)
			throw new GraphQLException("Login or password is not correct.");

		var claims = await _userManager.GetClaimsAsync(user);
		var token = _jwtGenerator.CreateToken(user: user, claims: claims);

		return token;
	}
	
	public async Task LogOut()
	{
		await _signInManager.SignOutAsync();
	}
}