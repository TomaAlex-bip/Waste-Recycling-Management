using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WasteRecyclingManagementApi.Core.Configuration;
using WasteRecyclingManagementApi.Core.Entities;

namespace WasteRecyclingManagementApi.Data.EntityConfigurations
{
    public class RecyclingPointConfiguration : IEntityTypeConfiguration<RecyclingPoint>
    {
        public void Configure(EntityTypeBuilder<RecyclingPoint> builder)
        {
            // primary key
            builder.HasKey(x => x.Id);

            // unique name
            builder.HasIndex(r => r.Name).IsUnique();
            
            // not null
            builder.Property(r => r.Name).IsRequired();
            builder.Property(r => r.Latitude).IsRequired();
            builder.Property(r => r.Longitude).IsRequired();

            // max length
            builder.Property(r => r.Name).HasMaxLength(EntityHelperConstants.RECYCLING_POINT_NAME_MAX_LENGTH);

        }
    }
}
