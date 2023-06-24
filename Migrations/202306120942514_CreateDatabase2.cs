namespace BaziMarket2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateDatabase2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.T_Description", "ProductId", "dbo.T_Product");
            DropForeignKey("dbo.T_Property", "ProductId", "dbo.T_Product");
            DropForeignKey("dbo.T_Specification", "ProductId", "dbo.T_Product");
            AddForeignKey("dbo.T_Description", "ProductId", "dbo.T_Product", "Id", cascadeDelete: true);
            AddForeignKey("dbo.T_Property", "ProductId", "dbo.T_Product", "Id", cascadeDelete: true);
            AddForeignKey("dbo.T_Specification", "ProductId", "dbo.T_Product", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.T_Specification", "ProductId", "dbo.T_Product");
            DropForeignKey("dbo.T_Property", "ProductId", "dbo.T_Product");
            DropForeignKey("dbo.T_Description", "ProductId", "dbo.T_Product");
            AddForeignKey("dbo.T_Specification", "ProductId", "dbo.T_Product", "Id");
            AddForeignKey("dbo.T_Property", "ProductId", "dbo.T_Product", "Id");
            AddForeignKey("dbo.T_Description", "ProductId", "dbo.T_Product", "Id");
        }
    }
}
