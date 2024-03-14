using FlowerShopBusinessObject.Entities;
using FlowerShopDAO.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowerShopRepository.Orders
{
    public interface IOrderRepository
    {
        Order SaveOrder(Order order);
        Order GetOrderById(string id);
        List<Order> GetOrders();
        List<Order> GetAllOrdersByCustomerId(string customerId);
        void UpdateOrder(Order order);
        void DeleteOrder(Order order);
        List<OrderDetail> GetOrderDetails(string orderId);
    }
}
