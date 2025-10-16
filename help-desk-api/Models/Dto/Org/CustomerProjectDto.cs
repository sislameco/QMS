namespace Models.Dto.Org
{
    public class CustomerOutputDto
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string FullName { get; set; }
    }
    public class ProjectOutputDto
    {
        public int Id { get; set; }
        public string ReferenceNumber { get; set; }
        public string ProjectAddress { get; set; }
    }
}
