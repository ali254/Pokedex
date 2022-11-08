using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Pokedex.Core.Constants;
using Pokedex.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

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
