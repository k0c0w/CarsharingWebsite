using Carsharing;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<CarsharingContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
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

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
