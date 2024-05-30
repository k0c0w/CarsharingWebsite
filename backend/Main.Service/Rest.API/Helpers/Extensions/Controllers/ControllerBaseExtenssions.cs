using Microsoft.AspNetCore.Mvc;

namespace Carsharing.Helpers.Extensions.Controllers
{
    public static class ControllerBaseExtensions
    {
        public static object GenerateServiceError(this ControllerBase controller, string? message) => 
            new { error = new { code = (int)ErrorCode.ServiceError, messages = new[] { message } } };

        public static IActionResult BadRequestWithErrorMessage(this ControllerBase controller, string? message)
        {
            return controller.BadRequest(controller.GenerateServiceError(message));
        }
    }
}
