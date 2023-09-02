using System.Collections.Generic;

namespace ERP.Web.Models
{
    public class ResultListSelect2<T>
    {
        public List<T> items { get; set; }
        public int total_count { get; set; }
    }
}
