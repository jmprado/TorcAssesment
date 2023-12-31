﻿using Torc.Assesment.Entities.Models;
using Torc.Assesment.Entities.ViewModel;

namespace Torc.Assesment.Dal
{
    public interface IOrderRepository
    {
        Task<OrderCreated> ExecCreateOrderProcedure(CreateOrderModel createOrderModel);
        Task<IEnumerable<Order>> ListOrders();
        void Dispose();
    }
}