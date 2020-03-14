using AspCoreBl.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AspCoreBl
{
    public class PaymentDetailContext : IdentityDbContext<ApplicationUser, ApplicationRole, string, IdentityUserClaim<string>, ApplicationUserRole, IdentityUserLogin<string>, IdentityRoleClaim<string>, IdentityUserToken<string>>
    {
        public PaymentDetailContext(DbContextOptions<PaymentDetailContext> options)
            : base(options)
        {
        }
        public virtual DbSet<PaymentDetail> PaymentDetail { get; set; }
        public virtual DbSet<ApplicationUserRole> ApplicationUserRole { get; set; }
        public virtual DbSet<ApplicationRole> ApplicationRole { get; set; }
        public virtual DbSet<ApplicationUser> ApplicationUser { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }

    public class ContextInitializer
    {
        private PaymentDetailContext _context;

        public ContextInitializer(PaymentDetailContext context)
        {
            _context = context;
        }
        public async Task Seed()
        {
            await _context.Database.MigrateAsync();
            if (await _context.ApplicationRole.CountAsync()==0)
            {
                var roles = new List<ApplicationRole>()
                    {
                        new ApplicationRole()
                        {
                            Name = "Admin",
                            NormalizedName = "ADMIN",
                            DisplayName = "Admin",
                            Id = "1"
                        },
                        new ApplicationRole()
                        {
                            Name = "WebUser",
                            NormalizedName = "WEBUSER",
                            DisplayName = "Web User",
                            Id = "2"
                        },
                        new ApplicationRole()
                        {
                            Name = "System",
                            NormalizedName = "SYSTEM",
                            DisplayName = "System",
                            Id = "3"
                        },
                        new ApplicationRole()
                        {
                            Name = "Dev",
                            NormalizedName = "DEV",
                            DisplayName = "Developer",
                            Id = "4"
                        },
                    };
                _context.ApplicationRole.AddRange(roles);
                await _context.SaveChangesAsync();
            }
        }
    }
}
