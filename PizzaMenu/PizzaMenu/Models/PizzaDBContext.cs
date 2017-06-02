using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace PizzaMenu.Models
{
    public partial class PizzaDBContext : DbContext
    {
        public virtual DbSet<Ingredients> Ingredients { get; set; }
        public virtual DbSet<Pizza> Pizza { get; set; }
        public virtual DbSet<PizzaIngredients> PizzaIngredients { get; set; }

        public PizzaDBContext(DbContextOptions<PizzaDBContext> options) :base(options)
        {

        }
        public PizzaDBContext() : base()
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
           // #warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
            optionsBuilder.UseSqlServer(@"Data Source=(localdb)\ProjectsV12;Initial Catalog=PizzaDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Ingredients>(entity =>
            {
                entity.Property(e => e.IngredientsId).HasColumnName("Ingredients_ID");

                entity.Property(e => e.IngredientName).HasMaxLength(50);
            });

            modelBuilder.Entity<Pizza>(entity =>
            {
                entity.Property(e => e.PizzaId).HasColumnName("Pizza_ID");

                entity.Property(e => e.Title).HasColumnType("varchar(50)");
            });

            modelBuilder.Entity<PizzaIngredients>(entity =>
            {
                entity.HasKey(e => new { e.PizzId, e.IngredienId })
                    .HasName("PK__PizzaIng__00229AD079031AFF");

                entity.Property(e => e.PizzId).HasColumnName("Pizz_ID");

                entity.Property(e => e.IngredienId).HasColumnName("Ingredien_ID");

                entity.HasOne(d => d.Ingredien)
                    .WithMany(p => p.PizzaIngredients)
                    .HasForeignKey(d => d.IngredienId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_PizzaIngredients_Ingredients");

                entity.HasOne(d => d.Pizz)
                    .WithMany(p => p.PizzaIngredients)
                    .HasForeignKey(d => d.PizzId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_PizzaIngredients_Pizza");
            });
        }
    }
}