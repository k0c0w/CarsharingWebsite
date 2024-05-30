using Analytics.Microservice.GrpcServices;

namespace Analytics.Microservice.Extensions;

public static class WebApplicationExtensions
{
    public static void ConfigureApp(this WebApplication app)
    {
        app.UseAuthentication();
        app.UseAuthorization();

        app.MapGrpcService<StatisticsServiceImpl>();
    }
}
