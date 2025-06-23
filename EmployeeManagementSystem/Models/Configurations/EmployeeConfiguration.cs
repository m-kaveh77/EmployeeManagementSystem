using EmployeeManagementSystem.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EmployeeManagementSystem.Models.Configurations
{
    public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.HasOne(e => e.EducationLevel)
                .WithMany(e => e.Employees)
                .HasForeignKey(e => e.EducationLevelId);

            builder.HasOne(e => e.Position)
                .WithMany(e => e.Employees)
                .HasForeignKey(e => e.PositionId);

            builder.Property(e => e.NationalCode).HasMaxLength(10).IsUnicode(false);
            builder.Property(e => e.Name).HasMaxLength(100);
            builder.Property(e => e.Family).HasMaxLength(100);
            builder.Property(e => e.Mobile).HasMaxLength(11).IsUnicode(false);
        }
    }
}
