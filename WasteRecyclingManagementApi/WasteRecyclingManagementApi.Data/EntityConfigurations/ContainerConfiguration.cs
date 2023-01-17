using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WasteRecyclingManagementApi.Core.Configuration;
using WasteRecyclingManagementApi.Core.Entities;

namespace WasteRecyclingManagementApi.Data.EntityConfigurations
{
    public class ContainerConfiguration : IEntityTypeConfiguration<Container>
    {
        public void Configure(EntityTypeBuilder<Container> builder)
        {
            // primary key
            builder.HasKey(x => x.Id);

            // unique type per recycling point
            builder.HasIndex(c => new { c.RecyclingPointId, c.Type }).IsUnique();

            // not null
            builder.Property(c => c.MeasureUnit).IsRequired();
            builder.Property(c => c.Type).IsRequired();
            builder.Property(c => c.Occupied).IsRequired();
            builder.Property(c => c.TotalCapacity).IsRequired();
            builder.Property(c => c.RecyclingPointId).IsRequired();

            // set check constraint for container waste capacity
            builder.HasCheckConstraint("CK_Occupied",
                "[Occupied] <= [TotalCapacity]", 
                c => c.HasName("CK_Container_Occupied"));

            // max length
            builder.Property(c => c.MeasureUnit).HasMaxLength(EntityHelperConstants.MEASURE_UNIT_MAX_LENGTH);
            builder.Property(c => c.Type).HasMaxLength(EntityHelperConstants.WASTE_TYPE_MAX_LENGTH);

            // set precision for decimal values
            builder.Property(c => c.TotalCapacity).HasPrecision(10, 2);
            builder.Property(c => c.Occupied).HasPrecision(10, 2);
        }
    }
}
