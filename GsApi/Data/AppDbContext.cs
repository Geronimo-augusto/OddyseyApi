using OddyseyApi.Model;
using OddyseyApi.Models;
using Microsoft.EntityFrameworkCore;
using System;
namespace OddyseyApi.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    // Mapeamento das Entidades para Tabelas no SQL Server
    public DbSet<PredictionHistory> PredictionHistories { get; set; }

    // Mapeia a classe base. O EF Core vai entender a herança automaticamente.
    public DbSet<SpaceEquipment> SpaceEquipments { get; set; }
    public DbSet<BiotelemetryTag> BiotelemetryTags { get; set; }
    public DbSet<Satellite> Satellites { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // 1. Configurando a herança (Table Per Hierarchy - TPH)
        modelBuilder.Entity<SpaceEquipment>()
            .HasDiscriminator<string>("EquipmentType")
            .HasValue<BiotelemetryTag>("TagBiotelemetria")
            .HasValue<Satellite>("SateliteLEO");

        // 2. Definindo restrições e chaves primárias explicitamente (Boa prática)
        modelBuilder.Entity<PredictionHistory>()
            .HasKey(p => p.Id);

        modelBuilder.Entity<PredictionHistory>()
            .Property(p => p.SpeciesId)
            .IsRequired()
            .HasMaxLength(100);
    }
}
