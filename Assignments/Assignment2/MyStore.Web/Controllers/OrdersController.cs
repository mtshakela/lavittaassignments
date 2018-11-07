using Microsoft.AspNet.Identity.Owin;
using MyStore.Domain;
using MyStore.Persistance.Repositories.Interfaces;
using MyStore.Web.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyStore.Web.Controllers
{
    [RoutePrefix("Orders")]
    [Authorize]
    public class OrdersController : Controller
    {
        private ApplicationUserManager _userManager;
        private readonly IProductRepository _productRepository;
        private readonly IDeliveryMethodRepository _deliveryMethodRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IOrderItemRepository _orderItemRepository;
        private readonly IOrderStatusRepository _orderStatusRepository;
        public OrdersController( IProductRepository productRepository, IOrderRepository orderRepository, IOrderItemRepository orderItemRepository,
            
            IDeliveryMethodRepository deliveryMethodRepository,IOrderStatusRepository orderStatusRepository)
        {

            _deliveryMethodRepository = deliveryMethodRepository;
             _productRepository = productRepository;
            _orderRepository = orderRepository;
            _orderItemRepository = orderItemRepository;
            _orderStatusRepository = orderStatusRepository;
        }
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }


        /// <summary>
        /// Default view
        /// </summary>
        /// <returns></returns>
        [Route("")]
        public ActionResult Index()
        {
            var user = UserManager.FindByNameAsync(User.Identity.Name).Result;
            var orders = _orderRepository.GetAll().Where(u=>u.UserId== user.Id).Select(x => new OrderViewModel()
            {
         
                Id = x.Id,
                CreatedDate = x.CreatedDate,
                Status =(x.OrderStatus.Name==Common.Constants.OrderStatuses.Submitted)?"Awaiting to be dispatched": Common.Constants.OrderStatuses.Dispatched,
                Items = x.OrderItems.Select(y=>new OrderItemViewModel {
                    Id = y.Id,
                    Price =y.Price,
                    ProductName = y.Product.Name,
                    ImageURL = y.Product.ImageUrl,
                    ProductId = y.ProductId,
                    Quantity = y.Quantity
                })
            });
            return View(orders);
        }
        /// <summary>
        /// Checkouts this instance.
        /// </summary>
        /// <returns></returns>
        [Route("~/Checkout")]
        public ActionResult Checkout()
        {
            //if (!User.Identity.IsAuthenticated)
            //{
            //    ViewBag.ReturnUrl = Request.Url;
            //    return RedirectToAction("Login", "Account");
            //}
            var products = new List<ProductViewModel>();

            var productIds = JsonConvert.DeserializeObject<IEnumerable<CartItem>>(Request.Cookies["cart"].Value);

            products = productIds.Select(x =>
            {
                var p = _productRepository.GetById(x.ProductId);

                return new ProductViewModel
                {
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Description,
                    ImageUrl = p.ImageUrl,
                    Quantity = x.Quantity,
                    DiscountPercentage = p.DiscountPercentage ?? 0,
                    Price = p.Price
                };
            }).ToList();

            var newOrder = new NewOrderViewModel()
            {
                products = products
            };
            return View("Checkout", newOrder);
        }
        /// <summary>
        /// Payments
        /// </summary>
        /// <returns></returns>
        [Route("~/Checkout/Payment")]
        public ActionResult Payment()
        {
            return View("Payment");
        }/// <summary>
        /// Submits order
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [Route("SubmitOrder")]
        public ActionResult SubmitOrder()
        {
            var productItems = new List<OrderItem>();
            if (Request.Cookies["cart"] != null)
            {
                var productIds = JsonConvert.DeserializeObject<IEnumerable<CartItem>>(Request.Cookies["cart"].Value);

                productItems = productIds.Select(x =>
                {
                    var p = _productRepository.GetById(x.ProductId);

                    return new OrderItem
                    {
                        ProductId = p.Id,
                        Quantity = x.Quantity,
                        Price = p.Price,

                    };
                }).ToList();
                var status = _orderStatusRepository.GetAll().FirstOrDefault(x => x.Name == Common.Constants.OrderStatuses.Submitted);
                var deliveryMothod = _deliveryMethodRepository.GetAll().FirstOrDefault(x => x.Name == Common.Constants.DeliveryMethods.Delivery);
                var user = UserManager.FindByNameAsync(User.Identity.Name).Result;
                var order = new Order()
                {
                    DeliveryMethodId = deliveryMothod.Id,
                    OrderStatusId = status.Id,
                    OrderItems = productItems,
                    UserId = user.Id,
                    CreatedBy = User.Identity.Name,
                    UpdatedBy = User.Identity.Name,
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now
                };
                _orderRepository.Add(order);

                _orderRepository.Save();
                HttpCookie cookie = Request.Cookies["cart"];
                cookie.Expires = DateTime.Now.AddDays(-1);
                Response.Cookies.Add(cookie);

                ViewData["orderNumber"] = order.Id;
                ViewData["username"] = User.Identity.Name;
            }
           
            return View("Confirmation");
        }
        [Route("~/Checkout/Confirmation")]
        public ActionResult PaymentDone()
        {
            return View("Confirmation");
        }
        [Route("Details/{id}")]
        // GET: Products/Details/5
        public ActionResult Details(int id)
        {
            var result = _orderRepository.GetById(id);

            var order = new OrderViewModel()
            {
                Id = result.Id,
                CreatedDate =result.CreatedDate,
                Status = (result.OrderStatus.Name == Common.Constants.OrderStatuses.Submitted) ? "Awaiting to be dispatched" : Common.Constants.OrderStatuses.Dispatched,
                Items = result.OrderItems.Select(y => {
                    
                    return new OrderItemViewModel
                    {
                        Id = y.Id,
                        Price = y.Price,
                        ProductName = y.Product.Name,
                        ImageURL = y.Product.ImageUrl,
                        ProductId = y.ProductId,
                        Quantity = y.Quantity
                    };
                }),
                SubTotal = result.OrderItems.Sum(z=>z.Price* z.Quantity),
          
            };
            order.Total = order.SubTotal;
            return View(order);
        }
    }
}