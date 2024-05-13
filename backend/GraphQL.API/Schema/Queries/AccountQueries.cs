using Domain.Entities;
using GraphQL.API.Helpers.Authorization;
using GraphQL.API.ViewModels.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GraphQL.API.Schema.Queries;

public partial class Queries
{
	[GraphQLName("login")]
	public async Task<string> Login(
		[FromServices] UserManager<User> userManager, 
		[FromServices] SignInManager<User> signInManager, 
		[FromServices] IJwtGenerator jwtGenerator, 
		[FromBody] LoginVM vm)
	{
		var user = await userManager.FindByEmailAsync(vm.Email) ?? throw new GraphQLException("User not found.");

		var resultSignIn = await signInManager.CheckPasswordSignInAsync(user, vm.Password, false);
		
		if (!resultSignIn.Succeeded)
			throw new GraphQLException("Login or password is not correct.");
    
		var claims = await userManager.GetClaimsAsync(user);
		var token = jwtGenerator.CreateToken(user: user, claims: claims);

		return token;
	}
}