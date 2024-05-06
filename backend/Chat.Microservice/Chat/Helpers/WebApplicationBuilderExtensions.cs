namespace Chat.Helpers;

public static class WebApplicationBuilderExtensions
{
    public static void Configure(this WebApplicationBuilder builder)
    {
        var services = builder.Services;
        var configuration = builder.Configuration;

        services
            .AddAuthorization(configuration)
            .AddGrpc();
    }
}
