using FlowerShopBusinessObject.Entities;
using FlowerShopDAO.OrderDetails;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowerShopRepository.OrderDetails
{
    public class OrderDetailRepository : IOrderDetailRepository
    {
        public void SaveOrderDetail(OrderDetail orderDetail) => OrderDetailDAO.Instance.SaveOrderDetail(orderDetail);
        public OrderDetail GetOrderDetailByOrderIdAndFlowerBouquetId(string orderId, string flowerBouquetId) => OrderDetailDAO.Instance.FindOrderDetailByOrderIdAndFlowerBouquetId(orderId, flowerBouquetId);
        public List<OrderDetail> GetOrderDetails() => OrderDetailDAO.Instance.GetOrderDetails();
        public List<OrderDetail> GetOrderDetailsByOrderId(string orderId) => OrderDetailDAO.Instance.FindAllOrderDetailsByOrderId(orderId);
        public void UpdateOrderDetail(OrderDetail orderDetail) => OrderDetailDAO.Instance.UpdateOrderDetail(orderDetail);
        public void DeleteOrderDetail(OrderDetail orderDetail) => OrderDetailDAO.Instance.DeleteOrderDetail(orderDetail);
    }
}
