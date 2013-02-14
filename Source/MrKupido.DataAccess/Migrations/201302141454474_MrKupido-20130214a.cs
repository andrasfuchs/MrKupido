namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MrKupido20130214a : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Nutritions", "Element_FilterItemId", "dbo.FilterItems");
            DropForeignKey("dbo.IngredientPrices", "Ingredient_FilterItemId", "dbo.FilterItems");
            DropForeignKey("dbo.IngredientPrices", "Supplier_SupplierId", "dbo.Suppliers");
            DropForeignKey("dbo.IngredientPrices", "Currency_CurrencyId", "dbo.Currencies");
            DropForeignKey("dbo.IngredientRatings", "User_UserId", "dbo.Users");
            DropForeignKey("dbo.IngredientRatings", "Ingredient_FilterItemId", "dbo.FilterItems");
            DropForeignKey("dbo.RecipeRatings", "User_UserId", "dbo.Users");
            DropForeignKey("dbo.RecipeRatings", "Recipe_FilterItemId", "dbo.FilterItems");
            DropForeignKey("dbo.RecipeTags", "Recipe_FilterItemId", "dbo.FilterItems");
            DropForeignKey("dbo.RecipeTags", "AssignedBy_UserId", "dbo.Users");
            DropIndex("dbo.Nutritions", new[] { "Element_FilterItemId" });
            DropIndex("dbo.IngredientPrices", new[] { "Ingredient_FilterItemId" });
            DropIndex("dbo.IngredientPrices", new[] { "Supplier_SupplierId" });
            DropIndex("dbo.IngredientPrices", new[] { "Currency_CurrencyId" });
            DropIndex("dbo.IngredientRatings", new[] { "User_UserId" });
            DropIndex("dbo.IngredientRatings", new[] { "Ingredient_FilterItemId" });
            DropIndex("dbo.RecipeRatings", new[] { "User_UserId" });
            DropIndex("dbo.RecipeRatings", new[] { "Recipe_FilterItemId" });
            DropIndex("dbo.RecipeTags", new[] { "Recipe_FilterItemId" });
            DropIndex("dbo.RecipeTags", new[] { "AssignedBy_UserId" });
            DropColumn("dbo.FilterItems", "Category");
            DropColumn("dbo.FilterItems", "Unit");
            DropColumn("dbo.FilterItems", "ServingTemperature");
            DropColumn("dbo.FilterItems", "PreparationTime");
            DropColumn("dbo.FilterItems", "CookingTime");
            DropColumn("dbo.FilterItems", "TotalTime");
            DropColumn("dbo.FilterItems", "EstimatedPrice");
            DropColumn("dbo.FilterItems", "EstimatedPriceDate");
            DropColumn("dbo.FilterItems", "Rating");
            DropColumn("dbo.FilterItems", "RatingCount");
            DropColumn("dbo.FilterItems", "RatingDate");
            DropColumn("dbo.FilterItems", "Discriminator");
            DropTable("dbo.Conditions");
            DropTable("dbo.Nutritions");
            DropTable("dbo.IngredientPrices");
            DropTable("dbo.Suppliers");
            DropTable("dbo.IngredientRatings");
            DropTable("dbo.RecipeRatings");
            DropTable("dbo.RecipeTags");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.RecipeTags",
                c => new
                    {
                        RecipeTagId = c.Int(nullable: false, identity: true),
                        AssignedAt = c.DateTime(nullable: false),
                        Recipe_FilterItemId = c.Int(nullable: false),
                        AssignedBy_UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.RecipeTagId);
            
            CreateTable(
                "dbo.RecipeRatings",
                c => new
                    {
                        RecipeRatingId = c.Int(nullable: false, identity: true),
                        Rating = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false),
                        User_UserId = c.Int(nullable: false),
                        Recipe_FilterItemId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.RecipeRatingId);
            
            CreateTable(
                "dbo.IngredientRatings",
                c => new
                    {
                        IngredientRatingId = c.Int(nullable: false, identity: true),
                        Rating = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false),
                        User_UserId = c.Int(nullable: false),
                        Ingredient_FilterItemId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.IngredientRatingId);
            
            CreateTable(
                "dbo.Suppliers",
                c => new
                    {
                        SupplierId = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.SupplierId);
            
            CreateTable(
                "dbo.IngredientPrices",
                c => new
                    {
                        IngredientPriceId = c.Int(nullable: false, identity: true),
                        Store = c.String(),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Date = c.DateTime(nullable: false),
                        Ingredient_FilterItemId = c.Int(nullable: false),
                        Supplier_SupplierId = c.Int(nullable: false),
                        Currency_CurrencyId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.IngredientPriceId);
            
            CreateTable(
                "dbo.Nutritions",
                c => new
                    {
                        NutritionId = c.Int(nullable: false, identity: true),
                        Amount = c.Single(nullable: false),
                        IngredientNutritionId = c.Int(),
                        RecipeNutritionId = c.Int(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                        Element_FilterItemId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.NutritionId);
            
            CreateTable(
                "dbo.Conditions",
                c => new
                    {
                        ConditionId = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.ConditionId);
            
            AddColumn("dbo.FilterItems", "Discriminator", c => c.String(nullable: false, maxLength: 128));
            AddColumn("dbo.FilterItems", "RatingDate", c => c.DateTime());
            AddColumn("dbo.FilterItems", "RatingCount", c => c.Int());
            AddColumn("dbo.FilterItems", "Rating", c => c.Single());
            AddColumn("dbo.FilterItems", "EstimatedPriceDate", c => c.DateTime());
            AddColumn("dbo.FilterItems", "EstimatedPrice", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("dbo.FilterItems", "TotalTime", c => c.Int());
            AddColumn("dbo.FilterItems", "CookingTime", c => c.Int());
            AddColumn("dbo.FilterItems", "PreparationTime", c => c.Int());
            AddColumn("dbo.FilterItems", "ServingTemperature", c => c.Int());
            AddColumn("dbo.FilterItems", "Unit", c => c.Int());
            AddColumn("dbo.FilterItems", "Category", c => c.Int());
            CreateIndex("dbo.RecipeTags", "AssignedBy_UserId");
            CreateIndex("dbo.RecipeTags", "Recipe_FilterItemId");
            CreateIndex("dbo.RecipeRatings", "Recipe_FilterItemId");
            CreateIndex("dbo.RecipeRatings", "User_UserId");
            CreateIndex("dbo.IngredientRatings", "Ingredient_FilterItemId");
            CreateIndex("dbo.IngredientRatings", "User_UserId");
            CreateIndex("dbo.IngredientPrices", "Currency_CurrencyId");
            CreateIndex("dbo.IngredientPrices", "Supplier_SupplierId");
            CreateIndex("dbo.IngredientPrices", "Ingredient_FilterItemId");
            CreateIndex("dbo.Nutritions", "Element_FilterItemId");
            AddForeignKey("dbo.RecipeTags", "AssignedBy_UserId", "dbo.Users", "UserId", cascadeDelete: true);
            AddForeignKey("dbo.RecipeTags", "Recipe_FilterItemId", "dbo.FilterItems", "FilterItemId", cascadeDelete: true);
            AddForeignKey("dbo.RecipeRatings", "Recipe_FilterItemId", "dbo.FilterItems", "FilterItemId", cascadeDelete: true);
            AddForeignKey("dbo.RecipeRatings", "User_UserId", "dbo.Users", "UserId", cascadeDelete: true);
            AddForeignKey("dbo.IngredientRatings", "Ingredient_FilterItemId", "dbo.FilterItems", "FilterItemId", cascadeDelete: true);
            AddForeignKey("dbo.IngredientRatings", "User_UserId", "dbo.Users", "UserId", cascadeDelete: true);
            AddForeignKey("dbo.IngredientPrices", "Currency_CurrencyId", "dbo.Currencies", "CurrencyId", cascadeDelete: true);
            AddForeignKey("dbo.IngredientPrices", "Supplier_SupplierId", "dbo.Suppliers", "SupplierId", cascadeDelete: true);
            AddForeignKey("dbo.IngredientPrices", "Ingredient_FilterItemId", "dbo.FilterItems", "FilterItemId", cascadeDelete: true);
            AddForeignKey("dbo.Nutritions", "Element_FilterItemId", "dbo.FilterItems", "FilterItemId", cascadeDelete: true);
        }
    }
}
