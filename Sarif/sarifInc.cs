namespace Sarif
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class sarifInc : DbContext
    {
        public sarifInc()
            : base("name=sarifInc")
        {
        }

        public virtual DbSet<categories> categories { get; set; }
        public virtual DbSet<product> product { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<categories>()
                .Property(e => e.categorie)
                .IsUnicode(false);

            modelBuilder.Entity<categories>()
                .HasMany(e => e.product)
                .WithRequired(e => e.categories)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<product>()
                .Property(e => e.name)
                .IsUnicode(false);

            modelBuilder.Entity<product>()
                .Property(e => e.description)
                .IsUnicode(false);

            modelBuilder.Entity<product>()
                .Property(e => e.serialNumber)
                .IsUnicode(false);
        }
    }
}