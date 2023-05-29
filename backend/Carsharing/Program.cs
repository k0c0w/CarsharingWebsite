using Carsharing;
using Carsharing.Authorization;
using Carsharing.Helpers;
using Carsharing.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Domain;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Services;
using Services.Abstractions;
using Services.Abstractions.Admin;
using Services.User;
using Carsharing.ChatHub;
using Carsharing.Hubs.ChatEntities;
using Microsoft.Extensions.FileProviders;
using IFileProvider = Services.Abstractions.IFileProvider;

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
 .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
 .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
 {
     //options.Cookie.Name = CookieAuthenticationDefaults.AuthenticationScheme;
     options.Events.OnRedirectToLogin = context =>
     {
         context.Response.StatusCode = StatusCodes.Status401Unauthorized;
         return Task.CompletedTask;
     };
     options.LoginPath = "/Login";

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
                .AddRequirements(new CanBuyRequirement(23));
        });
});


builder.Services.AddSingleton<IAuthorizationHandler, ApplicationRequirementsHandler>();
builder.Services.AddScoped<IAdminCarService, CarService>();
builder.Services.AddScoped<ICarService, CarService>();
builder.Services.AddScoped<IFileProvider, FileProvider>();
builder.Services.AddScoped<IBookingService, BookingService>();
builder.Services.AddScoped<IAdminPostService, PostService>();
builder.Services.AddScoped<IPostService, PostService>();
builder.Services.AddScoped<IAdminTariffService, TariffService>();
builder.Services.AddScoped<ITariffService, TariffService>();
builder.Services.AddScoped<IBalanceService, BalanceService>();

builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddAutoMapper(typeof(Program).Assembly);

builder.Services.AddSignalR();

builder.Services.AddTariffService();

builder.Services.AddSingleton<IDictionary<string, UserConnection>>(opts => new Dictionary<string, UserConnection>());


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

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseCors("CORSAllowLocalHost3000");
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();


app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
        Path.Combine(builder.Environment.ContentRootPath, "AdminPanelResources")),
    RequestPath = "/admin",
});


app.MapControllers();
app.MapHub<ChatHub>("/chat");
app.MapFallbackToFile("index.html");

app.Run();
