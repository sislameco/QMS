using Amazon.Runtime.Internal.Util;
using Microsoft.Extensions.Caching.StackExchangeRedis;
using Microsoft.Extensions.Configuration;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Utilities.Redis
{
    public static class AuthCacheUtil
    {
        public static ICacheService _cache;
        private static string _menuRouteKey = "user-menu";
        public static void RedisCacheInitialize(ICacheService cache)
        {
            _cache = cache;
        }
    }
}
