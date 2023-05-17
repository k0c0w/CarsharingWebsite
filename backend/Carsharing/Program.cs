using System.Text.Json;
using Carsharing;
using Carsharing.Authorization;
using Carsharing.Helpers;
using Carsharing.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Domain;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Services;
using Services.Abstractions;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Services.BuildServiceProvider().GetRequiredService<IConfiguration>();

// DbContext
builder.Services.AddDbContext<CarsharingContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"),
        x => x.MigrationsAssembly("Domain"));
});

builder.Services.AddIdentity<User, UserRole>(options =>
{
    options.User.AllowedUserNameCharacters = "user0123456789";
})
    .AddEntityFrameworkStores<CarsharingContext>()
    .AddDefaultTokenProviders();

// Auth
builder.Services
    .AddAuthentication(options =>
 {
     options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
     options.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;
 })
 .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
 {
     options.Cookie.Name = CookieAuthenticationDefaults.AuthenticationScheme;
     options.Cookie.SameSite = SameSiteMode.None;
     options.Cookie.HttpOnly = true;
     options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
     options.Events.OnRedirectToLogin = context =>
     {
         context.Response.StatusCode = StatusCodes.Status401Unauthorized;
         return Task.CompletedTask;
     };

     options.Events.OnRedirectToAccessDenied = context =>
     {
         context.Response.StatusCode = StatusCodes.Status403Forbidden;
         return Task.CompletedTask;
     };
 });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("CanBuy",
        options =>
        {
            options.RequireAuthenticatedUser()
                .AddAuthenticationSchemes(CookieAuthenticationDefaults.AuthenticationScheme)
                .RequireAuthenticatedUser()
                .AddRequirements(new CanBuyRequirement(18));
        });
});


builder.Services.AddSingleton<IAuthorizationHandler, ApplicationRequirementsHandler>();
builder.Services.AddScoped<ICarService, CarService>();
builder.Services.AddScoped<IFileProvider, FileProvider>();
builder.Services.AddScoped<IBookingService, BookingService>();
builder.Services.AddAutoMapper(typeof(Program).Assembly);

builder.Services.AddTariffService();

if (builder.Environment.IsDevelopment())
{
    builder.Services.AddCors(options =>
    {
        var frontendURL = configuration.GetValue<string>("FrontendHost");
    
        options.AddPolicy("CORSAllowLocalHost3000",
            builder =>
                builder.WithOrigins(frontendURL)
                    .AllowAnyHeader()
                    .AllowCredentials()
                    .AllowAnyMethod()
                    .SetIsOriginAllowed(hostName => true)
        );
    });
}

builder.Services.AddControllers();

builder.Services.Configure<ApiBehaviorOptions>(o =>
{
    o.InvalidModelStateResponseFactory = actionContext =>
    {
        var modelState = actionContext.ModelState;
        var json = modelState.Keys
            .ToDictionary(x => x, x => modelState[x].Errors.Select(x => x.ErrorMessage));
        
        return new BadRequestObjectResult(new
            { error = new { code = ErrorCode.ViewModelError, errors = json} });
    };
});


builder.Services.AddSwaggerGen();
builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseCors("CORSAllowLocalHost3000");

app.UseAuthentication();

app.UseAuthorization();

app.UseStaticFiles();

app.UseRouting();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();
