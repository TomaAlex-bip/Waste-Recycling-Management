using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WasteRecyclingManagementApi.Core.Configuration;
using WasteRecyclingManagementApi.Core.Entities;

namespace WasteRecyclingManagementApi.Data.EntityConfigurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            // primary key
            builder.HasKey(u => u.Id);

            // unique username
            builder.HasIndex(u => u.Username).IsUnique();

            // not null
            builder.Property(u => u.Username).IsRequired();
            builder.Property(u => u.Password).IsRequired();
            builder.Property(u => u.Role).IsRequired();

            // max lenght
            builder.Property(u => u.Username).HasMaxLength(EntityHelperConstants.USERNAME_MAX_LENGTH);
            builder.Property(u => u.Password).HasMaxLength(EntityHelperConstants.PASSWORD_MAX_LENGTH);

        }
    }
}
