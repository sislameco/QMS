using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Dto.GlobalDto
{
    public class UserDropdownDto
    {
        public int Id { get; set; }
        public string FullName { get; set; }
    }
    public class DropdownOutputDto<V, T>
    {

        public V Id { get; set; }
        public T Name { get; set; }
        public bool IsDefault { get; set; } = false;

        public DropdownOutputDto()
        {
        }

        public DropdownOutputDto(V id, T name, bool isDefault)
        {
            this.Id = id;
            this.Name = name;
            this.IsDefault = isDefault;
        }


    }
}
