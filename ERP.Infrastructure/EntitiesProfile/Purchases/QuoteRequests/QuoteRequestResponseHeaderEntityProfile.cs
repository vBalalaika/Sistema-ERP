using ERP.Domain.Entities.Purchases.QuoteRequests;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace ERP.Infrastructure.EntitiesProfile.Purchases.QuoteRequests
{
    public class QuoteRequestResponseHeaderEntityProfile : IEntityTypeConfiguration<QuoteRequestResponseHeader>
    {
        public void Configure(EntityTypeBuilder<QuoteRequestResponseHeader> builder)
        {
            builder.Property(x => x.ConcurrencyToken).IsConcurrencyToken();

        }
    }
}
