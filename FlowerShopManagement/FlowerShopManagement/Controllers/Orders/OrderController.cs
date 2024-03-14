using FlowerShopBusinessObject.Entities;
using FlowerShopDAO.Orders;
using FlowerShopManagement.Models;
using FlowerShopRepository.FlowerBouquets;
using FlowerShopRepository.OrderDetails;
using FlowerShopRepository.Orders;
using FlowerShopRepository.Suppliers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace FlowerShopManagement.Controllers.Orders
{
    [Route("api/v1/order")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private IOrderRepository _orderRepository;
        private IOrderDetailRepository _orderDetailRepository;
        private IFlowerBouquetRepository _flowerBouquetRepository;
        public OrderController(IOrderRepository orderRepository,
            IOrderDetailRepository orderDetailRepository,
            IFlowerBouquetRepository flowerBouquetRepository)
        {
            _orderRepository = orderRepository;
            _orderDetailRepository = orderDetailRepository;
            _flowerBouquetRepository = flowerBouquetRepository;
        }

        [Authorize(Roles = "1")]
        [HttpGet]
        public ActionResult<IEnumerable<Order>> GetOrders() => Ok(_orderRepository.GetOrders());

        [Authorize(Roles = "4")]
        [HttpGet("customer/{id}")]
        public ActionResult<IEnumerable<Order>> GetAllOrdersByCustomerId(string id)
        {
            var listOrder = _orderRepository.GetAllOrdersByCustomerId(id);
            foreach (var o in listOrder)
            {
                var orderDetails = _orderDetailRepository.GetOrderDetailsByOrderId(o.Id.ToString());
                o.OrderDetails = orderDetails;
            }
            return Ok(listOrder);
        }

        [Authorize(Roles = "4")]
        [HttpGet("customer/detail/{id}")]
        public ActionResult<Order> GetOrderDetailById(string id)
        {
            var order = _orderRepository.GetOrderById(id);
            if (order == null)
            {
                return NotFound();
            }
            var orderDetails = _orderDetailRepository.GetOrderDetailsByOrderId(id);
            order.OrderDetails = orderDetails;
            return Ok(order);
        }


        [Authorize(Roles = "1")]
        [HttpGet("{id}")]
        public ActionResult<Order> GetOrderById(string id)
        {
            var order = _orderRepository.GetOrderById(id);
            if (order == null)
            {
                return NotFound();
            }
            var orderDetails = _orderDetailRepository.GetOrderDetailsByOrderId(id);
            order.OrderDetails = orderDetails;
            return Ok(order);
        }

        [Authorize(Roles = "1, 4")]
        [HttpPost]
        public ActionResult<Order> PostOrder(CreateOrder postOrder)
        {
            foreach (var od in postOrder.OrderDetails)
            {
                var fb = _flowerBouquetRepository.GetFlowerBouquetById(od.FlowerBouquetID.ToString());
                if (fb == null)
                {
                    return NotFound();
                }
                if (fb.FlowerBouquetStatus != 1)
                {
                    return BadRequest();
                }
                if (fb.UnitsInStock < od.Quantity)
                {
                    return BadRequest();
                }
            }
            var order = new Order
            {
                OrderDate = postOrder.OrderDate,
                ShippedDate = null,
                Total = postOrder.Total,
                OrderStatus = 0,
                Freight = postOrder.Freight,
                AccountID = Guid.Parse(postOrder.CustomerID)
            };
            var savedOrder = _orderRepository.SaveOrder(order);
            foreach (var od in postOrder.OrderDetails)
            {
                var fb = _flowerBouquetRepository.GetFlowerBouquetById(od.FlowerBouquetID.ToString());
                fb.UnitsInStock -= od.Quantity;
                var orderDetail = new OrderDetail
                {
                    FlowerBouquetID = Guid.Parse(od.FlowerBouquetID),
                    UnitPrice = od.UnitPrice,
                    Quantity = od.Quantity,
                    OrderID = savedOrder.Id,
                    Discount = 0
                };
                _flowerBouquetRepository.UpdateFlowerBouquet(fb);
                _orderDetailRepository.SaveOrderDetail(orderDetail);
            }
            return Ok(savedOrder);
        }

        [Authorize(Roles = "1")]
        [HttpPut("shipped/{id}")]
        public IActionResult PutOrderShipped(string id)
        {
            var oTmp = _orderRepository.GetOrderById(id);
            if (oTmp == null)
            {
                return NotFound();
            }
            if (oTmp.OrderStatus != 0)
            {
                return BadRequest();
            }
            oTmp.ShippedDate = DateTime.Now;
            oTmp.OrderStatus = 1;
            _orderRepository.UpdateOrder(oTmp);
            return NoContent();
        }

        [Authorize(Roles = "1")]
        [HttpPut("cancel/{id}")]
        public IActionResult PutOrderCancel(string id)
        {
            var oTmp = _orderRepository.GetOrderById(id);
            if (oTmp == null)
            {
                return NotFound();
            }
            if (oTmp.OrderStatus != 0)
            {
                return BadRequest();
            }
            oTmp.OrderStatus = 2;
            _orderRepository.UpdateOrder(oTmp);
            var orderDetails = _orderDetailRepository.GetOrderDetailsByOrderId(id);
            foreach (var od in orderDetails)
            {
                var fb = _flowerBouquetRepository.GetFlowerBouquetById(od.FlowerBouquetID.ToString());
                fb.UnitsInStock += od.Quantity;
                _flowerBouquetRepository.UpdateFlowerBouquet(fb);
            }
            return NoContent();
        }
    }
}
