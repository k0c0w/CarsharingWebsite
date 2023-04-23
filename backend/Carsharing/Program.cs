using Carsharing.Services;
using Entities;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<CarsharingContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"),
        x => x.MigrationsAssembly("Entities"));
});

builder.Services.AddCors(options =>
{
    var configuration = builder.Services.BuildServiceProvider().GetRequiredService<IConfiguration>();
    var frontendURL = configuration.GetValue<string>("FrontendHost");
    options.AddDefaultPolicy(builder => builder.WithOrigins(frontendURL).AllowAnyMethod().AllowAnyHeader());
});


builder.Services.AddControllers();

builder.Services.AddScoped<IAsyncFileProvider, FileProvider>();

var app = builder.Build();
app.UseCors();

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseStaticFiles();

app.UseRouting();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "Api",
        pattern: "{area:exists}/{controller}/{action=Index}");
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
});

app.Run();
