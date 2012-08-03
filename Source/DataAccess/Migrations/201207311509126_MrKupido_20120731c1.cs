namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MrKupido_20120731c1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Nutritions", "IngredientNutritionId", c => c.Int());
            AddColumn("dbo.Nutritions", "RecipeNutritionId", c => c.Int());
            AddColumn("dbo.Nutritions", "Discriminator", c => c.String(nullable: false, maxLength: 128));
            DropTable("dbo.IngredientNutritions");
            DropTable("dbo.RecipeNutritions");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.RecipeNutritions",
                c => new
                    {
                        RecipeNutritionId = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.RecipeNutritionId);
            
            CreateTable(
                "dbo.IngredientNutritions",
                c => new
                    {
                        IngredientNutritionId = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.IngredientNutritionId);
            
            DropColumn("dbo.Nutritions", "Discriminator");
            DropColumn("dbo.Nutritions", "RecipeNutritionId");
            DropColumn("dbo.Nutritions", "IngredientNutritionId");
        }
    }
}
