using ERP.Domain.Entities.Lists;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ERP.Infrastructure.EntitiesProfile.Lists
{
    public class CityEntityProfile : IEntityTypeConfiguration<City>
    {
        public void Configure(EntityTypeBuilder<City> builder)
        {
            // Reglas para cada campo en particular
            builder.Property(x => x.ConcurrencyToken).IsConcurrencyToken();
        }
    }
}
