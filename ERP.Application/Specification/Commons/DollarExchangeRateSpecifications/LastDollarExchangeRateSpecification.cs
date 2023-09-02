using ERP.Domain.Entities.Commons;

namespace ERP.Application.Specification.Commons.DollarExchangeRateSpecifications
{
    public class LastDollarExchangeRateSpecification : BaseSpecification<DollarExchangeRate>
    {
        public LastDollarExchangeRateSpecification()
        {
            AddOrderByDescending(der => der.Id);
        }
    }
}
