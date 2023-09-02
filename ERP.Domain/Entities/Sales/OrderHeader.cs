using ERP.Domain.Common;
using ERP.Domain.Entities.Clients;
using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities.Sales
{
    public class OrderHeader : IAuditableBaseEntity, IConcurrencyToken
    {
        public int Id { get; set; }

        public int? ClientId { get; set; }
        public Client Client { get; set; }
        public DateTime OrderDate { get; set; }
        public int Number { get; set; }

        //Impuestos
        public decimal Taxes { get; set; }

        //Costo de flete y seguro
        public decimal SecureAndFreightCosts { get; set; }

        //Bonificaciones
        public decimal Bonus { get; set; }

        // Tipo de flete y seguro
        public string TypeOfFreightAndSecure { get; set; }
        // Forma de pago
        public string PaymentMethod { get; set; }
        // Embalaje
        public string Packaging { get; set; }
        // Transporte
        public string Transport { get; set; }
        // Lugar de entrega
        public string PlaceOfDelivery { get; set; }

        //OrderTotalPrice = TotalPrice + Taxes + SecureAndFreightCosts - Bonus
        public decimal OrderTotalPrice { get; set; }

        // Suma de todos los precios unitarios
        public decimal TotalPrice { get; set; }

        public DateTime? ValidOfferDate { get; set; }

        // Observaciones para producción
        public string ProductionObservations { get; set; }
        // Observaciones para ventas
        public string SaleObservations { get; set; }

        public DateTime? DeliveredDate { get; set; }
        public int OrderStateId { get; set; }
        public OrderState OrderState { get; set; }
        public ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

        // Estado de facturación: 1 -> Facturado ; 2 -> No facturado
        public int Billed { get; set; }

        public string User { get; set; }
        public SaleOperation SaleOperation { get; set; }
        public int? SaleOperationId { get; set; }    

        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime? LastModifiedOn { get; set; }
        public int ConcurrencyToken { get; set; }
    }
}
