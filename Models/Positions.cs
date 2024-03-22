namespace cTest.Models
{
    public class Positions
    {
        public int id { get; set; }
        public string? name { get; set; }
        public int salaryMin { get; set; }
        public int salaryMax { get; set; }
        public int countSeats { get; set; }
        public int departmentId { get; set; }
    }
}
