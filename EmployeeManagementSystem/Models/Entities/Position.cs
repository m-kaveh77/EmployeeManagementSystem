namespace EmployeeManagementSystem.Models.Entities
{
    public class Position : BaseEntity
    {
        public Position ParentPosition { get; set; }
        public int? ParentId { get; set; }
        public string Title { get; set; }

        public List<Employee> Employees { get; set; }
        public List<Position> Positions { get; set; }
    }
}
