namespace BaziMarket2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateDatabase1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.T_Answer",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Text = c.String(nullable: false, maxLength: 100),
                        RegisterDate = c.DateTime(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        UserId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.T_Question", t => t.Id, cascadeDelete: true)
                .ForeignKey("dbo.T_User", t => t.UserId)
                .Index(t => t.Id)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.T_Question",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Text = c.String(nullable: false, maxLength: 100),
                        RegisterDate = c.DateTime(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        ProductId = c.Int(nullable: false),
                        UserId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.T_Product", t => t.ProductId, cascadeDelete: true)
                .ForeignKey("dbo.T_User", t => t.UserId, cascadeDelete: true)
                .Index(t => t.ProductId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.T_Product",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        Price = c.Int(nullable: false),
                        InStockCount = c.Int(nullable: false),
                        Sold = c.Int(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        RegisterDate = c.DateTime(nullable: false),
                        Discount = c.Double(),
                        CategoryId = c.Int(),
                        Image = c.String(maxLength: 100),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.T_Category", t => t.CategoryId)
                .Index(t => t.Name, unique: true)
                .Index(t => t.CategoryId);
            
            CreateTable(
                "dbo.T_Category",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 30),
                        Image = c.String(maxLength: 50, unicode: false),
                        ParentId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.T_Category", t => t.ParentId)
                .Index(t => t.Name, unique: true)
                .Index(t => t.ParentId);
            
            CreateTable(
                "dbo.T_Coment",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Rate = c.Int(nullable: false),
                        Text = c.String(nullable: false, maxLength: 100),
                        RegisterDate = c.DateTime(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        UserId = c.Int(nullable: false),
                        ProductId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.T_Product", t => t.ProductId, cascadeDelete: true)
                .ForeignKey("dbo.T_User", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.ProductId);
            
            CreateTable(
                "dbo.T_User",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Username = c.String(nullable: false, maxLength: 30),
                        Password = c.String(nullable: false, maxLength: 64, unicode: false),
                        FirstName = c.String(nullable: false, maxLength: 30),
                        LastName = c.String(nullable: false, maxLength: 30),
                        RegisterDate = c.DateTime(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        ProfileImage = c.String(maxLength: 50, unicode: false),
                        Address = c.String(maxLength: 200),
                        RoleId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.T_Role", t => t.RoleId)
                .Index(t => t.Username, unique: true)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.T_Cart",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Cost = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.T_User", t => t.Id, cascadeDelete: true)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.T_ProductCart",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Number = c.Int(nullable: false),
                        ProductId = c.Int(),
                        CartId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.T_Cart", t => t.CartId, cascadeDelete: true)
                .ForeignKey("dbo.T_Product", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.ProductId)
                .Index(t => t.CartId);
            
            CreateTable(
                "dbo.T_Role",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 20),
                        Title = c.String(nullable: false, maxLength: 20),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true);
            
            CreateTable(
                "dbo.T_Description",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 30),
                        Text = c.String(storeType: "ntext"),
                        Image = c.String(maxLength: 50, unicode: false),
                        ProductId = c.Int(),
                        Index = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.T_Product", t => t.ProductId)
                .Index(t => t.ProductId);
            
            CreateTable(
                "dbo.T_Picture",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 50, unicode: false),
                        Index = c.Int(),
                        ProductId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.T_Product", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.ProductId);
            
            CreateTable(
                "dbo.T_Property",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Text = c.String(nullable: false, maxLength: 30),
                        ProductId = c.Int(),
                        Index = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.T_Product", t => t.ProductId)
                .Index(t => t.ProductId);
            
            CreateTable(
                "dbo.T_Specification",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 30),
                        Text = c.String(nullable: false, maxLength: 100),
                        ProductId = c.Int(),
                        Index = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.T_Product", t => t.ProductId)
                .Index(t => t.ProductId);
            
            CreateTable(
                "dbo.T_Discount",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Code = c.String(nullable: false, maxLength: 30),
                        Percent = c.Double(nullable: false),
                        MaxPrice = c.Int(),
                        ProductId = c.Int(),
                        CategoryId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Code, unique: true);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.T_Answer", "UserId", "dbo.T_User");
            DropForeignKey("dbo.T_Answer", "Id", "dbo.T_Question");
            DropForeignKey("dbo.T_Question", "UserId", "dbo.T_User");
            DropForeignKey("dbo.T_Question", "ProductId", "dbo.T_Product");
            DropForeignKey("dbo.T_Specification", "ProductId", "dbo.T_Product");
            DropForeignKey("dbo.T_Property", "ProductId", "dbo.T_Product");
            DropForeignKey("dbo.T_Picture", "ProductId", "dbo.T_Product");
            DropForeignKey("dbo.T_Description", "ProductId", "dbo.T_Product");
            DropForeignKey("dbo.T_Coment", "UserId", "dbo.T_User");
            DropForeignKey("dbo.T_User", "RoleId", "dbo.T_Role");
            DropForeignKey("dbo.T_Cart", "Id", "dbo.T_User");
            DropForeignKey("dbo.T_ProductCart", "ProductId", "dbo.T_Product");
            DropForeignKey("dbo.T_ProductCart", "CartId", "dbo.T_Cart");
            DropForeignKey("dbo.T_Coment", "ProductId", "dbo.T_Product");
            DropForeignKey("dbo.T_Product", "CategoryId", "dbo.T_Category");
            DropForeignKey("dbo.T_Category", "ParentId", "dbo.T_Category");
            DropIndex("dbo.T_Discount", new[] { "Code" });
            DropIndex("dbo.T_Specification", new[] { "ProductId" });
            DropIndex("dbo.T_Property", new[] { "ProductId" });
            DropIndex("dbo.T_Picture", new[] { "ProductId" });
            DropIndex("dbo.T_Description", new[] { "ProductId" });
            DropIndex("dbo.T_Role", new[] { "Name" });
            DropIndex("dbo.T_ProductCart", new[] { "CartId" });
            DropIndex("dbo.T_ProductCart", new[] { "ProductId" });
            DropIndex("dbo.T_Cart", new[] { "Id" });
            DropIndex("dbo.T_User", new[] { "RoleId" });
            DropIndex("dbo.T_User", new[] { "Username" });
            DropIndex("dbo.T_Coment", new[] { "ProductId" });
            DropIndex("dbo.T_Coment", new[] { "UserId" });
            DropIndex("dbo.T_Category", new[] { "ParentId" });
            DropIndex("dbo.T_Category", new[] { "Name" });
            DropIndex("dbo.T_Product", new[] { "CategoryId" });
            DropIndex("dbo.T_Product", new[] { "Name" });
            DropIndex("dbo.T_Question", new[] { "UserId" });
            DropIndex("dbo.T_Question", new[] { "ProductId" });
            DropIndex("dbo.T_Answer", new[] { "UserId" });
            DropIndex("dbo.T_Answer", new[] { "Id" });
            DropTable("dbo.T_Discount");
            DropTable("dbo.T_Specification");
            DropTable("dbo.T_Property");
            DropTable("dbo.T_Picture");
            DropTable("dbo.T_Description");
            DropTable("dbo.T_Role");
            DropTable("dbo.T_ProductCart");
            DropTable("dbo.T_Cart");
            DropTable("dbo.T_User");
            DropTable("dbo.T_Coment");
            DropTable("dbo.T_Category");
            DropTable("dbo.T_Product");
            DropTable("dbo.T_Question");
            DropTable("dbo.T_Answer");
        }
    }
}
