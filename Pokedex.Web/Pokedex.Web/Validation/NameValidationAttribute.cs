using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Pokedex.Core.Constants;
using System.Text.RegularExpressions;

namespace Pokedex.Web.Validation
{
    public class NameValidationAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var name = filterContext.RouteData.Values["name"].ToString();

            if (!Regex.IsMatch(name, RegexExpressions.ValidatePokeApiName))
            {
                filterContext.Result = new BadRequestObjectResult("Bad request - name must be alphabets and hyphens only");
            }

            base.OnActionExecuting(filterContext);
        }
    }
}
