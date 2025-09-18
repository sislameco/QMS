using Amazon.Runtime.Internal.Util;
using Microsoft.Extensions.Caching.StackExchangeRedis;
using Microsoft.Extensions.Configuration;
using Models.Dto.Menus;
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
        public static void SetPermittedMenu(string key, List<UserAccessDto> permittedMenus)
        {
            // Removing the cached menu if there have any from prevous session.
            RemovePermittedMenu(key);
            // Short delay before setting new cache (if necessary)
            Task.Delay(50);
            _cache.SetString<List<UserAccessDto>>(key, permittedMenus, 24);
        }
        public static void RemovePermittedMenu(string key)
        {
            _cache.Remove(key);
        }
        public static List<UserAccessDto> GetPermittedMenu(string key)
        {
            return _cache.GetString<List<UserAccessDto>>(key);
        }
    }
}
