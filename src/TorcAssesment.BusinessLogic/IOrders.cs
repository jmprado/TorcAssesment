using Torc.Assesment.Entities.Models;
using Torc.Assesment.Entities.ViewModel;

namespace TorcAssesment.BusinessLogic
{
    public interface IOrders
    {
        Task<OrderCreated> CreateOrder(CreateOrderModel createOrderModel);
    }
}