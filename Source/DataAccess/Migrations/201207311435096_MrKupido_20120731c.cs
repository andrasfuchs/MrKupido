namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MrKupido_20120731c : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.FilterItems", "Element_FilterItemId", "dbo.FilterItems");
            DropForeignKey("dbo.FilterItems", "Ingredient_FilterItemId", "dbo.FilterItems");
            DropForeignKey("dbo.FilterItems", "Recipe_FilterItemId", "dbo.FilterItems");
            DropIndex("dbo.FilterItems", new[] { "Element_FilterItemId" });
            DropIndex("dbo.FilterItems", new[] { "Ingredient_FilterItemId" });
            DropIndex("dbo.FilterItems", new[] { "Recipe_FilterItemId" });
            CreateTable(
                "dbo.IngredientNutritions",
                c => new
                    {
                        IngredientNutritionId = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.IngredientNutritionId);
            
            CreateTable(
                "dbo.Nutritions",
                c => new
                    {
                        NutritionId = c.Int(nullable: false, identity: true),
                        Amount = c.Single(nullable: false),
                    })
                .PrimaryKey(t => t.NutritionId);
            
            CreateTable(
                "dbo.RecipeNutritions",
                c => new
                    {
                        RecipeNutritionId = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.RecipeNutritionId);
            
            DropColumn("dbo.FilterItems", "NutritionId");
            DropColumn("dbo.FilterItems", "Amount");
            DropColumn("dbo.FilterItems", "IngredientNutritionId");
            DropColumn("dbo.FilterItems", "RecipeNutritionId");
            DropColumn("dbo.FilterItems", "Element_FilterItemId");
            DropColumn("dbo.FilterItems", "Ingredient_FilterItemId");
            DropColumn("dbo.FilterItems", "Recipe_FilterItemId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.FilterItems", "Recipe_FilterItemId", c => c.Int());
            AddColumn("dbo.FilterItems", "Ingredient_FilterItemId", c => c.Int());
            AddColumn("dbo.FilterItems", "Element_FilterItemId", c => c.Int());
            AddColumn("dbo.FilterItems", "RecipeNutritionId", c => c.Int());
            AddColumn("dbo.FilterItems", "IngredientNutritionId", c => c.Int());
            AddColumn("dbo.FilterItems", "Amount", c => c.Single());
            AddColumn("dbo.FilterItems", "NutritionId", c => c.Int());
            DropTable("dbo.RecipeNutritions");
            DropTable("dbo.Nutritions");
            DropTable("dbo.IngredientNutritions");
            CreateIndex("dbo.FilterItems", "Recipe_FilterItemId");
            CreateIndex("dbo.FilterItems", "Ingredient_FilterItemId");
            CreateIndex("dbo.FilterItems", "Element_FilterItemId");
            AddForeignKey("dbo.FilterItems", "Recipe_FilterItemId", "dbo.FilterItems", "FilterItemId");
            AddForeignKey("dbo.FilterItems", "Ingredient_FilterItemId", "dbo.FilterItems", "FilterItemId");
            AddForeignKey("dbo.FilterItems", "Element_FilterItemId", "dbo.FilterItems", "FilterItemId");
        }
    }
}
