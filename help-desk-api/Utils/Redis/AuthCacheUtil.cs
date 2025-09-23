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
        public static void SetPermittedMenu(string key, List<PermittedActionsOutputDto> permittedMenus)
        {
            // Removing the cached menu if there have any from prevous session.
            RemovePermittedMenu(key);
            // Short delay before setting new cache (if necessary)
            Task.Delay(50);
            _cache.SetString<List<PermittedActionsOutputDto>>(key, permittedMenus, 24);
        }
        public static void RemovePermittedMenu(string key)
        {
            _cache.Remove(key);
        }
        public static List<UserAccessDto> GetPermittedMenu(string key)
        {
            return _cache.GetString<List<UserAccessDto>>(key);
        }
        public static UserMenus GetPermittedUserResources(int userId)
        {
            string key = $"{_menuRouteKey}-{userId}";
            return _cache.GetString<UserMenus>(key);
        }
        // Caching Permitted Menus, User Info and Permitted Power BI reports etc.
        public static void SetPermittedUserResources(int userId,  UserMenus resources)
        {
            string key = $"{_menuRouteKey}-{userId}";
            // Remove old cached data
            RemovePermittedMenu(key);
            // Short delay before setting new cache (if necessary)
            Task.Delay(50);
            // Save new data
            _cache.SetString<UserMenus>(key, resources, 24);
        }
    }
}
