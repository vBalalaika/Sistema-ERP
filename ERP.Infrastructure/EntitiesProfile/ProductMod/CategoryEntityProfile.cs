using ERP.Domain.Entities.ProductMod;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ERP.Infrastructure.EntitiesProfile.ProductMod
{
    public class CategoryEntityProfile : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.Property(x => x.ConcurrencyToken).IsConcurrencyToken();
            builder.Property(x => x.Description).HasMaxLength(ConstantConfiguration.Description);
        }
    }
}