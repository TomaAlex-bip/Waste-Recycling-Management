using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WasteRecyclingManagementApi.Core.Configuration;
using WasteRecyclingManagementApi.Core.Entities;

namespace WasteRecyclingManagementApi.Data.EntityConfigurations
{
    public class OperationConfiguration : IEntityTypeConfiguration<Operation>
    {
        public void Configure(EntityTypeBuilder<Operation> builder)
        {
            // primary key
            builder.HasKey(x => x.Id);

            //not null
            builder.Property(x => x.UserId).IsRequired();
            builder.Property(x => x.WasteAmount).IsRequired();
            builder.Property(x => x.ContainerId).IsRequired();
            builder.Property(x => x.Date).IsRequired();
            builder.Property(x => x.Type).IsRequired();

            // default
            builder.Property(o => o.Date).HasDefaultValueSql("getdate()");
            builder.Property(o => o.Status).HasDefaultValue("pending");

            // precision
            builder.Property(o => o.WasteAmount).HasPrecision(10, 2);

            // max length
            builder.Property(o => o.Type).HasMaxLength(20);
            builder.Property(o => o.Status).HasMaxLength(20);

        }
    }
}
