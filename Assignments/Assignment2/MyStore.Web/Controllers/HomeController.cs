using MyStore.Persistance;
using MyStore.Persistance.Repositories;
using MyStore.Persistance.Repositories.Interfaces;
using MyStore.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace MyStore.Web.Controllers
{
    public class HomeController : Controller
    {

        private IProductCategoryRepository _productCategoryRepository;
        private IProductRepository _productRepository;
        private readonly IOrderItemRepository _orderItemRepository;
        public HomeController(IProductCategoryRepository productCategoryRepository,
            IOrderItemRepository orderItemRepository,
            IProductRepository productRepository)
        {
            _orderItemRepository = orderItemRepository;
            _productCategoryRepository = productCategoryRepository;
            _productRepository = productRepository;
        }

        
        public ActionResult Index()
        {
            var products = _productRepository.GetAll().Select(x => new ProductViewModel
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                ImageUrl = x.ImageUrl,
                DiscountPercentage = x.DiscountPercentage ?? 0,
                Price = x.Price
            }).AsEnumerable();
            ViewData["products"] = products;
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}