using System.Reflection;
using Estacionamento.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Estacionamento.Data;

public class EstacionamentoDbContext : DbContext
{
    public EstacionamentoDbContext(DbContextOptions<EstacionamentoDbContext> options) : base(options) { }
    
    public DbSet<Condutor> Condutores { get; set; }
    public DbSet<Veiculo> Veiculos { get; set; }
    public DbSet<Locacao> Locacoes { get; set; }
    public DbSet<PoliticaPreco> PoliticasPrecos { get; set; }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
        
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            optionsBuilder.UseSqlite(connectionString, options =>
            {
                options.MigrationsAssembly(Assembly.GetExecutingAssembly().FullName);
            });
        }
       
        base.OnConfiguring(optionsBuilder);
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Condutor>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasIndex(e => e.Cpf).IsUnique();
            entity.Property(e => e.Cpf).HasMaxLength(14);
            entity.Property(x => x.Nome).HasMaxLength(50);
        });
        
        modelBuilder.Entity<PeriodoLivre>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasOne<PoliticaPreco>().WithMany(x=>x.PeriodosLivres);
        });
        
        modelBuilder.Entity<PoliticaPreco>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(x => x.DataBase).HasColumnType("datetime");
            entity.Property(x => x.InicioVigencia).HasColumnType("datetime");
            entity.Property(x => x.FimVigencia).HasColumnType("datetime");
        });
        
        modelBuilder.Entity<Veiculo>(entity =>
        {
            entity.HasKey(e => e.Id);
        });
        
        modelBuilder.Entity<Locacao>(entity =>
        {
            entity.HasKey(e => e.Id);
            
            entity.Property(x => x.Inicio).HasColumnType("datetime");
            entity.Property(x => x.Fim).HasColumnType("datetime");
            entity.Property(x => x.Fim).HasDefaultValue(null);
        });
        
        base.OnModelCreating(modelBuilder);
    }
}