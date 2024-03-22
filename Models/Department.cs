namespace cTest.Models
{
    public class Department
    {
        public int id { get; set; }
        public string? name { get; set; }
        public int departmentId { get; set; }
        public virtual List<Department>? departments { get; set; }
        public List<Positions>? positions { get; set; }
    }
}
