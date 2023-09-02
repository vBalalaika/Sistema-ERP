using ERP.Domain.Entities.ProductMod;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ERP.Infrastructure.EntitiesProfile.ProductMod
{
    public class CompanyEntityProfile : IEntityTypeConfiguration<Company>
    {
        public void Configure(EntityTypeBuilder<Company> builder)
        {
            builder.Property(x => x.ConcurrencyToken).IsConcurrencyToken();
            builder.Property(x => x.BusinessName).HasMaxLength(ConstantConfiguration.Description);
            builder.Property(x => x.City).HasMaxLength(ConstantConfiguration.Description);
            builder.Property(x => x.Address).HasMaxLength(ConstantConfiguration.Description);
            builder.Property(x => x.PostalCode).HasMaxLength(ConstantConfiguration.Code);
            builder.Property(x => x.PhoneNumber).HasMaxLength(ConstantConfiguration.Code);
            builder.Property(x => x.Email).HasMaxLength(ConstantConfiguration.Code);
            builder.Property(x => x.Web).HasMaxLength(ConstantConfiguration.Code);
            builder.Property(x => x.Facebook).HasMaxLength(ConstantConfiguration.Code);
            builder.Property(x => x.Skype).HasMaxLength(ConstantConfiguration.Code);
            builder.Property(x => x.Subjet).HasMaxLength(ConstantConfiguration.Description);
            builder.Property(x => x.Body).HasMaxLength(ConstantConfiguration.Description);
            builder.Property(x => x.Eslogan).HasMaxLength(ConstantConfiguration.Description);
        }
    }
}
