using Torc.Assesment.Entities.Models;

namespace TorcAssesment.BusinessLogic
{
    public interface IOrders
    {
        Task CreateOrder(CreateOrderModel createOrderModel);
    }
}