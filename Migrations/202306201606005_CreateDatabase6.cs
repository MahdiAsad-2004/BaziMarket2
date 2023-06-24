namespace BaziMarket2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateDatabase6 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.T_Product", "CategoryId", "dbo.T_Category");
            AddForeignKey("dbo.T_Product", "CategoryId", "dbo.T_Category", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.T_Product", "CategoryId", "dbo.T_Category");
            AddForeignKey("dbo.T_Product", "CategoryId", "dbo.T_Category", "Id");
        }
    }
}
