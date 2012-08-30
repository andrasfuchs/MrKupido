namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MrKupido_20120830d : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ImportedRecipes", "OriginalDirections", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ImportedRecipes", "OriginalDirections", c => c.String());
        }
    }
}
