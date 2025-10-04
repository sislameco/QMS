using Models.Enum;

namespace Models.Dto.Org
{
    public class CompanyDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ShortName { get; set; }
        public string Description { get; set; }
        public string AccessKey { get; set; }
        public string SecretKey { get; set; }
        public string PrefixTicket { get; set; }
        public int LastTicketNumber { get; set; }
        public List<CompanyDefineDataSourceDto> DefineDataSources { get; set; } = new List<CompanyDefineDataSourceDto>();
    }
    public class CompanyDefineDataSourceDto
    {
        public int Id { get; set; }
        public string Source { get; set; }
        public bool IsSync { get; set; }
        public string JSonData { get; set; }
        public EnumDataSource Type { get; set; }
    }




}