namespace FoodNinja.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initialmigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Invites",
                c => new
                    {
                        InviteId = c.Int(nullable: false, identity: true),
                        TeamId = c.Int(nullable: false),
                        Token = c.String(),
                    })
                .PrimaryKey(t => t.InviteId)
                .ForeignKey("dbo.Teams", t => t.TeamId, cascadeDelete: true)
                .Index(t => t.TeamId);
            
            CreateTable(
                "dbo.Teams",
                c => new
                    {
                        TeamId = c.Int(nullable: false, identity: true),
                        TeamName = c.String(),
                        Address1 = c.String(),
                        Address2 = c.String(),
                        Address3 = c.String(),
                        City = c.String(),
                        State = c.String(),
                        PostCode = c.String(),
                        Telephone = c.String(),
                        CreatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.TeamId);
            
            CreateTable(
                "dbo.NinjaUsers",
                c => new
                    {
                        NinjaUserId = c.Int(nullable: false, identity: true),
                        TeamId = c.Int(nullable: false),
                        UserName = c.String(),
                        FirstName = c.String(),
                        LastName = c.String(),
                        EmailAddress = c.String(),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        CreatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.NinjaUserId)
                .ForeignKey("dbo.Teams", t => t.TeamId, cascadeDelete: true)
                .Index(t => t.TeamId);
            
            CreateTable(
                "dbo.Participations",
                c => new
                    {
                        ParticipationId = c.Int(nullable: false, identity: true),
                        OrderId = c.Int(nullable: false),
                        RestaurantLocationId = c.Int(nullable: false),
                        NinjaUserId = c.Int(nullable: false),
                        FavoriteChoice = c.Boolean(nullable: false),
                        Selection = c.String(),
                        CreatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ParticipationId)
                .ForeignKey("dbo.RestaurantOptions", t => new { t.OrderId, t.RestaurantLocationId }, cascadeDelete: true)
                .ForeignKey("dbo.NinjaUsers", t => t.NinjaUserId, cascadeDelete: true)
                .Index(t => new { t.OrderId, t.RestaurantLocationId })
                .Index(t => t.NinjaUserId);
            
            CreateTable(
                "dbo.RestaurantOptions",
                c => new
                    {
                        OrderId = c.Int(nullable: false),
                        RestaurantLocationId = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => new { t.OrderId, t.RestaurantLocationId })
                .ForeignKey("dbo.Orders", t => t.OrderId, cascadeDelete: true)
                .ForeignKey("dbo.RestaurantLocations", t => t.RestaurantLocationId, cascadeDelete: true)
                .Index(t => t.OrderId)
                .Index(t => t.RestaurantLocationId);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        OrderId = c.Int(nullable: false, identity: true),
                        TeamId = c.Int(nullable: false),
                        NinjaUserId = c.Int(nullable: false),
                        OrderName = c.String(),
                        CreatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.OrderId)
                .ForeignKey("dbo.Teams", t => t.TeamId, cascadeDelete: true)
                .Index(t => t.TeamId);
            
            CreateTable(
                "dbo.RestaurantLocations",
                c => new
                    {
                        RestaurantLocationId = c.Int(nullable: false, identity: true),
                        RestaurantId = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        Address1 = c.String(),
                        Address2 = c.String(),
                        Address3 = c.String(),
                        City = c.String(),
                        State = c.String(),
                        PostCode = c.String(),
                        Telephone = c.String(),
                    })
                .PrimaryKey(t => t.RestaurantLocationId)
                .ForeignKey("dbo.Restaurants", t => t.RestaurantId, cascadeDelete: true)
                .Index(t => t.RestaurantId);
            
            CreateTable(
                "dbo.Restaurants",
                c => new
                    {
                        RestaurantId = c.Int(nullable: false, identity: true),
                        CreatedDate = c.DateTime(nullable: false),
                        Name = c.String(),
                        Description = c.String(),
                        URL = c.String(),
                    })
                .PrimaryKey(t => t.RestaurantId);
            
            CreateTable(
                "dbo.UserRoles",
                c => new
                    {
                        UserId = c.Int(nullable: false),
                        RoleId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.Roles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.NinjaUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Orders", "TeamId", "dbo.Teams");
            DropForeignKey("dbo.NinjaUsers", "TeamId", "dbo.Teams");
            DropForeignKey("dbo.UserRoles", "UserId", "dbo.NinjaUsers");
            DropForeignKey("dbo.UserRoles", "RoleId", "dbo.Roles");
            DropForeignKey("dbo.Participations", "NinjaUserId", "dbo.NinjaUsers");
            DropForeignKey("dbo.RestaurantOptions", "RestaurantLocationId", "dbo.RestaurantLocations");
            DropForeignKey("dbo.RestaurantLocations", "RestaurantId", "dbo.Restaurants");
            DropForeignKey("dbo.Participations", new[] { "OrderId", "RestaurantLocationId" }, "dbo.RestaurantOptions");
            DropForeignKey("dbo.RestaurantOptions", "OrderId", "dbo.Orders");
            DropForeignKey("dbo.Invites", "TeamId", "dbo.Teams");
            DropIndex("dbo.UserRoles", new[] { "RoleId" });
            DropIndex("dbo.UserRoles", new[] { "UserId" });
            DropIndex("dbo.RestaurantLocations", new[] { "RestaurantId" });
            DropIndex("dbo.Orders", new[] { "TeamId" });
            DropIndex("dbo.RestaurantOptions", new[] { "RestaurantLocationId" });
            DropIndex("dbo.RestaurantOptions", new[] { "OrderId" });
            DropIndex("dbo.Participations", new[] { "NinjaUserId" });
            DropIndex("dbo.Participations", new[] { "OrderId", "RestaurantLocationId" });
            DropIndex("dbo.NinjaUsers", new[] { "TeamId" });
            DropIndex("dbo.Invites", new[] { "TeamId" });
            DropTable("dbo.Roles");
            DropTable("dbo.UserRoles");
            DropTable("dbo.Restaurants");
            DropTable("dbo.RestaurantLocations");
            DropTable("dbo.Orders");
            DropTable("dbo.RestaurantOptions");
            DropTable("dbo.Participations");
            DropTable("dbo.NinjaUsers");
            DropTable("dbo.Teams");
            DropTable("dbo.Invites");
        }
    }
}
