using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Carsharing.Helpers.Attributes
{
    public class ModelStateFilter : ActionFilterAttribute
    {
        public ModelStateFilter() { }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var modelState = filterContext.ModelState;

            if (modelState.IsValid)
            {
                var s = modelState.Select(t => new { Field = t.Key, Errors = t.Value?.Errors.Select(e => e.ErrorMessage) });
                filterContext.HttpContext.Response.StatusCode = 400;
                filterContext.HttpContext.Response.ContentType = "application/json";
                filterContext.Result = new BadRequestObjectResult(s);
            }

        }
    }
}
