using FlowerShopBusinessObject.DBContext;
using FlowerShopBusinessObject.Entities;
using FlowerShopDAO.FlowerBoquets;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowerShopDAO.Orders
{
    public class OrderDAO
    {
        private static OrderDAO instance = null;
        private readonly ApplicationDBContext _dbContext = null;

        public OrderDAO()
        {
            if (_dbContext == null)
            {
                _dbContext = new ApplicationDBContext();
            }
        }

        public static OrderDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new OrderDAO();
                }
                return instance;
            }
        }

        public List<Order> GetOrders()
        {
            var listOrders = new List<Order>();
            try
            {
                using (var context = new ApplicationDBContext())
                {
                    listOrders = context.Order.ToList();
                    listOrders.ForEach(o => o.Account = context.Accounts.Find(o.AccountID));
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return listOrders;
        }

        public List<Order> FindAllOrdersByCustomerId(string customerId)
        {
            var listOrders = new List<Order>();
            try
            {
                listOrders = _dbContext.Order.Where(o => o.AccountID == Guid.Parse(customerId)).ToList();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return listOrders;
        }

        public Order FindOrderById(string orderId)
        {
            var order = new Order();
            try
            {
                using (var context = new ApplicationDBContext())
                {
                    order = context.Order.SingleOrDefault(o => o.Id == Guid.Parse(orderId));
                    if (order != null)
                    {
                        order.Account = context.Accounts.Find(order.AccountID);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return order;
        }   

        public Order SaveOrder(Order order)
        {
            try
            {
                using (var context = new ApplicationDBContext())
                {
                    context.Order.Add(order);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return order;
        }

        public void UpdateOrder(Order order)
        {
            try
            {
                using (var context = new ApplicationDBContext())
                {
                    context.Entry(order).State =
                        Microsoft.EntityFrameworkCore.EntityState.Modified;
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void DeleteOrder(Order order)
        {
            try
            {
                var orderToDelete = _dbContext
                    .Order
                    .SingleOrDefault(o => o.Id == order.Id);
                _dbContext.Order.Remove(orderToDelete);
                _dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
