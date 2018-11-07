using MyStore.Persistance;
using MyStore.Persistance.Repositories;
using MyStore.Persistance.Repositories.Interfaces;
using MyStore.Web.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace MyStore.Web.Controllers
{
    [RoutePrefix("Products")]
    public class ProductsController : Controller
    {
        private IProductCategoryRepository _productCategoryRepository;
        private IProductRepository _productRepository;
        private readonly IOrderItemRepository _orderItemRepository;
        public ProductsController(IProductCategoryRepository productCategoryRepository,
            IOrderItemRepository orderItemRepository,
            IProductRepository productRepository)
        {
            _orderItemRepository = orderItemRepository;
            _productCategoryRepository = productCategoryRepository;
            _productRepository = productRepository;
        }
        /// <summary>
        /// Gets product categories.
        /// </summary>
        /// <returns></returns>
        [Route("GetProductCategories")]
        public JsonResult GetProductCategories()
        {
            var categories =  _productCategoryRepository.GetAll().Select(x=>new {
                id= x.Id,
                name= x.Name
            });
            return Json(categories, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// gets default view
        /// </summary>
        /// <returns></returns>
        [Route]
        public ActionResult Index()
        {
            var products = GetProducts("");
            return View(products);
        }
        /// <summary>
        /// Gets the product list.
        /// </summary>
        /// <param name="category">The category.</param>
        /// <returns></returns>
        [Route("{category}")]
        public ActionResult GetProductList(string category="")
        {
            var products = GetProducts(category);
            return View("Index",products);
        }
        /// <summary>
        /// Gets the products by filter.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        //[Route("filtered/{filter}",Name = "GetProductsByFilter")]

        public ActionResult GetProductsByFilter(string filter = "")
        {
            if (filter == "featured")
            {
                var orderItems = _orderItemRepository.GetAll();

              
                    var products = from product in _productRepository.GetAll().AsEnumerable()
                                   join orderItem in _orderItemRepository.GetAll().AsEnumerable() on product.Id equals orderItem.ProductId into product_items
                                   from p in product_items.DefaultIfEmpty()
                                   
                                   select new ProductViewModel
                                   {
                                       Id = product.Id,
                                       Name = product.Name,
                                       Description = product.Description,
                                       ImageUrl = product.ImageUrl,
                                       DiscountPercentage = product.DiscountPercentage ?? 0,
                                       Price = product.Price
                                   };
                    return PartialView("List", products.Take(10).ToList());
                  
               
            }
            else if(filter=="OnSale")
            {
                var products = _productRepository.GetAll().Where(y => y.DiscountPercentage>0).Select(x => new ProductViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    ImageUrl = x.ImageUrl,
                    DiscountPercentage = x.DiscountPercentage ?? 0,
                    Price = x.Price
                }).AsEnumerable();
                return PartialView("List", products.Take(10).ToList());
            }
            else
            { 
                var products = GetProducts("");
                return PartialView("List", products.Take(10).ToList());
            }
          
        }

        /// <summary>
        /// Gets products.
        /// </summary>
        /// <param name="category">The category.</param>
        /// <returns></returns>
        private IEnumerable<ProductViewModel> GetProducts(string category)
        {
            var products = _productRepository.GetAll().Where(y => y.ProductCategory.Name == ((category != "") ? category : y.ProductCategory.Name)).Select(x => new ProductViewModel
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                ImageUrl = x.ImageUrl,
                DiscountPercentage = x.DiscountPercentage ?? 0,
                Price = x.Price
            }).AsEnumerable();
            return products;
        }
        /// <summary>
        /// Lists the specified category.
        /// </summary>
        /// <param name="category">The category.</param>
        /// <returns></returns>
        public ActionResult List(string category)
        {
            var products = GetProducts(category);
            return View(products);
        }
        /// <summary>
        /// Add product to cart.
        /// </summary>
        /// <param name="productId">The product identifier.</param>
        /// <param name="returnUrl">The return URL.</param>
        /// <returns></returns>
        [HttpPost]
        [Route("AddToCart")]
        public ActionResult AddToCart(int productId,int quantity=1, string returnUrl="")
        {
            HttpCookie myCookie = new HttpCookie("cart");

            if (Request.Cookies["cart"] != null)
            {
                myCookie = Request.Cookies["cart"];
            }
            var list = (myCookie.Value == null || myCookie.Value == "") ? new List<CartItem>() : JsonConvert.DeserializeObject<List<CartItem>>(myCookie.Value);

            list.Add(new CartItem {
                ProductId = productId,
                Quantity = quantity
            });
            myCookie.Value = JsonConvert.SerializeObject(list);
            Response.Cookies.Add(myCookie);

            return Redirect(returnUrl);
        }
        /// <summary>
        /// Deletes item from a cart.
        /// </summary>
        /// <param name="productId">The product identifier.</param>
        /// <param name="returnUrl">The return URL.</param>
        /// <returns></returns>
        [HttpPost]
        [Route("DeleteCartItems")]
        public ActionResult DeleteCartItems(List<int> listIds, string returnUrl)
        {
            HttpCookie myCookie = new HttpCookie("cart");

            if (Request.Cookies["cart"] != null)
            {
                myCookie = Request.Cookies["cart"];
            }
            var list = (myCookie.Value == null || myCookie.Value == "") ? new List<CartItem>() : JsonConvert.DeserializeObject<List<CartItem>>(myCookie.Value);

            list.RemoveAll(x => listIds.Contains(x.ProductId));
            myCookie.Value = JsonConvert.SerializeObject(list);
            Response.Cookies.Add(myCookie);

            return Json(new { }, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// Gets the cart information.
        /// </summary>
        /// <returns></returns>
        [Route("GetCartInformation")]
        public JsonResult GetCartInformation()
        {

            var num = 0;

            if (Request.Cookies["cart"] != null)
            {
                num = JsonConvert.DeserializeObject<IEnumerable<CartItem>>(Request.Cookies["cart"].Value).Count();

            }

            return Json(new { number = num }, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// Gets the cart view.
        /// </summary>
        /// <returns></returns>
        [Route("Cart")]
        public ActionResult GetCart()
        {
            var products = new List<ProductViewModel>();
            if (Request.Cookies["cart"] != null)
            {
                var productIds = JsonConvert.DeserializeObject<IEnumerable<CartItem>>(Request.Cookies["cart"].Value);

               products =  productIds.Select(x => {
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
                
              
            }
         
            ViewData["totalAmount"] = products.Sum(x=>x.Price*x.Quantity);
            return View("Cart", products);
        }
        /// <summary>
        /// Details view for the product.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [Route("Details/{name}/{id}")]
        // GET: Products/Details/5
        public ActionResult Details(string name,int id)
        {

            var result = _productRepository.GetById(id);
            var product = new ProductViewModel()
            {
                Id = result.Id,
                Category = result.ProductCategory.Name,
                ImageUrl = result.ImageUrl,
                Name = result.Name,
                Quantity = 1,
                Price = result.Price,
                Description = result.Description,
                DiscountPercentage = result.DiscountPercentage ?? 0
            };
            return View(product);
        }
    }
}
