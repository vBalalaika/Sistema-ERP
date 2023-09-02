using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;

namespace ERP.Web.Areas.Clients.Models
{
    public class CommunicationViewModel
    {
        public int Id { get; set; }

        public int ClientId { get; set; }
        public ClientViewModel Client { get; set; }
        public DateTime ComunicationDate { get; set; }
        public string ComunicationDateString { get; set; }

        public string ContactSource { get; set; }
        public SelectList ContactSources { get; set; }
        public string Comments { get; set; }

        public IList<ConsultedProductViewModel> ConsultedProducts { get; set; } = new List<ConsultedProductViewModel>();
        public string ConsultedProductsCount { get; set; }
        public SelectList ConsultedProductsSL { get; set; }

        public SelectList Functionalities { get; set; }
        public SelectList PieceTypes { get; set; }
        public SelectList Operations { get; set; }

        public bool Incoming { get; set; }
        public bool Outgoing { get; set; }

        public string DateTimeToday { get; set; }

        public bool ConsultedProduct { get; set; }
        public bool AnotherReason { get; set; }
        public bool PostSale { get; set; }

        public bool NewOperation { get; set; }
        public bool ExistingOperation { get; set; }

        public int SaleOperationId { get; set; }
        public SaleOperationViewModel SaleOperation { get; set; }
        public int CommunicationNumber { get; set; }
        public string CommunicationName { get; set; }

        public string cpCodeAndDescription { get; set; }
        public string cpFuncionality { get; set; }
        public string cpPieceType { get; set; }

        public string IncomingOrOutgoin { get; set; }
        public bool ViewAll { get; set; }

        public int ConcurrencyToken { get; set; }
    }
}
