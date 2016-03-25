using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using FoodNinja.Core.Domain;

namespace FoodNinja.Data.Infrastructure
{
    public class NinjaDataContext : DbContext
    {
        public NinjaDataContext() : base("Ninja")
        {
            var ensureDLLIsCopied = System.Data.Entity.SqlServer.SqlProviderServices.Instance;
        }

        public IDbSet<Team> Teams { get; set; }
        public IDbSet<Invite> Invites { get; set; }
        public IDbSet<NinjaUser> NinjaUsers { get; set; }
        public IDbSet<Order> Orders { get; set; }
        public IDbSet<Participation> Participations { get; set; }
        public IDbSet<Restaurant> Restaurants { get; set; }
        public IDbSet<RestaurantLocation> RestaurantLocations { get; set; }
        public IDbSet<RestaurantOption> RestaurantOptions { get; set; }
        public IDbSet<Role> Roles { get; set; }
        public IDbSet<UserRole> UserRoles { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Team>()
                .HasMany(g => g.Invites)
                .WithRequired(i => i.Team)
                .HasForeignKey(i => i.TeamId);

            modelBuilder.Entity<Team>()
                .HasMany(g => g.NinjaUsers)
                .WithRequired(nu => nu.Team)
                .HasForeignKey(nu => nu.TeamId);

            modelBuilder.Entity<Team>()
                .HasMany(g => g.Orders)
                .WithRequired(o => o.Team)
                .HasForeignKey(o => o.TeamId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<NinjaUser>()
                .HasMany(n => n.Participations)
                .WithRequired(p => p.NinjaUser)
                .HasForeignKey(p => p.NinjaUserId);
            // .WillCascadeOnDelete(false);

            //modelBuilder.Entity<Order>()
            //    .HasMany(o => o.Participations)
            //    .WithRequired(p => p.Order)
            //    .HasForeignKey(p => p.OrderId);

            modelBuilder.Entity<Order>()
                .HasMany(o => o.RestaurantOptions)
                .WithRequired(ro => ro.Order)
                .HasForeignKey(ro => ro.OrderId);

            modelBuilder.Entity<Restaurant>()
                .HasMany(r => r.RestaurantLocations)
                .WithRequired(rl => rl.Restaurant)
                .HasForeignKey(rl => rl.RestaurantId);

            modelBuilder.Entity<RestaurantLocation>()
                .HasMany(rl => rl.RestaurantOptions)
                .WithRequired(ro => ro.RestaurantLocation)
                .HasForeignKey(ro => ro.RestaurantLocationId);            

            //TODO: Configure relationships for Users/Roles/Etc.
            modelBuilder.Entity<NinjaUser>().Property(p => p.Id).HasColumnName("NinjaUserId");

            modelBuilder.Entity<NinjaUser>()
                .HasMany(u => u.UserRoles)
                .WithRequired(ur => ur.NinjaUser)
                .HasForeignKey(ur => ur.UserId);

            modelBuilder.Entity<Role>()
                .HasMany(r => r.UserRoles)
                .WithRequired(ur => ur.Role)
                .HasForeignKey(ur => ur.RoleId);

            modelBuilder.Entity<RestaurantOption>()
                .HasKey(ro => new { ro.OrderId, ro.RestaurantLocationId });

            modelBuilder.Entity<UserRole>().HasKey(u => new { u.UserId, u.RoleId });
        }

    }

}