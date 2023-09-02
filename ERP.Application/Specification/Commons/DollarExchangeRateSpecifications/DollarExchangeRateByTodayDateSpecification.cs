using ERP.Domain.Entities.Commons;
using System;

namespace ERP.Application.Specification.Commons.DollarExchangeRateSpecifications
{
    public class DollarExchangeRateByTodayDateSpecification : BaseSpecification<DollarExchangeRate>
    {
        public DollarExchangeRateByTodayDateSpecification(DateTime today) : base(der => (der.Date.Value.Day == today.Day && der.Date.Value.Month == today.Month && der.Date.Value.Year == today.Year))
        {

        }
    }
}
