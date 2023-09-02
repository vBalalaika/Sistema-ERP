﻿using ERP.Domain.Common;
using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities.ProductMod
{
    public class FunctionalArea : IAuditableBaseEntity, IConcurrencyToken
    {
        public string Description { get; set; }


        public int Id { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime? LastModifiedOn { get; set; }
        public int ConcurrencyToken { get; set; }

        public ICollection<Station> Stations { get; set; }
    }
}