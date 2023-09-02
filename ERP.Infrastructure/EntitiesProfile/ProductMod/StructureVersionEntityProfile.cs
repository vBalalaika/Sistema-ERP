using ERP.Domain.Entities.ProductMod;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace ERP.Infrastructure.EntitiesProfile.ProductMod
{
    class StructureVersionEntityProfile : IEntityTypeConfiguration<StructureVersion>
    {
        public void Configure(EntityTypeBuilder<StructureVersion> builder)
        {
            builder.Property(x => x.ConcurrencyToken).IsConcurrencyToken();
            builder.Property(x => x.VersionNumber).HasMaxLength(ConstantConfiguration.Code);
            builder.Property(x => x.Comment).HasMaxLength(ConstantConfiguration.Description);

        }
    }
}
