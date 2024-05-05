using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace GraphQL.API.Helpers.Attributes
{
    public class ModelStateFilterAttribute : ActionFilterAttribute
    {
        public ModelStateFilterAttribute() { }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var modelState = context.ModelState;

            if (modelState.IsValid)
            {
                var s = modelState.Select(t => new { Field = t.Key, Errors = t.Value?.Errors.Select(e => e.ErrorMessage) });
                context.HttpContext.Response.StatusCode = 400;
                context.HttpContext.Response.ContentType = "application/json";
                context.Result = new BadRequestObjectResult(s);
            }

        }
    }
}
