using Microsoft.EntityFrameworkCore;

namespace webapi.src.Payment.Infrastructure
{
    public class PaymentDbContext: DbContext
    {
        public DbSet<Domain.Payment> Payments { get; set; }

        public PaymentDbContext(DbContextOptions<PaymentDbContext> options)
            : base(options)
        { }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Domain.Payment>()
                        .ToTable("Payments")
                        .HasKey(k => k.Id);
                        
            modelBuilder.Entity<Domain.Payment>()
                        .Property(x => x.Id)
                        .HasConversion(x => x.Value, x => new Domain.ValueObject.PaymentId(x));

            modelBuilder.Entity<Domain.Payment>()
                        .OwnsOne(c => c.CreditCardNumber);

            modelBuilder.Entity<Domain.Payment>()
                        .OwnsOne(c => c.CardHolder);

            modelBuilder.Entity<Domain.Payment>()
                        .OwnsOne(c => c.SecurityCode);

            modelBuilder.Entity<Domain.Payment>()
                        .OwnsOne(c => c.Ammount);

            modelBuilder.Entity<Domain.Payment>()
                        .OwnsOne(c => c.ExpirationDate);     
        }
    }
}