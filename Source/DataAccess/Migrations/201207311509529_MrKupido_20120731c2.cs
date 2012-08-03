namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MrKupido_20120731c2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Nutritions", "Element_FilterItemId", c => c.Int(nullable: false));
            AddForeignKey("dbo.Nutritions", "Element_FilterItemId", "dbo.FilterItems", "FilterItemId", cascadeDelete: true);
            CreateIndex("dbo.Nutritions", "Element_FilterItemId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Nutritions", new[] { "Element_FilterItemId" });
            DropForeignKey("dbo.Nutritions", "Element_FilterItemId", "dbo.FilterItems");
            DropColumn("dbo.Nutritions", "Element_FilterItemId");
        }
    }
}
