using ERP.Domain.Entities.Purchases.QuoteRequests;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace ERP.Infrastructure.EntitiesProfile.Purchases.QuoteRequests
{
    public class QuoteRequestResponseDetailEntityProfile : IEntityTypeConfiguration<QuoteRequestResponseDetail>
    {
        public void Configure(EntityTypeBuilder<QuoteRequestResponseDetail> builder)
        {
            builder.Property(x => x.ConcurrencyToken).IsConcurrencyToken();

        }
    }
}
