﻿using Carsharing.Forms;
using Carsharing.Helpers.Authorization;
using Domain.Entities;
using Features.Users.Commands.CreateUser;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GraphQL.API.Schema.Mutations;

public partial class Mutations
{
	
	[GraphQLName("registerUser")]
	public async Task<bool> RegisterUser(
        [FromServices] IMediator mediator,
        [FromBody]RegistrationVm vm)
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
    
	[GraphQLName("login")]
	public async Task<string> Login(
		[FromServices] UserManager<User> userManager, 
		[FromServices] SignInManager<User> signInManager, 
		[FromServices] IJwtGenerator jwtGenerator, 
		[FromBody] LoginVM vm)
	{
		var user = await userManager.FindByEmailAsync(vm.Email);

		if (user == null)
			throw new GraphQLException("User not found.");

		var resultSignIn = await signInManager.CheckPasswordSignInAsync(user, vm.Password, false);
		
		if (!resultSignIn.Succeeded)
			throw new GraphQLException("Login or password is not correct.");
    
		var claims = await userManager.GetClaimsAsync(user);
		var token = jwtGenerator.CreateToken(user: user, claims: claims);

		return token;
	}
}