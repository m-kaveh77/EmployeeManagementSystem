namespace EmployeeManagementSystem.Models.Entities
{
    public class Employee : BaseEntity
    {
        public EducationLevel EducationLevel { get; set; }
        public int EducationLevelId { get; set; }
        public Position Position { get; set; }
        public int PositionId { get; set; }
        public string Name { get; set; }
        public string Family { get; set; }
        public string NationalCode { get; set; }
        public string Mobile { get; set; }
        public string StudyField { get; set; }
        public GenderType Gender { get; set; }
        public DateTime EmploymentDate { get; set; }
        public DateTime BirthDate { get; set; }
        public string Avatar { get; set; }
    }

    public enum GenderType
    {
        Male, Female
    }
}
