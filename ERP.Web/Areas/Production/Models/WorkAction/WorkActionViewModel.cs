using System;

namespace ERP.Web.Areas.Production.Models
{
    public class WorkActionViewModel
    {
        public int Id { get; set; }
        public int ConcurrencyToken { get; set; }
        public int WorkActivityId { get; set; }
        public WorkActivityViewModel WorkActivity { get; set; }

        public TimeSpan ActionTime
        {
            get
            {
                if (this.EndingDate != null)
                {
                    return (TimeSpan)(this.EndingDate - this.StartDate);
                }
                else
                {
                    return new TimeSpan(0, 0, 0, 0);
                }
            }
        }

        public DateTime? StartDate { get; set; }
        public DateTime? EndingDate { get; set; }
        public string ActionName { get; set; }
    }
}
