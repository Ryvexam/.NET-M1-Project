using Microsoft.EntityFrameworkCore;
using MonApp.Events;

namespace MonApp.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext() { }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<EventEntity> Events { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder
                .Entity<EventEntity>(e =>
                {
                    // Clé primaire
                    e.HasKey(x => x.Id);

                    // Propriétés requises avec contraintes
                    e.Property(x => x.Title)
                        .IsRequired()
                        .HasMaxLength(40);

                    e.Property(x => x.Address)
                        .IsRequired();

                    e.Property(x => x.PostalCode)
                        .IsRequired()
                        .HasMaxLength(5);

                    e.Property(x => x.City)
                        .IsRequired();

                    e.Property(x => x.StartDateTime)
                        .IsRequired();

                    e.Property(x => x.EndDateTime)
                        .IsRequired();

                    e.Property(x => x.MaxRegistrations)
                        .IsRequired();
                    
                    e.Property(x => x.CreatedBy)
                        .IsRequired();

                    e.Property(x => x.Status)
                        .IsRequired()
                        .HasConversion<string>();

                    // Propriétés optionnelles avec contraintes
                    e.Property(x => x.Description)
                        .HasMaxLength(1000);

                    // Propriété avec valeur par défaut
                    e.Property(x => x.CreatedAt)
                        .IsRequired()
                        .HasDefaultValueSql("GETUTCDATE()");

                    // Configuration pour stocker la liste de tags
                    e.Property(x => x.Tags)
                        .HasConversion(
                            v => v != null ? string.Join(',', v) : null,
                            v => v != null ? v.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList() : null);

                    // Index pour les recherches fréquentes
                    e.HasIndex(x => x.StartDateTime);
                    e.HasIndex(x => x.Status);
                    e.HasIndex(x => x.Title);
                });
        }
    }
}