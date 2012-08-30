namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MrKupido_20120801a : DbMigration
    {
        public override void Up()
        {
            //AlterColumn("dbo.FilterItems", "PreparationTime", c => c.Int());
            //AlterColumn("dbo.FilterItems", "CookingTime", c => c.Int());
            //AlterColumn("dbo.FilterItems", "TotalTime", c => c.Int());
            //AlterColumn("dbo.FilterItems", "ExpirationTime", c => c.Int());

            DropColumn("dbo.FilterItems", "PreparationTime");
            DropColumn("dbo.FilterItems", "CookingTime");
            DropColumn("dbo.FilterItems", "TotalTime");
            DropColumn("dbo.FilterItems", "ExpirationTime");

            AddColumn("dbo.FilterItems", "PreparationTime", c => c.Int());
            AddColumn("dbo.FilterItems", "CookingTime", c => c.Int());
            AddColumn("dbo.FilterItems", "TotalTime", c => c.Int());
            AddColumn("dbo.FilterItems", "ExpirationTime", c => c.Int());

            DropColumn("dbo.FilterItems", "ElementId");
            DropColumn("dbo.FilterItems", "RecipeId");
            DropColumn("dbo.FilterItems", "IngredientId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.FilterItems", "IngredientId", c => c.Int());
            AddColumn("dbo.FilterItems", "RecipeId", c => c.Int());
            AddColumn("dbo.FilterItems", "ElementId", c => c.Int());

            DropColumn("dbo.FilterItems", "PreparationTime");
            DropColumn("dbo.FilterItems", "CookingTime");
            DropColumn("dbo.FilterItems", "TotalTime");
            DropColumn("dbo.FilterItems", "ExpirationTime");

            AddColumn("dbo.FilterItems", "PreparationTime", c => c.Time());
            AddColumn("dbo.FilterItems", "CookingTime", c => c.Time());
            AddColumn("dbo.FilterItems", "TotalTime", c => c.Time());
            AddColumn("dbo.FilterItems", "ExpirationTime", c => c.Time());
            
            //AlterColumn("dbo.FilterItems", "ExpirationTime", c => c.Time());
            //AlterColumn("dbo.FilterItems", "TotalTime", c => c.Time());
            //AlterColumn("dbo.FilterItems", "CookingTime", c => c.Time());
            //AlterColumn("dbo.FilterItems", "PreparationTime", c => c.Time());
        }
    }
}
