using AspCoreBl.Model;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AspCoreBl
{
    public class PaymentDetailContext : IdentityDbContext
    {
        public PaymentDetailContext(DbContextOptions<PaymentDetailContext> options)
            : base(options)
        {
        }
        public DbSet<PaymentDetail> PaymentDetail { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
