using GraphQL.API.Schema.Queries;
using GraphQL.API.Schema.Mutations;
using ApiExtensions;
using Domain.Common;
using CraphQl.ChatHub;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var currentAssembly = Assembly.GetExecutingAssembly();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddGraphQLServer()
	.AddAuthorization()
	.AddQueryType<Queries>()
	.AddMutationType<Mutations>();

services.AddDatabase(builder.Configuration);

services.AddIdentityAuthorization();

services.AddAutoMapper(currentAssembly);

services
	.AddMassTransitWithRabbitMQProvider(builder.Configuration, currentAssembly)
	.RegisterBusinessLogicServices(builder.Configuration)
	.AddMediatorWithFeatures();

services.AddSingleton<IMessageProducer, FakeMessageProducer>();

services.AddAuthenticationAndAuthorization(builder.Configuration);

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthentication()
	.UseAuthorization();

app.UseRouting();
app.MapGraphQL("/graphql");

app.Run();
