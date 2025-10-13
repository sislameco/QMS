using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Entities.File
{ 
    [Table("TempFiles", Schema = "file")]
    public class TempFileModel : BaseEntity<int>
    {
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public string Extension { get; set; }
        public DateTime ExpiryDate { get; set; }
    }
}
