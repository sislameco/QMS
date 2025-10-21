using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Dto.Pagination
{
    public class PaginationResponse<T>: PageBase
    {
        public List<T> Items { get; set; } = new();
        public int Total { get; set; }
    }
    public class PageBase
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
    }
}
