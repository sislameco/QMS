using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utils.LoginData
{
    public interface IUserInfos
    {
        int GetCurrentUserId();
        void SetUserId(int userId);
    }
    public class UserInfos : IUserInfos
    {
        private readonly IHttpContextAccessor _accessor;
        private static int _userId = 0;
        public UserInfos(IHttpContextAccessor accessor)
        {
            _accessor = accessor;

        }
        public void SetUserId(int userId)
        {
            _userId = userId;
        }
        public int GetCurrentUserId()
        {
            if (GetUserIdFromCalim() is { } userId)
            {
                return userId;
            }
            return 0;
        }
        private int GetUserIdFromCalim()
        {
            var userIdClaim = _accessor.HttpContext?.User.Claims.FirstOrDefault(c => c.Type.ToLower() == "userid")?.Value;
            if (int.TryParse(userIdClaim, out int userId))
                return userId;
            return 0;
        }
    }
}
