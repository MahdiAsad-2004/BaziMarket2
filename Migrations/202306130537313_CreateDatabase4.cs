namespace BaziMarket2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateDatabase4 : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.T_Discount", "CategoryId");
            AddForeignKey("dbo.T_Discount", "CategoryId", "dbo.T_Category", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.T_Discount", "CategoryId", "dbo.T_Category");
            DropIndex("dbo.T_Discount", new[] { "CategoryId" });
        }
    }
}
