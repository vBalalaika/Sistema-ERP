using ERP.Domain.Entities.Purchases.QuoteRequests;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ERP.Infrastructure.EntitiesProfile.Purchases.QuoteRequests
{
    public class QuoteRequestDetailEntityProfile : IEntityTypeConfiguration<QuoteRequestDetail>
    {
        public void Configure(EntityTypeBuilder<QuoteRequestDetail> builder)
        {
            builder.Property(x => x.ConcurrencyToken).IsConcurrencyToken();

        }
    }
}
