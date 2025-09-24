using Models.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Dto.GlobalDto
{
    public class PagedInputDto
    {
        /// <summary>
        /// Optional search text for filtering (applies to Name, Code, etc.)
        /// </summary>
        public string? SearchText { get; set; }

        /// <summary>
        /// Status filter (e.g., Active/Inactive, Approved/Pending).
        /// </summary>
        public EnumRStatus Status { get; set; } = EnumRStatus.Active;

        /// <summary>
        /// Current page number (1-based).
        /// </summary>
        public int PageNo { get; set; } = 1;

        /// <summary>
        /// Number of items per page.
        /// </summary>
        public int ItemsPerPage { get; set; } = 20;
    }
}
