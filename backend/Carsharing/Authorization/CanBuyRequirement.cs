using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace Carsharing.Authorization
{
    public class CanBuyRequirement : IAuthorizationRequirement
    {
        public int AgeThreshold { get; set; }
        public CanBuyRequirement(int ageThreshold) {
            AgeThreshold = ageThreshold;
        }
    }
    public class ApplicationRequirementsHandler : AuthorizationHandler<CanBuyRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, CanBuyRequirement requirement)
        {
            //var authorizationFilterContext = context.Resource as AuthorizationFilterContext;

            var passport = context.User.Claims.FirstOrDefault(claim => claim.Type == "Passport")?.Value;
            if (passport is null || passport == "")
            {
                //authorizationFilterContext.Result = new ForbidResult("Паспорт неверен или не указан."); 
                return Task.CompletedTask;
            }

            var dateOdBirthString = context.User.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.DateOfBirth)?.Value;
            if (String.IsNullOrEmpty(dateOdBirthString))
            {
                //authorizationFilterContext.Result = new ForbidResult("Вы не указали дату рождения.");
                return Task.CompletedTask;
            }

            if (!DateTime.TryParse(dateOdBirthString, out DateTime dateOfBirth))
            { 
                //authorizationFilterContext.Result = new ForbidResult("Дата Вашего рождения не указана");
                return Task.CompletedTask;
            }

            var period = DateTime.Today.Year - dateOfBirth.Year;
            if (dateOfBirth > DateTime.Today.AddYears(-period))
            {
                period--;
            }

            if (period >= requirement.AgeThreshold)
            {
                context.Succeed(requirement);
                return Task.CompletedTask;
            }

            //authorizationFilterContext.Result = new ForbidResult("Вы не достигли 18 лет");
            return Task.CompletedTask;
        }
    }
}
