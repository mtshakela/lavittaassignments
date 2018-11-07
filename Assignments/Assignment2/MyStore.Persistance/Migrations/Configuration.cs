namespace MyStore.Persistance.Migrations
{
    using Microsoft.AspNet.Identity.EntityFramework;
    using MyStore.Common;
    using MyStore.Domain;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<ApplicationDBContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            ContextKey = "MyStore.Persistance.ApplicationDbContext";
        }

        protected override void Seed(ApplicationDBContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
            InitializeRoles(context);
            InitializeProductCategories(context);
            InitializeOrderStatuses(context);
            InitializeProducts(context);
            InitializeDeliveryMethods(context);
            InitializeAddressTypes(context);
        }

        /// <summary>
        /// Initializes the roles.
        /// </summary>
        /// <param name="context">The context.</param>
        private void InitializeRoles(ApplicationDBContext context)
        {

            var cats = new List<string>() {Constants.Roles.SystemAdministrator,
                Constants.Roles.Owner,
            Constants.Roles.User};
            foreach (var item in cats)
            {
                var cat = SetRole(context, item);
                context.Roles.AddOrUpdate(cat);
            }

        }
        /// <summary>
        /// Initializes the product categories.
        /// </summary>
        /// <param name="context">The context.</param>
        private void InitializeProductCategories(ApplicationDBContext context)
        {
            var dt = DateTime.Now;
            var cats = new List<string>() { "Beauty","Lifestyle", "Mommy and Me", "Health" };
            foreach (var item in cats)
            {
                var cat = SetProductCategory(context, item);
                context.ProductCategories.AddOrUpdate(cat);
            }
            
        }
        /// <summary>
        /// Initializes the order statuses.
        /// </summary>
        /// <param name="context">The context.</param>
        private void InitializeOrderStatuses(ApplicationDBContext context)
        {
            var dt = DateTime.Now;
            var cats = new List<string>() {Constants.OrderStatuses.Submitted,

                Constants.OrderStatuses.Dispatched,
                Constants.OrderStatuses.Cancelled,
                Constants.OrderStatuses.Returned,
                Constants.OrderStatuses.Collected,
                Constants.OrderStatuses.Delivered};
            foreach (var item in cats)
            {
                var cat = SetOrderStatus(context, item);
                context.OrderStatuses.AddOrUpdate(cat);
            }

        }
        /// <summary>
        /// Initializes the delivery methods.
        /// </summary>
        /// <param name="context">The context.</param>
        private void InitializeDeliveryMethods(ApplicationDBContext context)
        {
            var dt = DateTime.Now;
            var cats = new List<string>() {Constants.DeliveryMethods.Delivery,

                Constants.DeliveryMethods.Collect};
            foreach (var item in cats)
            {
                var cat = SetDeliveryMethod(context, item);
                context.DeliveryMethods.AddOrUpdate(cat);
            }

        }
        private void InitializeAddressTypes(ApplicationDBContext context)
        {
            var dt = DateTime.Now;
            var cats = new List<string>() {Constants.AddressTypes.Residential,

                Constants.AddressTypes.Business};
            foreach (var item in cats)
            {
                var cat = SetAddressType(context, item);
                context.AddressTypes.AddOrUpdate(cat);
            }
        }
        /// <summary>
        /// Initializes the products.
        /// </summary>
        /// <param name="context">The context.</param>
        private void InitializeProducts(ApplicationDBContext context)
        {
            var dt = DateTime.Now;
            var cat = context.ProductCategories.FirstOrDefault(x => x.Name == "Beauty");
            var cat1 = context.ProductCategories.FirstOrDefault(x => x.Name == "Lifestyle");
            var cat2 = context.ProductCategories.FirstOrDefault(x => x.Name == "Mommy and Me");
            var cat3 = context.ProductCategories.FirstOrDefault(x => x.Name == "Health");
            var products = new List<Product>() {
                SetProduct(context, "Aloe Hydrating Cream",230,"images/products/Aloe-Cream.jpg",cat.Id),
                SetProduct(context, "Aloe Hydrating Lotion",159,"images/products/Aloe-Lotion.jpg",cat.Id),
                SetProduct(context, "Aloe Hydrating Mask",120,"images/products/Aloe-Cream.jpg",cat.Id),
                SetProduct(context, "Aloe Hydrating Tonner",230,"images/products/Aloe-toner.jpg",cat.Id),

                SetProduct(context, "Aloe Body Scrub",120,"images/products/Body-Scrub_RBG.jpg",cat1.Id),
                SetProduct(context, "Aloe Deodorant",30,"images/products/La-Vita_Deodrant.jpg",cat1.Id),
                SetProduct(context, "Aloe Toothpaste",56,"images/products/Tooth-paste.jpg",cat1.Id),
                SetProduct(context, "Aloe Bathroom Detergent",129,"images/products/Bathroom-Detergent-1.jpg",cat1.Id),

                 SetProduct(context, "Baby Bath",56,"images/products/Baby-Bath.jpg",cat2.Id),
                SetProduct(context, "Night Sanitary Pads",129,"images/products/Pad_Night_2-pack-1.jpg",cat2.Id),

                 SetProduct(context, "Aloe Vera Refreshment Drink",56,"images/products/Aloe-Vera-Refreshment-Drink.jpg",cat3.Id),
                SetProduct(context, "La Vita Aloe Juice",129,"images/products/La-Vita_Aloe-Juice.jpg",cat3.Id),


            };
            foreach (var item in products)
            {
                context.Products.AddOrUpdate(item);
            }
                 }
        /// <summary>
        /// Sets the role.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        private IdentityRole SetRole(ApplicationDBContext context, string name)
        {
            var dt = DateTime.Now;
            var cat = new IdentityRole
            {
                Name = name,
            };

            return cat = context.Roles.FirstOrDefault(x => x.Name == name) ?? cat;
        }
        /// <summary>
        /// Sets the order status.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        private OrderStatus SetOrderStatus(ApplicationDBContext context, string name)
        {
            var dt = DateTime.Now;
            var cat = new OrderStatus
            {
                Name = name,
                CreatedBy = Constants.GeneratedBy,
                CreatedDate = dt,
                UpdatedBy = Constants.GeneratedBy,
                UpdatedDate = dt
            };

            return cat = context.OrderStatuses.FirstOrDefault(x => x.Name == name) ?? cat;
        }
        /// <summary>
        /// Sets the type of the address.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        private AddressType SetAddressType(ApplicationDBContext context, string name)
        {
            var dt = DateTime.Now;
            var cat = new AddressType
            {
                Name = name,
                CreatedBy = Constants.GeneratedBy,
                CreatedDate = dt,
                UpdatedBy = Constants.GeneratedBy,
                UpdatedDate = dt
            };

            return cat = context.AddressTypes.FirstOrDefault(x => x.Name == name) ?? cat;
        }
        /// <summary>
        /// Sets the delivery method.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        private DeliveryMethod SetDeliveryMethod(ApplicationDBContext context, string name)
        {
            var dt = DateTime.Now;
            var cat = new DeliveryMethod
            {
                Name = name,
                CreatedBy = Constants.GeneratedBy,
                CreatedDate = dt,
                UpdatedBy = Constants.GeneratedBy,
                UpdatedDate = dt
            };

            return cat = context.DeliveryMethods.FirstOrDefault(x => x.Name == name) ?? cat;
        }
        /// <summary>
        /// Sets the product category.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        private ProductCategory SetProductCategory(ApplicationDBContext context,string name)
        {
            var dt = DateTime.Now;
            var cat = new ProductCategory
            {
                Name = name,
                CreatedBy = Constants.GeneratedBy,
                CreatedDate = dt,
                UpdatedBy = Constants.GeneratedBy,
                UpdatedDate = dt
            };

            return cat = context.ProductCategories.FirstOrDefault(x => x.Name == name)??cat;
        }
        /// <summary>
        /// Sets the product.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="name">The name.</param>
        /// <param name="price">The price.</param>
        /// <param name="categoryId">The category identifier.</param>
        /// <returns></returns>
        private Product SetProduct(ApplicationDBContext context, string name,decimal price,string imgURL,int categoryId)
        {
            var dt = DateTime.Now;
            var cat = new Product
            {
                Name = name,
                Description = "Description goes here",
                ImageUrl = imgURL,
                Price = price,
                DiscountPercentage = 0,
                ProductCategoryId =categoryId,
                CreatedBy = Constants.GeneratedBy,
                CreatedDate = dt,
                UpdatedBy = Constants.GeneratedBy,
                UpdatedDate = dt
            };

            return cat = context.Products.FirstOrDefault(x => x.Name == name) ?? cat;
        }
    }
}
