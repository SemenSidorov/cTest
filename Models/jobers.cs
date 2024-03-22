namespace cTest.Models
{
    public class Jobers
    {
        public int id { get; set; }
        public string? fio { get; set; }
        public int departmentId { get; set; }
        public string? phone { get; set; }
        public string? photo { get; set; }
        public int positionId { get; set; }
        public virtual Department? department { get; set; }
        public virtual Positions? position { get; set; }
    }
}
