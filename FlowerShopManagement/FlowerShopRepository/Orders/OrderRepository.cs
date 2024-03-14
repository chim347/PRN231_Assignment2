using FlowerShopBusinessObject.Entities;
using FlowerShopDAO.OrderDetails;
using FlowerShopDAO.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowerShopRepository.Orders
{
    public class OrderRepository : IOrderRepository
    {
        public Order SaveOrder(Order order) => OrderDAO.Instance.SaveOrder(order);
        public Order GetOrderById(string id) => OrderDAO.Instance.FindOrderById(id);
        public List<Order> GetOrders() => OrderDAO.Instance.GetOrders();
        public List<Order> GetAllOrdersByCustomerId(string customerId) => OrderDAO.Instance.FindAllOrdersByCustomerId(customerId);
        public void UpdateOrder(Order order) => OrderDAO.Instance.UpdateOrder(order);
        public void DeleteOrder(Order order) => OrderDAO.Instance.DeleteOrder(order);
        public List<OrderDetail> GetOrderDetails(string orderId) => OrderDetailDAO.Instance.FindAllOrderDetailsByOrderId(orderId);
    }
}
