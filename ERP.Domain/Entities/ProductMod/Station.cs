using ERP.Domain.Common;
using ERP.Domain.Entities.Purchases.Providers;
using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities.ProductMod
{
    public class Station : IAuditableBaseEntity, IConcurrencyToken
    {
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public string Abbreviation { get; set; }
        // Default users id string
        public string DefaultUser { get; set; }
        // Assigned users's ids separados por ',' -.-
        public string Users { get; set; }
        public string WorkOrderDisplayType { get; set; }

        public FunctionalArea FunctionalArea { get; set; }
        public int FunctionalAreaId { get; set; }

        public int Id { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime? LastModifiedOn { get; set; }
        public int ConcurrencyToken { get; set; }

        public ICollection<RelProductStation> RelProductStations { get; set; }

        public ICollection<RelProviderStation> RelProviderStations { get; set; } = new List<RelProviderStation>();
    }
}
