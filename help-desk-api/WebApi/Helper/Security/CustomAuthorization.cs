using Microsoft.AspNetCore.Mvc.Filters;
using Models.Dto.Menus;
using Utilities.Redis;
using Utils.Exceptions;

namespace WebApi.Helper.Security
{
    public class CustomAuthorization : Attribute
    {
        public void OnAuthorization(ActionExecutingContext context, string userId)
        {
            //Getting full path of the requested end point
            string AbsolutePath = context.ActionDescriptor.AttributeRouteInfo?.Template?.ToString(); 
            //And requested method using HttpContext
            var requestType = context.HttpContext.Request.Method;
            bool IsApproved = false;

            // Write a private methode
            switch (requestType.ToString())
            {
                case "GET":
                    IsApproved = IsPermittedMethod(AbsolutePath, "GET", userId);
                    break;
                case "POST":
                    IsApproved = IsPermittedMethod(AbsolutePath, "POST", userId);
                    break;
                case "PUT":
                    IsApproved = IsPermittedMethod(AbsolutePath, "PUT", userId);
                    break;
                case "DELETE":
                    IsApproved = IsPermittedMethod(AbsolutePath, "DELETE", userId);
                    break;
                case "PATCH":
                    IsApproved = IsPermittedMethod(AbsolutePath, "PATCH", userId);
                    break;
                default:
                    IsApproved = false;
                    break;
            }
            if (IsApproved == false)
                throw new ForbiddenException("Access Denied!");
        }

        private bool IsPermittedMethod(string absolutePath, string methodName, string userId) {
            //Get Pemitted menus for requested user using his userId from Redis cache
            //We normally store users permitted menus in redis when a user logged in
           
            /// todo
            List<UserAccessDto> GetMenuActions = AuthCacheUtil.GetPermittedMenu($"{userId}");
            
            //Checking if user has this particular endpoint in his permitted menu list
            return GetMenuActions != null && GetMenuActions.Where(i => i.HttpVerb == methodName && i.ApiUrl == absolutePath).Any();
        }

    }


}
