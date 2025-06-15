using EmployeeManagementSystem.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EmployeeManagementSystem.Models.Configurations
{
    public class PositionConfiguration : IEntityTypeConfiguration<Position>
    {
        public void Configure(EntityTypeBuilder<Position> builder)
        {
            builder.HasOne(p => p.ParentPosition)
                .WithMany(p => p.Positions)
                .HasForeignKey(p => p.ParentId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property(p => p.Title).HasMaxLength(200);
        }
    }
}
