using Microsoft.EntityFrameworkCore;
using SistemaConsultasUVV.Models;

namespace SistemaConsultasUVV.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Usuario> Usuarios => Set<Usuario>();
    public DbSet<Consulta> Consultas => Set<Consulta>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Garante índice único para e-mails cadastrados
        modelBuilder.Entity<Usuario>()
            .HasIndex(u => u.Email)
            .IsUnique();

        // Relacionamento 1:N entre Usuario e Consulta
        modelBuilder.Entity<Consulta>()
            .HasOne(c => c.Usuario)
            .WithMany(u => u.Consultas)
            .HasForeignKey(c => c.UsuarioId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
