using ERP.Domain.Entities.Lists;
using System.Collections.Generic;

namespace ERP.Domain.Entities.Commons
{
    public class Country
    {
        public int Id { get; set; }
        public int Code { get; set; }
        public string Denomination { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public string TimeOffset { get; set; }
        public ICollection<State> States { get; set; }
    }
}