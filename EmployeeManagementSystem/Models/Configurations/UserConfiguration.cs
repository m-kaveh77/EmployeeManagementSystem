using EmployeeManagementSystem.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EmployeeManagementSystem.Models.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasIndex(u => u.Username).IsUnique();
            builder.Property(e => e.Username).HasMaxLength(100);
            builder.Property(e => e.Password).HasMaxLength(100).IsUnicode(false);
            builder.Property(e => e.PasswordSalt).HasMaxLength(100).IsUnicode(false);
            builder.Property(e => e.Role).HasMaxLength(100).IsUnicode(false);
        }
    }
}
