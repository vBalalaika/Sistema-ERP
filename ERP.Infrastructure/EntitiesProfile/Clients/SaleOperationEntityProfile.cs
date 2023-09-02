using ERP.Domain.Entities.Clients;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ERP.Infrastructure.EntitiesProfile.Clients
{
    public class SaleOperationEntityProfile : IEntityTypeConfiguration<SaleOperation>
    {
        public void Configure(EntityTypeBuilder<SaleOperation> builder)
        {
            builder.Property(x => x.ConcurrencyToken).IsConcurrencyToken();
        }
    }
}
