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

            // modelBuilder.Entity<Domain.Payment>()
            //             .OwnsOne(c => c.Id);

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

            // var entity = modelBuilder.Entity<Domain.Payment>();
            // entity.HasKey(p => p.Id);
            // entity.ToTable("Payments");
            // entity.Property(p => p.CreditCardNumber).HasColumnName("CreditCardNumber");
            // entity.Property(p => p.CardHolder).HasColumnName("CardHolder");
            // entity.Property(p => p.ExpirationDate).HasColumnName("ExpirationDate");
            // entity.Property(p => p.PaymentStatus).HasColumnName("PaymentStatus");
            // entity.Property(p => p.SecurityCode).HasColumnName("SecurityCode");
            // entity.Property(p => p.Ammount).HasColumnName("Ammount");     
        }
    }
}