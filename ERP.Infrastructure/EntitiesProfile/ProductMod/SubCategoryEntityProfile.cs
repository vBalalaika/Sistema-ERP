using ERP.Domain.Entities.ProductMod;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ERP.Infrastructure.EntitiesProfile.ProductMod
{
    public class SubCategoryEntityProfile : IEntityTypeConfiguration<SubCategory>
    {
        public void Configure(EntityTypeBuilder<SubCategory> builder)
        {
            builder.Property(x => x.ConcurrencyToken).IsConcurrencyToken();
            builder.Property(x => x.Description).HasMaxLength(ConstantConfiguration.Description);
            builder.Property(x => x.Prefix).HasMaxLength(ConstantConfiguration.Code);
        }
    }
}
