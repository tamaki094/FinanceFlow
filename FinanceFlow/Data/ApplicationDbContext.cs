using FinanceFlow.Models;
using Microsoft.EntityFrameworkCore;

namespace FinanceFlow.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Usuario> Usuarios => Set<Usuario>();
        public DbSet<Categoria> Categorias => Set<Categoria>();
        public DbSet<Gasto> Gastos => Set<Gasto>();
        public DbSet<SueldoHistorico> SueldosHistoricos => Set<SueldoHistorico>();
        public DbSet<SueldoPresupuesto> SueldosPresupuestos => Set<SueldoPresupuesto>();


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Gasto>()
                .Property(g => g.Id)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Categoria>()
                .Property(c => c.Id)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<SueldoHistorico>()
                .Property(sh => sh.Id)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<SueldoPresupuesto>()
                .Property(sp => sp.PresupuestoId)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Usuario>()
                .Property(u => u.Id)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Gasto>()
                .HasOne(g => g.Categoria)
                .WithMany()
                .HasForeignKey(g => g.CategoriaId)
                .HasPrincipalKey(c => c.Id);

            modelBuilder.Entity<Gasto>()
                .HasOne(g => g.Usuario)
                .WithMany()
                .HasForeignKey(g => g.UsuarioId)
                .HasPrincipalKey(u => u.Id);
                

            modelBuilder.Entity<Categoria>()
                .HasIndex(C => C.Nombre)
                .IsUnique();

            modelBuilder.Entity<Usuario>()
                .HasIndex(u => u.Email)
                .IsUnique();

            modelBuilder.Entity<SueldoPresupuesto>()
                .HasOne(sp => sp.Sueldo)
                .WithOne()
                .HasForeignKey<SueldoPresupuesto>(sp => sp.SueldoId);
        }
    }
}
