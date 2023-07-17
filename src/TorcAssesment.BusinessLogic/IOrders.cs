using Torc.Assesment.Entities.ViewModel;

namespace TorcAssesment.BusinessLogic
{
    public interface IOrders
    {
        Task CreateOrder(CreateOrderModel createOrderModel);
    }
}