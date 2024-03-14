using FlowerShopBusinessObject.DBContext;
using FlowerShopBusinessObject.Entities;
using FlowerShopDAO.FlowerBoquets;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowerShopDAO.OrderDetails
{
    public class OrderDetailDAO
    {
        private static OrderDetailDAO instance = null;
        private readonly ApplicationDBContext _dbContext = null;

        public OrderDetailDAO()
        {
            if (_dbContext == null)
            {
                _dbContext = new ApplicationDBContext();
            }
        }

        public static OrderDetailDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new OrderDetailDAO();
                }
                return instance;
            }
        }

        public List<OrderDetail> GetOrderDetails()
        {
            var listOrderDetails = new List<OrderDetail>();
            try
            {
                listOrderDetails = _dbContext.OrderDetail.ToList();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return listOrderDetails;
        }

        public List<OrderDetail> FindAllOrderDetailsByFlowerBouquetId(string flowerBouquetId)
        {
            var listOrderDetails = new List<OrderDetail>();
            try
            {
                listOrderDetails = _dbContext
                    .OrderDetail
                    .Where(o => o.FlowerBouquetID == Guid.Parse(flowerBouquetId))
                    .ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return listOrderDetails;
        }

        public List<OrderDetail> FindAllOrderDetailsByOrderId(string orderId)
        {
            var listOrderDetails = new List<OrderDetail>();
            try
            {
                listOrderDetails = _dbContext
                    .OrderDetail
                    .Where(o => o.OrderID == Guid.Parse(orderId))
                    .ToList();
                listOrderDetails.ForEach(o =>
                    o.FlowerBouquet = _dbContext.FlowerBouquet.SingleOrDefault(f => f.Id == o.FlowerBouquetID)
                );
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return listOrderDetails;
        }

        public OrderDetail FindOrderDetailByOrderIdAndFlowerBouquetId(string orderId, string flowerBouquetId)
        {
            var orderDetail = new OrderDetail();
            try
            {
                orderDetail = _dbContext
                    .OrderDetail
                    .SingleOrDefault(o => o.OrderID == Guid.Parse(orderId) && o.FlowerBouquetID == Guid.Parse(flowerBouquetId));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return orderDetail;
        }

        public void SaveOrderDetail(OrderDetail orderDetail)
        {
            try
            {
                _dbContext.OrderDetail.Add(orderDetail);
                _dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void UpdateOrderDetail(OrderDetail orderDetail)
        {
            try
            {
                _dbContext.Entry(orderDetail).State =
                        Microsoft.EntityFrameworkCore.EntityState.Modified;
                _dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void DeleteOrderDetail(OrderDetail orderDetail)
        {
            try
            {
                var orderDetailToDelete = _dbContext
                    .OrderDetail
                    .SingleOrDefault(o => o.OrderID == orderDetail.OrderID && o.FlowerBouquetID == orderDetail.FlowerBouquetID);
                _dbContext.OrderDetail.Remove(orderDetailToDelete);
                _dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
