using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebStore.Infrastructure.Middleware
{
    public class TestMiddleware
    {
        private readonly RequestDelegate _Next;

        public TestMiddleware(RequestDelegate next) => _Next = next;

        public async Task Invoke(HttpContext context)
        {
            //do befor next
            await _Next(context);
            //do after next
        }
    }
}
