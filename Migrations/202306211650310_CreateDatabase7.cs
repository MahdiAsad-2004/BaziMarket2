namespace BaziMarket2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateDatabase7 : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.T_Discount", name: "CategoryId", newName: "Category_Id");
            RenameIndex(table: "dbo.T_Discount", name: "IX_CategoryId", newName: "IX_Category_Id");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.T_Discount", name: "IX_Category_Id", newName: "IX_CategoryId");
            RenameColumn(table: "dbo.T_Discount", name: "Category_Id", newName: "CategoryId");
        }
    }
}
