using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Drawing.Text;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Policy;
using System.Web.Http;
using System.Web.Http.Description;
using AutoMapper;
using Soldiers_Cafteria.Models;
using Soldiers_Cafteria.Models.InitModels;
using Soldiers_Cafteria.Models.DTOs.Order;
using Soldiers_Cafteria.Models.DTOs.OrderProduct;
using Soldiers_Cafteria.Models.DTOs;

namespace Soldiers_Cafteria.Controllers.Demo
{
    public class OrdersController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/Orders
        public IEnumerable<OrderDetailsDTO> GetOrders()
        {
            return db.Orders.ToList().Select(Mapper.Map<Order, OrderDetailsDTO>);
        }

        // GET: api/Orders/5
        [ResponseType(typeof(OrderDetailsDTO))]
        public IHttpActionResult GetOrder(int id)
        {
            Order order = db.Orders
                .Include(o=>o.Seller)
                .Include(o=>o.Person)
                .Include(o=>o.OrderProducts)
                .SingleOrDefault(o=>o.Id==id);
            if (order == null) 
            {
                return NotFound();
            }
            var orderDetails = new OrderDetailsDTO();
            orderDetails = Mapper.Map<OrderDetailsDTO>(order);
            orderDetails.SellerName = order.Seller.Name;
            orderDetails.PersonName = order.Person.Name;
            orderDetails.BuyerName    = db.People.Find(order.BuyerName).Name;

            var productsInOrder = new List<ProductsOfOrderDTO>();
            foreach (var temp in order.OrderProducts)
            {
                var productInOrder = new ProductsOfOrderDTO
                {
                    Name = temp.Product.Name,
                    CountOfProduct = temp.CountOfProduct,
                    SellPrice = temp.Product.SellPrice,
                    TotalPrice = temp.CountOfProduct * temp.Product.SellPrice
                };
                productsInOrder.Add(productInOrder);
            }
            orderDetails.Products = productsInOrder;
            return Ok(orderDetails);
        }
        [ResponseType(typeof(OrderPageDTO))]
        [HttpGet]
        [Route("api/Order/OrderPage")]
        public IHttpActionResult GetOrderPage()
        {
            var people = db.People
                .Select(p => new PersonDropDown()
                {
                    Id = p.Id,
                    Name = p.Name,
                    Ta2re4a = p.Ta2Re4A.MaxValue - p.Ta2Re4A.CurrentValue
                }).ToList();
            var products = db.Products
                .Select(p => new ProductDropDown()
                {
                    Id = p.Id,
                    Name = p.Name,
                    Count = p.TotalItemsCount,
                    Price = p.SellPrice
                }).ToList();
            return Ok(new OrderPageDTO() {People = people, Products = products});
        }

        // POST: api/Orders
        [ResponseType(typeof(string))]
        public IHttpActionResult PostOrder(AddOrderDTO orderDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.GetErrorMessages());
            }
            //valdition
            OrderValidator ov = ValidateOrder(orderDTO);
            switch (ov)
            {
                case OrderValidator.NonFoundSeller:
                    return BadRequest("هذا البائع غير موجود");
                case OrderValidator.NonFoundPerson:
                    return BadRequest("هذا الشخص غير موجود");
                case OrderValidator.NonFoundProduct:
                    return BadRequest("هناك منتجات مطلوبة غير موجودة");
                case OrderValidator.NotEqualPrice:
                    return BadRequest("سعر المنتجات المطلوبة لا يساوى السعر الكلى");
                case OrderValidator.PriceMoreThanTa2re4a:
                    return BadRequest("التقريشة لا تكفى للدفع");
                case OrderValidator.Ta2re4aAndCashNotEqualPrice:
                    return BadRequest("المبلغ المدفوع على التقريشة و المبلغ المدفوع كاش لا يساوون المبلغ الكلى ");
                case OrderValidator.OrderProductCountLessThanProductCount:
                    return BadRequest("بعض اعداد المنتجات المطلوبة اكبر من الاعداد الموجودة");
                case OrderValidator.ClosedTa2re4a:
                    return BadRequest("التقريشة مغلقة");
            }
            //add order model only in the db
            var order = Mapper.Map<Order>(orderDTO);
            order.Time = DateTime.Now;
            order.OrderProducts = orderDTO.OrderProductsVM
                .Select(o => new OrderProduct {ProductId = o.ProductId, CountOfProduct = o.Count})
                .ToList();
            order.Payments=new List<Payment>();
            switch (orderDTO.PaymentOptionId)
            {
                case 1:
                case 2:
                    order.Payments.Add(new Payment
                    {
                        Time = DateTime.Now,
                        Value = orderDTO.TotalPrice,
                        PaymentOptionId = orderDTO.PaymentOptionId
                    });
                    break;
                case 3:
                    order.Payments.AddRange(new List<Payment>
                    {
                        new Payment
                        {
                            Time = DateTime.Now,
                            Value = orderDTO.PriceFromCash == null ? 0f : (float) orderDTO.PriceFromCash,
                            PaymentOptionId = 1
                        },
                        new Payment
                        {
                            Time = DateTime.Now,
                            Value = orderDTO.PriceFromTa2re4a == null ? 0f : (float) orderDTO.PriceFromTa2re4a,
                            PaymentOptionId = 2
                        }
                    });
                    break;
                default:
                    return BadRequest("طريقة الدفع غير موجودة");
            }
            db.Orders.Add(order);
            if (orderDTO.PaymentOptionId == 2)
            {
                var person =db.People.Find(orderDTO.PersonId);
                person.Ta2Re4A.CurrentValue += orderDTO.TotalPrice;

            }else if (orderDTO.PaymentOptionId == 3)
            {
                var person = db.People.Find(orderDTO.PersonId);
                person.Ta2Re4A.CurrentValue += (float)orderDTO.PriceFromTa2re4a;
            }
            
            order.OrderProducts.ForEach(o =>
            {
                o.Product.TotalItemsCount -= o.CountOfProduct;
            });
            db.SaveChanges();
            return CreatedAtRoute("DefaultApi", new { id = order.Id }, "تم");
        }

        // DELETE: api/Orders/5
        [ResponseType(typeof(OrderDetailsDTO))]
        public IHttpActionResult DeleteOrder(int id)
        {
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return NotFound();
            }
            Person person = order.Person;
            foreach (var payment in order.Payments)
            {
                if(payment.PaymentOptionId==1)
                    person.Ta2Re4A.CurrentValue -= payment.Value;
            }
            db.Orders.Remove(order);
            db.SaveChanges();
            return Ok(Mapper.Map<OrderDetailsDTO>(order));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool OrderExists(int id)
        {
            return db.Orders.Count(e => e.Id == id) > 0;
        }
        private enum OrderValidator { NonFoundSeller,NonFoundPerson,NonFoundProduct,NotEqualPrice,PriceMoreThanTa2re4a,ClosedTa2re4a,Ta2re4aAndCashNotEqualPrice,OrderProductCountLessThanProductCount,True}
        private OrderValidator ValidateOrder(AddOrderDTO orderDTO)
        {
            // check if the seller is in db or not
            var seller = db.Sellers.SingleOrDefault(s => s.Id == orderDTO.SellerId);
            if (seller == null)
            {
                return OrderValidator.NonFoundSeller;
            }
            // check if the person is in db or not
            var person = db.People.SingleOrDefault(p => p.Id == orderDTO.PersonId);
            if (person == null)
            {
                return OrderValidator.NonFoundPerson;
            }
            // check if the total price equal the sum of all products price or not and there is non found product or not

            float priceofAllProducts = 0;
            foreach (var temp in orderDTO.OrderProductsVM)
            {
                var product = db.Products.Find(temp.ProductId);
                if (product == null)
                {
                    return OrderValidator.NonFoundProduct;
                }
                if (temp.Count > product.TotalItemsCount)
                {
                    return OrderValidator.OrderProductCountLessThanProductCount;
                }
                priceofAllProducts += (temp.Count * product.SellPrice);

            }
            if (!float.Equals(priceofAllProducts , orderDTO.TotalPrice))
            {
                return OrderValidator.NotEqualPrice;
            }
            // check if the ta2re4a enough to take total price from it or not and if the ta2re4a open or closed
            switch (orderDTO.PaymentOptionId)
            {
                case 2:
                    if (person.Ta2Re4A.IsOpen)
                    {
                        float rest = person.Ta2Re4A.MaxValue - person.Ta2Re4A.CurrentValue;
                        if (orderDTO.TotalPrice > rest)
                        {
                            return OrderValidator.PriceMoreThanTa2re4a;
                        }
                    }
                    else
                    {
                        return OrderValidator.ClosedTa2re4a;
                    }
                    break;
                case 3:
                    if (!float.Equals((orderDTO.PriceFromTa2re4a + orderDTO.PriceFromCash), orderDTO.TotalPrice))
                    {
                        return OrderValidator.Ta2re4aAndCashNotEqualPrice;
                    }
                    if (person.Ta2Re4A.IsOpen)
                    {
                        float rest = person.Ta2Re4A.MaxValue - person.Ta2Re4A.CurrentValue;
                        if (orderDTO.PriceFromTa2re4a > rest)
                        {
                            return OrderValidator.PriceMoreThanTa2re4a;
                        }
                    }
                    else
                    {
                        return OrderValidator.ClosedTa2re4a;
                    }
                    break;
            }
            return OrderValidator.True;
        }
        
        //[HttpPost]
        //public IHttpActionResult AbdoPostOrder(OrderParameterDTO parameterDTO) 
        //{
        //    if (!IsOrderParameterDTOValid(parameterDTO))
        //    {
        //        return BadRequest("please fill SellerId&&PersonId&&ProductId with existing one");
        //    }
        //    var order=db.Orders.Add
        //    //map property that already in both
        //    var orderAddition = Mapper.Map<Order>(parameterDTO);
        //    //Set the Property That not Matched
        //    order.Time = DateTime.Now;
        //    order.Time = DateTime.Now;
        //    order.OrderProducts = parameterDTO.OrderProductsList.ToList();
            

        //}

        private bool IsOrderParameterDTOValid(OrderParameterDTO parameterDTO)
        {
            var seller = db.Sellers.SingleOrDefault(s => s.Id == parameterDTO.SellerId);
            var person = db.People.SingleOrDefault(p => p.Id == parameterDTO.PersonId);
            if (seller == null || person == null)
            {
                return false;
            }
            foreach (var productList in parameterDTO.OrderProductsList)
            {
                var productId = db.Products.SingleOrDefault(p => p.Id == productList.ProductId);
                if (productId == null)
                {
                    return false;
                }
                else if (productId.TotalItemsCount < productList.CountOfProduct)
                {
                    return false;
                }
                else
                {
                    continue;
                }
            }
            return true;
        }

    }
}