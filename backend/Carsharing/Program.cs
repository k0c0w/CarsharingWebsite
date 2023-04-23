using Carsharing.Authorization;
using Entities;
using Entities.Model;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Services.BuildServiceProvider().GetRequiredService<IConfiguration>();

// DbContext
builder.Services.AddDbContext<CarsharingContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"),
        x => x.MigrationsAssembly("Entities"));
});

builder.Services.AddIdentity<User, UserRole>(options =>
{

})
    .AddEntityFrameworkStores<CarsharingContext>()
    .AddDefaultTokenProviders();

// Auth
builder.Services.ConfigureApplicationCookie(config =>
{
})
 .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
 .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
 {
     options.Cookie.Name = CookieAuthenticationDefaults.AuthenticationScheme;
     options.Cookie.SameSite = SameSiteMode.Lax;
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
 })
 .AddFacebook(options =>
 {
     options.AppId = configuration["Authorization:Facebook:AppId"];
     options.AppSecret = configuration["Authorization:Facebook:AppSecret"];
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


builder.Services.AddSingleton<IAuthorizationHandler, CanBuyRequirementsHandler>();


builder.Services.AddCors(options =>
{
    var frontendURL = configuration.GetValue<string>("FrontendHost");

    //options.AddDefaultPolicy(builder => builder.WithOrigins(frontendURL).AllowAnyMethod().AllowAnyHeader());

    //options.AddPolicy("CORSAllowLocalHost3000",
    //    builder =>
    //        builder.WithOrigins(frontendURL)
    //            .AllowAnyHeader()
    //            .AllowAnyMethod()
    //            .AllowCredentials() // 'Access-Control-Allow-Credentials' : true
    //    );
    options.AddPolicy("CORSAllowLocalHost3000",
       builder =>
           builder.WithOrigins(frontendURL)
                .AllowAnyHeader()
                .AllowCredentials()
                .AllowAnyMethod()
                .SetIsOriginAllowed(hostName => true)
       );
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

app.UseCors("CORSAllowLocalHost3000");
//app.UseCors(options => options
//    .AllowAnyHeader()
//    .AllowCredentials()
//    .AllowAnyMethod()
//    .SetIsOriginAllowed(hostName => true));

//app.UseCookiePolicy(new CookiePolicyOptions
//{
//    HttpOnly = HttpOnlyPolicy.Always,
//    MinimumSameSitePolicy = SameSiteMode.None,
//    Secure = CookieSecurePolicy.Always
//});

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
