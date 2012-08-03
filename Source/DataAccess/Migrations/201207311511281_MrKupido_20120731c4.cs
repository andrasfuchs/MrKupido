namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MrKupido_20120731c4 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Nutritions", "Recipe_FilterItemId", c => c.Int());
            //AddForeignKey("dbo.Nutritions", "Recipe_FilterItemId", "dbo.FilterItems", "FilterItemId", cascadeDelete: true);
            CreateIndex("dbo.Nutritions", "Recipe_FilterItemId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Nutritions", new[] { "Recipe_FilterItemId" });
            //DropForeignKey("dbo.Nutritions", "Recipe_FilterItemId", "dbo.FilterItems");
            DropColumn("dbo.Nutritions", "Recipe_FilterItemId");
        }
    }
}
