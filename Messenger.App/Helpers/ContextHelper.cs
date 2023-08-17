using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Messenger.App.Helpers
{
    public static class ContextHelper
    {
        public static string? GetAuthToken(this HttpContext context)
        {
            return context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
        }
    }
}
