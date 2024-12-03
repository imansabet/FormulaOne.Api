﻿using FormulaOne.Entities.DbSet;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System.Security.Cryptography.X509Certificates;

namespace FormulaOne.DataService.Data;

public class AppDbContext : DbContext
{
    // Define Db Entities
    public virtual DbSet<Driver> Drivers { get; set; }
    public virtual DbSet<Achievement> Achievements { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
    {}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        // Specified RelationShip between entities
        modelBuilder.Entity<Achievement>(entity =>
        {
            entity.HasOne(d => d.Driver)
            .WithMany(p => p.Achievements)
            .HasForeignKey(d => d.DriverId)
            .OnDelete(DeleteBehavior.NoAction)
            .HasConstraintName("FK_Achievements_Driver");
        });
    }





}
