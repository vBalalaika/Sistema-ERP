using ERP.Domain.Entities.Production;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ERP.Infrastructure.EntitiesProfile.Production
{
    public class WorkOrderHeaderEntityProfile : IEntityTypeConfiguration<WorkOrderHeader>
    {
        public void Configure(EntityTypeBuilder<WorkOrderHeader> builder)
        {
            builder.Property(x => x.ConcurrencyToken).IsConcurrencyToken();
        }
    }
}
