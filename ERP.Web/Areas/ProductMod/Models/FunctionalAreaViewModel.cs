namespace ERP.Web.Areas.ProductMod.Models
{
    public class FunctionalAreaViewModel
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public int ConcurrencyToken { get; set; }

        public int Order
        {
            get
            {
                if (this.Description == "Corte" || this.Id == 3)
                {
                    return 1;
                }
                else if (this.Description == "Plegado y Mecanizado" || this.Id == 4)
                {
                    return 2;
                }
                else if (this.Description == "Soldadura" || this.Id == 5)
                {
                    return 3;
                }
                else if (this.Description == "Montaje" || this.Id == 6)
                {
                    return 4;
                }
                else if (this.Description == "Recepción" || this.Id == 1)
                {
                    return 5;
                }
                else if (this.Description == "Almacén" || this.Id == 2)
                {
                    return 6;
                }
                else if (this.Description == "Despacho" || this.Id == 7)
                {
                    return 7;
                }
                else
                {
                    return 0;
                }
            }
        }
    }
}
