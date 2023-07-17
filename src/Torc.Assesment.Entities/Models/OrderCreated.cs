using Microsoft.EntityFrameworkCore;

namespace Torc.Assesment.Entities.Models
{
    [Keyless]
    public class OrderCreated
    {
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public int ProductId { get; set; }
        public string? ProductName { get; set; }
        public decimal ProductPrice { get; set; }
        public int CustomerId { get; set; }
        public string? CustomerName { get; set; }
        public int Quantity { get; set; }
        public decimal OrderTotal { get; set; }
    }
}
