using System.Reflection;
using Estacionamento.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Estacionamento.Data;

public class EstacionamentoDbContext : DbContext
{
    public DbSet<Condutor> Condutores { get; set; }
    public DbSet<PeriodoLivre> PeriodosLivres { get; set; }
    public DbSet<Veiculo> Veiculos { get; set; }
    public DbSet<Locacao> Locacoes { get; set; }
    public DbSet<PoliticaPreco> PoliticasPrecos { get; set; }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // TODO PEGAR JSON CONFIG
        optionsBuilder.UseSqlite("Filename=Estacionamento.db", options =>
        {
            options.MigrationsAssembly(Assembly.GetExecutingAssembly().FullName);
        });
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
            entity.HasOne<PoliticaPreco>(nameof(PeriodoLivre.PoliticaPrecoId));
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
            entity.HasOne<Condutor>(nameof(Veiculo.CondutorId));
        });
        
        modelBuilder.Entity<Locacao>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasOne<Condutor>(nameof(Locacao.CondutorId));
            entity.HasOne<Veiculo>(nameof(Locacao.VeiculoId));
            
            entity.Property(x => x.Inicio).HasColumnType("datetime");
            entity.Property(x => x.Fim).HasColumnType("datetime");
            entity.Property(x => x.Fim).HasDefaultValue(null);
        });
        
        base.OnModelCreating(modelBuilder);
    }
}