using ERP.Domain.Entities.Production;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ERP.Infrastructure.EntitiesProfile.Production
{
    public class WorkActionEntityProfile : IEntityTypeConfiguration<WorkAction>
    {

        public void Configure(EntityTypeBuilder<WorkAction> builder)
        {
            builder.Property(x => x.ConcurrencyToken).IsConcurrencyToken();
        }
    }
}
