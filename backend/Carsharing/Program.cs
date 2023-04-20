using Carsharing;
using Entities;
using Entities.Model;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<CarsharingContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"),
        x => x.MigrationsAssembly("Entities"));
});

builder.Services.AddIdentity<User, UserRole>(options =>
{
   
})
    .AddEntityFrameworkStores<CarsharingContext>();

builder.Services.ConfigureApplicationCookie(options =>
{

})
    .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
    {

    });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("manager", options => {
        options.RequireAuthenticatedUser()
            .AddAuthenticationSchemes(CookieAuthenticationDefaults.AuthenticationScheme)
            .RequireClaim(claimType: ClaimsIdentity.DefaultRoleClaimType, "manager");
        //.RequireRole(new string[] {"manager", "admin"});
    });
});

builder.Services.AddCors(options =>
{
    var configuration = builder.Services.BuildServiceProvider().GetRequiredService<IConfiguration>();
    var frontendURL = configuration.GetValue<string>("FrontendHost");
    options.AddDefaultPolicy(builder => builder.WithOrigins(frontendURL).AllowAnyMethod().AllowAnyHeader());
});


builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
