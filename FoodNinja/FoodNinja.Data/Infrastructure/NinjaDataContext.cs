using System.FoodNinja.Core.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace FoodNinja.Data.Infrastructure
{
    public class NinjaDataContext : DbContext
    {
        public NinjaDataContext() : base("Ninja")
        {
            var ensureDLLIsCopied = System.Data.Entity.SqlServer.SqlProviderServices.Instance;
        }

        public IDbSet<Group> Groups { get; set; }
        public IDbSet<Invite> Invites { get; set; }
        public IDbSet<NinjaUser> NinjaUsers { get; set; }
        public IDbSet<Order> Orders { get; set; }
        public IDbSet<Participation> Participations { get; set; }
        public IDbSet<Restaurant> Restaurants { get; set; }
        public IDbSet<RestaurantLocation> RestaurantLocations { get; set; }
        public IDbSet<RestaurantOrder> RestaurantOrders { get; set; }
        public IDbSet<Role> Roles { get; set; }
        public IDbSet<UserRole> UserRoles { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Group>()
                .HasMany(g => g.Invites)
                .WithRequired(i => i.Group)
                .HasForeignKey(i => i.GroupId);

            modelBuilder.Entity<Group>()
                .HasMany(g => g.NinjaUsers)
                .WithRequired(nu => nu.Group)
                .HasForeignKey(nu => nu.GroupId);

            modelBuilder.Entity<Group>()
                .HasMany(g => g.Orders)
                .WithRequired(o => o.Group)
                .HasForeignKey(o => o.GroupId); 

            modelBuilder.Entity<NinjaUser>()
                .HasMany(n => n.Participations)
                .WithRequired(p => p.NinjaUser)
                .HasForeignKey(p => p.NinjaUserId);
            // .WillCascadeOnDelete(false);

            modelBuilder.Entity<Order>()
                .HasMany(o => o.Participations)
                .WithRequired(p => p.Order)
                .HasForeignKey(p => p.OrderId);

            modelBuilder.Entity<Order>()
                .HasMany(o => o.RestaurantOrder)
                .WithRequired(ro => ro.Order)
                .HasForeignKey(ro => ro.OrderId);

            modelBuilder.Entity<Restaurant>()
                .HasMany(r => r.RestaurantLocation)
                .WithRequired(rl => rl.Restaurant)
                .HasForeignKey(rl => rl.RestaurantId);

            modelBuilder.Entity<RestaurantLocation>()
                .HasMany(rl => rl.RestaurantOrder)
                .WithRequired(ro => ro.RestaurantLocation)
                .HasForeignKey(ro => ro.RestaurantLocationId);            

            //TODO: Configure relationships for Users/Roles/Etc.
            modelBuilder.Entity<NinjaUser>().Property(p => p.Id).HasColumnName("NinjaUserId");

            modelBuilder.Entity<NinjaUser>()
                .HasMany(u => u.UserRoles)
                .WithRequired(ur => ur.NinjaUser)
                .HasForeignKey(ur => ur.NinjaUserId);

            modelBuilder.Entity<Role>()
                .HasMany(r => r.UserRoles)
                .WithRequired(ur => ur.Role)
                .HasForeignKey(ur => ur.RoleId);

            modelBuilder.Entity<UserRole>().HasKey(u => new { u.UserId, u.RoleId });
        }

    }

}