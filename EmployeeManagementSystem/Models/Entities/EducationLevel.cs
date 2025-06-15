namespace EmployeeManagementSystem.Models.Entities
{
    public class EducationLevel : BaseEntity
    {
        public string Title { get; set; }

        public List<Employee> Employees { get; set; }
    }
}
