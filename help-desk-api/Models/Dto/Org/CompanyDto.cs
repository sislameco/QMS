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
        List<CompanyDefineDataSourceDto> DefineDataSources { get; set; }
    }
    public class CompanyDefineDataSourceDto
    {
        public int Id { get; set; }
        public string Source { get; set; }
        public bool IsValidate { get; set; }
        public bool IsSync { get; set; }
        public string JsonData { get; set; }
    }
}