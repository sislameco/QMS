using Models.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Dto.Org
{
    public class CustomFieldInputDto
    {
        public int Id { get; set; }
        public int FkTicketTypeId { get; set; }
        public int Type { get; set; }
        public string DisplayName { get; set; }
        public EnumDataType DataType { get; set; }
        public string[] DDLValue { get; set; }
        public bool IsRequired { get; set; }
        public string Description { get; set; }
        public bool IsMultiSelect { get; set; }
    }
    public class CustomFieldOutPutDto : CustomFieldInputDto
    {
        public string TicketType { get; set; }
    }
    public class SubFromConfigByTicketTypeDto
    {
        public int Id { get; set; }
        public string DisplayName { get; set; }
        public DataType DataType { get; set; }
        public bool IsRequired { get; set; }
        public string[] DDLValue { get; set; }
        public bool IsMultiSelect { get; set; }
    }
    public class SubFromInputDto
    {
        public int Id { get; set; }
        public string Value { get; set; }
    }
    public class FieldOutputDto
    {
        public int Id { get; set; }
        public int FkTicketTypeId { get; set; }
        public string DisplayName { get; set; }
        public EnumDataType DataType { get; set; }
        public string[] DDLValue { get; set; }
        public bool IsRequired { get; set; }
        public string Description { get; set; }
        public bool IsMultiSelect { get; set; }
    }
}
