using ERP.Domain.Entities.Commons;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ERP.Infrastructure.EntitiesProfile.Commons
{
    public class DollarExchangeRateEntityProfile : IEntityTypeConfiguration<DollarExchangeRate>
    {
        public void Configure(EntityTypeBuilder<DollarExchangeRate> builder)
        {

        }
    }
}
