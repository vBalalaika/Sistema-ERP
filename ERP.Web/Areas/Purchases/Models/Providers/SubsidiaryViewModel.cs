using ERP.Domain.Entities.Commons;
using ERP.Domain.Entities.Lists;
using ERP.Domain.Entities.Purchases.Providers;
using System.Collections.Generic;
using ERP.Web.Areas.Commons.Models;
using ERP.Web.Areas.Lists.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ERP.Web.Areas.Purchases.Models.Providers
{
    public class SubsidiaryViewModel
    {
        public int Id { get; set; }
        public int ConcurrencyToken { get; set; }

        public string Address { get; set; }

        public int CountryId { get; set; }
        public IList<CountryViewModel> Countries { get; set; }
        public SelectList CountriesList { get; set; }
        public Country Country { get; set; }
        public int? StateId { get; set; }
        public IList<StateViewModel> States { get; set; }
        public SelectList StatesList { get; set; }
        public State State { get; set; }
        public int? CityId { get; set; }
        public IList<CityViewModel> Cities { get; set; }
        public SelectList CitiesList { get; set; }
        public City City { get; set; }
        public string SubsidiaryNumber { get; set; }

        public string PostalCode { get; set; }

        public int ProviderId { get; set; }
        public Provider Provider { get; set; }

        public SubsidiaryViewModel()
        {
            Countries = new List<CountryViewModel>();
            States = new List<StateViewModel>();
            Cities = new List<CityViewModel>();
        }
    }
}
