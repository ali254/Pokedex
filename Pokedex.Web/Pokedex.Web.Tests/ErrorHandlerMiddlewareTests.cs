using Microsoft.AspNetCore.Http;
using Moq;
using Pokedex.Core.Exceptions;
using Pokedex.Web.Middleware;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Pokedex.Web.Tests
{
    public class ErrorHandlerMiddlewareTests
    {
        [Fact]
        public async Task Invoke_ApiExceptionThrown_ReturnAppropriateResponseCode()
        {
            var middleware = new ErrorHandlerMiddleware((httpContext) => throw new ApiException("error") { StatusCode = 400 });
            var _context = new DefaultHttpContext();

            await middleware.Invoke(_context);

            Assert.Equal(400, _context.Response.StatusCode);
        }

        [Fact]
        public async Task Invoke_ServerExceptionThrown_ReturnAppropriateResponseCode()
        {
            var middleware = new ErrorHandlerMiddleware((httpContext) => throw new Exception("error"));
            var _context = new DefaultHttpContext();

            await middleware.Invoke(_context);

            Assert.Equal(500, _context.Response.StatusCode);
        }
    }
}
