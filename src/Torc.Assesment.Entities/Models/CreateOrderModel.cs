namespace Torc.Assesment.Entities.Models
{
    public class CreateOrderModel
    {
        public int ProductId { get; set; }
        public int CustomerId { get; set; }
        public int Quantity { get; set; }
    }
}
