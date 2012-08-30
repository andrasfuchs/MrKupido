namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MrKupido_20120830c : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ImportedRecipes", "Language", c => c.String(nullable: false, maxLength: 3));
            AlterColumn("dbo.ImportedRecipes", "UniqueName", c => c.String(nullable: false, maxLength: 150));
            AlterColumn("dbo.ImportedRecipes", "DisplayName", c => c.String(nullable: false, maxLength: 150));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ImportedRecipes", "DisplayName", c => c.String(nullable: false));
            AlterColumn("dbo.ImportedRecipes", "UniqueName", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.ImportedRecipes", "Language", c => c.String(nullable: false));
        }
    }
}
