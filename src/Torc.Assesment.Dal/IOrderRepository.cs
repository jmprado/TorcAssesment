using Torc.Assesment.Entities.Models;

namespace Torc.Assesment.Dal
{
    public interface IOrderRepository
    {
        Task ExecCreateOrderProcedure(CreateOrderModel createOrderModel);
    }
}