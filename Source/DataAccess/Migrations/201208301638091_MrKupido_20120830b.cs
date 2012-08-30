namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MrKupido_20120830b : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ImportedRecipes", "Language", c => c.String(nullable: false));
            AlterColumn("dbo.ImportedRecipes", "UniqueName", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.ImportedRecipes", "DisplayName", c => c.String(nullable: false));
            AlterColumn("dbo.ImportedRecipes", "Ingredients", c => c.String(nullable: false));
            AlterColumn("dbo.ImportedRecipes", "Directions", c => c.String(nullable: false));

            CreateIndex("dbo.ImportedRecipes", new string[1] { "UniqueName" }, true, "IX_ImportedRecipes_UniqueName");
        }
        
        public override void Down()
        {
            DropIndex("dbo.ImportedRecipes", "IX_ImportedRecipes_UniqueName");

            AlterColumn("dbo.ImportedRecipes", "Directions", c => c.String());
            AlterColumn("dbo.ImportedRecipes", "Ingredients", c => c.String());
            AlterColumn("dbo.ImportedRecipes", "DisplayName", c => c.String());
            AlterColumn("dbo.ImportedRecipes", "UniqueName", c => c.String());
            AlterColumn("dbo.ImportedRecipes", "Language", c => c.String());
        }
    }
}
