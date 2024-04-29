using GraphQL.API.Schema.Queries;
using GraphQL.API.Helpers.ServiceRegistration;
using GraphQL.API.Schema.Mutations;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddGraphQLServer()
	.AddAuthorization()
	.AddQueryType<Queries>()
	.AddMutationType<Mutations>();

services.AddDatabase(builder.Configuration)
	.AddMassTransitWithRabbitMQProvider(builder.Configuration);

services.AddIdentityAuthorization();

services.AddAutoMapper(typeof(Program).Assembly);

services.RegisterChat()
	.RegisterBuisnessLogicServices(builder.Configuration)
	.AddMediatorWithFeatures();

services.AddAuthenticationAndAuthorization(builder.Configuration);

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthentication()
	.UseAuthorization();

app.UseRouting();

app.MapGet("/", () => "Hello World!");
app.MapGraphQL("/graphql");

app.Run();
