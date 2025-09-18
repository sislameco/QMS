using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Dto.Menus
{
    public class UserAccessDto
    {
        public int UserId { get; set; }
        public int MenuId { get; set; }
        public int MapId { get; set; }
        public string MenuName { get; set; }
        public string SystemMenuName { get; set; }
        public string HttpVerb { get; set; }
        public string ApiUrl { get; set; }
    }
}
