using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MyStore.Domain
{
    public class ApplicationUser : IdentityUser,IAuditEntity
    {
        [Required]
        [Column("FirstName", TypeName = "VARCHAR")]
        [StringLength(256)]
        public string FirstName { get; set; }
        [Required]
        [Column("Surname", TypeName = "VARCHAR")]
        [StringLength(256)]
        public string Surname { get; set; }
        [Column("CreatedBy", TypeName = "VARCHAR")]
        [StringLength(256)]
        public string CreatedBy { get; set; }
        [Column("CreatedDate", TypeName = "DateTime")]
        public DateTime CreatedDate { get; set; }
        [Column("UpdatedBy", TypeName = "VARCHAR")]
        [StringLength(256)]
        public string UpdatedBy { get; set; }
        [Column("UpdatedDate", TypeName = "DateTime")]
        public DateTime UpdatedDate { get; set; }
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }
}
