using ERP.Domain.Entities.Commons;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ERP.Infrastructure.EntitiesProfile.Commons
{
    public class IVAConditionEntityProfile : IEntityTypeConfiguration<IVACondition>
    {
        public void Configure(EntityTypeBuilder<IVACondition> builder)
        {
            builder.Property(x => x.Description).HasMaxLength(ConstantConfiguration.Description);
        }
    }
}
