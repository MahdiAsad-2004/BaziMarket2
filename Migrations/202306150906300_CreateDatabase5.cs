namespace BaziMarket2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateDatabase5 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserProducts",
                c => new
                    {
                        User_Id = c.Int(nullable: false),
                        Product_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.User_Id, t.Product_Id })
                .ForeignKey("dbo.T_User", t => t.User_Id, cascadeDelete: true)
                .ForeignKey("dbo.T_Product", t => t.Product_Id, cascadeDelete: true)
                .Index(t => t.User_Id)
                .Index(t => t.Product_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserProducts", "Product_Id", "dbo.T_Product");
            DropForeignKey("dbo.UserProducts", "User_Id", "dbo.T_User");
            DropIndex("dbo.UserProducts", new[] { "Product_Id" });
            DropIndex("dbo.UserProducts", new[] { "User_Id" });
            DropTable("dbo.UserProducts");
        }
    }
}
