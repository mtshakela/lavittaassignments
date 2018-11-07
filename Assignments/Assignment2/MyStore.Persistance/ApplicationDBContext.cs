using Microsoft.AspNet.Identity.EntityFramework;
using MyStore.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyStore.Persistance
{
    public class ApplicationDBContext : IdentityDbContext<ApplicationUser>, IApplicationDBContext
    {
        public IDbSet<ProductCategory> ProductCategories { get; set; }
        public IDbSet<OrderStatus> OrderStatuses { get; set; }
        public IDbSet<Product> Products { get; set; }
        public IDbSet<Order> Orders { get; set; }
        public IDbSet<OrderItem> OrderItems { get; set; }
        public IDbSet<OrderActivity> OrderActivities { get; set; }
        public IDbSet<DeliveryMethod> DeliveryMethods { get; set; }
        public IDbSet<DeliveryAddress> DeliveryAddresses { get; set ; }
        public IDbSet<AddressType> AddressTypes { get ; set ; }

        //public IDbSet<IdentityUserLogin> UserLogins { get; set; }
        //public IDbSet<IdentityUserRole> UserRoles { get; set; }

        public ApplicationDBContext()
            : base("MyStoreConnection", throwIfV1Schema: false)
        {
        }

        public new IDbSet<T> Set<T>() where T : class
        {
            return base.Set<T>();
        }
        public void Save()
        {
            this.SaveChanges();
        }
        protected override void OnModelCreating(DbModelBuilder builder)
        {

            base.OnModelCreating(builder);
            builder.Entity<ProductCategory>().ToTable("ProductCategories");
            builder.Entity<OrderStatus>().ToTable("OrderStatuses");
            builder.Entity<OrderActivity>().ToTable("OrderActivities");

            builder.Entity<ApplicationUser>().Property(x => x.PhoneNumber).HasColumnName("ContactNumber");
            builder.Entity<ApplicationUser>().Property(x => x.PhoneNumberConfirmed).HasColumnName("ContactNumberConfirmed");

            builder.Entity<ApplicationUser>().ToTable("Users");
            builder.Entity<IdentityRole>().ToTable("Roles");
            //builder.Entity<IdentityUserRole<string>>().ToTable("UserRoles");
            //builder.Entity<IdentityUserRole<string>>().HasKey(c => new { c.UserId, c.RoleId });
          //  builder.Entity<IdentityUserRole<string>>().HasMany(c => c.Users).WithRequired(c => new { c.UserId, c.RoleId });
            //builder.Entity<IdentityUserRole<string>>().Map(c =>
            //{
            //    c.ToTable("UserRoles");
            //}).HasKey(c => new { c.UserId, c.RoleId });

            //builder.Entity<IdentityUserLogin<string>>().Map(c =>
            //{
            //    c.ToTable("UserLogins");
            //}).HasKey(p => new { p.LoginProvider, p.ProviderKey, p.UserId });
            //// Mapping for ApiRole
            //builder.Entity<IdentityRole>().Map(c =>
            //{
            //    c.ToTable("Roles");
            //}).HasKey(p => p.Id);
            //builder.Entity<IdentityRole>().HasMany(c => c.Users).WithRequired().HasForeignKey(c => c.RoleId);

            //builder.Entity<ApplicationUser>().Map(c =>
            //{
            //    c.ToTable("Users");

            //}).HasKey(c => c.Id);
            //builder.Entity<ApplicationUser>().HasMany(c => c.Logins).WithOptional().HasForeignKey(c => c.UserId);
            //builder.Entity<ApplicationUser>().HasMany(c => c.Claims).WithOptional().HasForeignKey(c => c.UserId);
            //builder.Entity<ApplicationUser>().HasMany(c => c.Roles).WithRequired().HasForeignKey(c => c.UserId);




            //builder.Entity<IdentityUserRole<string>>().Map(c =>
            //{
            //    c.ToTable("UserRoles");
            //}).HasKey(c => new { c.UserId, c.RoleId });

            //builder.Entity<IdentityUserClaim<string>>().Map(c =>
            //{
            //    c.ToTable("UserClaims");

            //}).HasKey(c => c.Id);
        }
        public static ApplicationDBContext Create()
        {
            return new ApplicationDBContext();
        }
    }
}
