using ERP.Domain.Entities.Purchases.QuoteRequests;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ERP.Infrastructure.EntitiesProfile.Purchases.QuoteRequests
{
    public class RelQuoteRequestProviderEntityProfile : IEntityTypeConfiguration<RelQuoteRequestProvider>
    {
        public void Configure(EntityTypeBuilder<RelQuoteRequestProvider> builder)
        {
            builder.Property(x => x.ConcurrencyToken).IsConcurrencyToken();
        }
    }
}
