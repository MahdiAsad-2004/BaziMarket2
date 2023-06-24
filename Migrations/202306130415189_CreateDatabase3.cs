namespace BaziMarket2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateDatabase3 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.T_Discount", "MaxPrice", c => c.Int(nullable: false));
            DropColumn("dbo.T_Discount", "ProductId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.T_Discount", "ProductId", c => c.Int());
            AlterColumn("dbo.T_Discount", "MaxPrice", c => c.Int());
        }
    }
}
