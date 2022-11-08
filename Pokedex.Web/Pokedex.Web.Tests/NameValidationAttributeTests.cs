
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Routing;
using Moq;
using Pokedex.Web.Validation;
using System.Collections.Generic;
using Xunit;

namespace Pokedex.Web.Tests
{
    public class NameValidationAttributeTests
    {
        [Theory]
        [InlineData("123")]
        [InlineData("x123")]
        [InlineData("!£$")]
        public void InvalidNameInRoute_Should_Return_BadRequestObjectResult(string nameValue)
        {
            var validationFilter = new NameValidationAttribute();
            var modelState = new ModelStateDictionary();
            var routeValues = new RouteValueDictionary();
            routeValues.Add("name", nameValue);
            var routeData = new RouteData(routeValues);

            var actionContext = new ActionContext(
                Mock.Of<HttpContext>(),
                routeData,
                Mock.Of<ActionDescriptor>(),
                modelState
            );

            var actionExecutingContext = new ActionExecutingContext(
                actionContext,
                new List<IFilterMetadata>(),
                new Dictionary<string, object>(),
                Mock.Of<Controller>()
            );

            validationFilter.OnActionExecuting(actionExecutingContext);

            Assert.IsType<BadRequestObjectResult>(actionExecutingContext.Result);
        }
    }
}
