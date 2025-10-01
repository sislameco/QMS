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
        public string DepartmentEndPoint { get; set; }
        public string UserEndPoint { get; set; }
    }
}