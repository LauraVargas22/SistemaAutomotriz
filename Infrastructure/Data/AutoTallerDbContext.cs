using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Reflection;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Interceptors;

namespace Infrastructure.Data
{
    public class AutoTallerDbContext : DbContext
    {
        private readonly AuditInterceptor _auditInterceptor;
        public AutoTallerDbContext(DbContextOptions<AutoTallerDbContext> options, AuditInterceptor auditInterceptor) : base(options)
        {
            _auditInterceptor = auditInterceptor;
        }

        public DbSet<Auditory> Auditory { get; set; }
        public DbSet<Client> Client { get; set; }
        public DbSet<DetailInspection> DetaillInspection { get; set; }
        public DbSet<Diagnostic> Diagnostic { get; set; }
        public DbSet<Inspection> Inspection { get; set; }
        public DbSet<Invoice> Invoice { get; set; }
        public DbSet<OrderDetails> OrderDetails { get; set; }
        public DbSet<Rol> Rol { get; set; }
        public DbSet<ServiceOrder> ServiceOrder { get; set; }
        public DbSet<SparePart> SparePart { get; set; }
        public DbSet<Specialization> Specialization { get; set; }
        public DbSet<State> State { get; set; }
        public DbSet<TypeService> TypeService { get; set; }
        public DbSet<TypeVehicle> TypeVehicles { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<UserRol> UserRole { get; set; }
        public DbSet<UserSpecialization> UserSpecialization { get; set; }
        public DbSet<Vehicle> Vehicle { get; set; }
        public DbSet<DetailsDiagnostic> DetailsDiagnostics { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            modelBuilder.Entity<UserRol>().ToTable("user_roles");
            modelBuilder.Entity<UserRol>()
                .HasKey(ur => new { ur.UserId, ur.RolId });

            modelBuilder.Entity<UserRol>()
                .HasOne(ur => ur.User)
                .WithMany(u => u.UserRoles)
                .HasForeignKey(ur => ur.UserId);

            modelBuilder.Entity<UserRol>()
                .HasOne(ur => ur.Rol)
                .WithMany(r => r.UserRoles)
                .HasForeignKey(ur => ur.RolId);

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.AddInterceptors(_auditInterceptor);
        }
        
    }
}