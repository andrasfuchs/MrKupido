namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MrKupido_20120730b : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Addresses", "User_UserId", "dbo.Users");
            DropForeignKey("dbo.Addresses", "Country_CountryId", "dbo.Countries");
            DropForeignKey("dbo.Countries", "DefaultCurrency_CurrencyId", "dbo.Currencies");
            DropForeignKey("dbo.Currencies", "Rate_ConversionRateId", "dbo.ConversionRates");
            DropForeignKey("dbo.IngredientPrices", "Ingredient_FilterItemId", "dbo.FilterItems");
            DropForeignKey("dbo.IngredientPrices", "Supplier_FilterItemId", "dbo.FilterItems");
            DropForeignKey("dbo.IngredientPrices", "Currency_CurrencyId", "dbo.Currencies");
            DropForeignKey("dbo.IngredientRatings", "User_UserId", "dbo.Users");
            DropForeignKey("dbo.IngredientRatings", "Ingredient_FilterItemId", "dbo.FilterItems");
            DropForeignKey("dbo.Logs", "User_UserId", "dbo.Users");
            DropForeignKey("dbo.RecipeRatings", "User_UserId", "dbo.Users");
            DropForeignKey("dbo.RecipeRatings", "Recipe_FilterItemId", "dbo.FilterItems");
            DropForeignKey("dbo.RecipeTags", "Recipe_FilterItemId", "dbo.FilterItems");
            DropForeignKey("dbo.RecipeTags", "AssignedBy_UserId", "dbo.Users");
            DropIndex("dbo.Addresses", new[] { "User_UserId" });
            DropIndex("dbo.Addresses", new[] { "Country_CountryId" });
            DropIndex("dbo.Countries", new[] { "DefaultCurrency_CurrencyId" });
            DropIndex("dbo.Currencies", new[] { "Rate_ConversionRateId" });
            DropIndex("dbo.IngredientPrices", new[] { "Ingredient_FilterItemId" });
            DropIndex("dbo.IngredientPrices", new[] { "Supplier_FilterItemId" });
            DropIndex("dbo.IngredientPrices", new[] { "Currency_CurrencyId" });
            DropIndex("dbo.IngredientRatings", new[] { "User_UserId" });
            DropIndex("dbo.IngredientRatings", new[] { "Ingredient_FilterItemId" });
            DropIndex("dbo.Logs", new[] { "User_UserId" });
            DropIndex("dbo.RecipeRatings", new[] { "User_UserId" });
            DropIndex("dbo.RecipeRatings", new[] { "Recipe_FilterItemId" });
            DropIndex("dbo.RecipeTags", new[] { "Recipe_FilterItemId" });
            DropIndex("dbo.RecipeTags", new[] { "AssignedBy_UserId" });
            RenameColumn(table: "dbo.IngredientPrices", name: "Supplier_FilterItemId", newName: "Supplier_SupplierId");
            CreateTable(
                "dbo.Suppliers",
                c => new
                    {
                        SupplierId = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.SupplierId);
            
            AddColumn("dbo.Users", "Email", c => c.String(nullable: false, maxLength: 50));
            AddColumn("dbo.Users", "LastName", c => c.String(nullable: false, maxLength: 50));
            AddColumn("dbo.Users", "NickName", c => c.String(nullable: false, maxLength: 50));
            AddColumn("dbo.IngredientPrices", "Store", c => c.String());
            AddColumn("dbo.IngredientPrices", "Date", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Addresses", "Province", c => c.String(nullable: false));
            AlterColumn("dbo.Addresses", "Town", c => c.String(nullable: false));
            AlterColumn("dbo.Addresses", "PostalCode", c => c.String(nullable: false));
            AlterColumn("dbo.Addresses", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.Addresses", "User_UserId", c => c.Int(nullable: false));
            AlterColumn("dbo.Addresses", "Country_CountryId", c => c.Int(nullable: false));
            AlterColumn("dbo.Countries", "ISO", c => c.String(nullable: false));
            AlterColumn("dbo.Countries", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.Countries", "DefaultCultureName", c => c.String(nullable: false));
            AlterColumn("dbo.Countries", "PostalCodeValidatorRegularExpression", c => c.String(nullable: false));
            AlterColumn("dbo.Countries", "PostalCodeSample", c => c.String(nullable: false));
            AlterColumn("dbo.Countries", "DefaultCurrency_CurrencyId", c => c.Int(nullable: false));
            AlterColumn("dbo.Currencies", "ISO", c => c.String(nullable: false));
            AlterColumn("dbo.Currencies", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.Currencies", "Prefix", c => c.String(nullable: false));
            AlterColumn("dbo.Currencies", "Postfix", c => c.String(nullable: false));
            AlterColumn("dbo.Currencies", "Rate_ConversionRateId", c => c.Int(nullable: false));
            AlterColumn("dbo.IngredientPrices", "Ingredient_FilterItemId", c => c.Int(nullable: false));
            AlterColumn("dbo.IngredientPrices", "Currency_CurrencyId", c => c.Int(nullable: false));
            AlterColumn("dbo.IngredientRatings", "User_UserId", c => c.Int(nullable: false));
            AlterColumn("dbo.IngredientRatings", "Ingredient_FilterItemId", c => c.Int(nullable: false));
            AlterColumn("dbo.Logs", "User_UserId", c => c.Int(nullable: false));
            AlterColumn("dbo.RecipeRatings", "User_UserId", c => c.Int(nullable: false));
            AlterColumn("dbo.RecipeRatings", "Recipe_FilterItemId", c => c.Int(nullable: false));
            AlterColumn("dbo.RecipeTags", "Recipe_FilterItemId", c => c.Int(nullable: false));
            AlterColumn("dbo.RecipeTags", "AssignedBy_UserId", c => c.Int(nullable: false));
            AddForeignKey("dbo.Addresses", "User_UserId", "dbo.Users", "UserId", cascadeDelete: true);
            AddForeignKey("dbo.Addresses", "Country_CountryId", "dbo.Countries", "CountryId", cascadeDelete: true);
            AddForeignKey("dbo.Countries", "DefaultCurrency_CurrencyId", "dbo.Currencies", "CurrencyId", cascadeDelete: true);
            AddForeignKey("dbo.Currencies", "Rate_ConversionRateId", "dbo.ConversionRates", "ConversionRateId", cascadeDelete: true);
            AddForeignKey("dbo.IngredientPrices", "Ingredient_FilterItemId", "dbo.FilterItems", "FilterItemId", cascadeDelete: true);
            AddForeignKey("dbo.IngredientPrices", "Supplier_SupplierId", "dbo.Suppliers", "SupplierId", cascadeDelete: true);
            AddForeignKey("dbo.IngredientPrices", "Currency_CurrencyId", "dbo.Currencies", "CurrencyId", cascadeDelete: true);
            AddForeignKey("dbo.IngredientRatings", "User_UserId", "dbo.Users", "UserId", cascadeDelete: true);
            AddForeignKey("dbo.IngredientRatings", "Ingredient_FilterItemId", "dbo.FilterItems", "FilterItemId", cascadeDelete: true);
            AddForeignKey("dbo.Logs", "User_UserId", "dbo.Users", "UserId", cascadeDelete: true);
            AddForeignKey("dbo.RecipeRatings", "User_UserId", "dbo.Users", "UserId", cascadeDelete: true);
            AddForeignKey("dbo.RecipeRatings", "Recipe_FilterItemId", "dbo.FilterItems", "FilterItemId", cascadeDelete: true);
            AddForeignKey("dbo.RecipeTags", "Recipe_FilterItemId", "dbo.FilterItems", "FilterItemId", cascadeDelete: true);
            AddForeignKey("dbo.RecipeTags", "AssignedBy_UserId", "dbo.Users", "UserId", cascadeDelete: true);
            CreateIndex("dbo.Addresses", "User_UserId");
            CreateIndex("dbo.Addresses", "Country_CountryId");
            CreateIndex("dbo.Countries", "DefaultCurrency_CurrencyId");
            CreateIndex("dbo.Currencies", "Rate_ConversionRateId");
            CreateIndex("dbo.IngredientPrices", "Ingredient_FilterItemId");
            CreateIndex("dbo.IngredientPrices", "Supplier_SupplierId");
            CreateIndex("dbo.IngredientPrices", "Currency_CurrencyId");
            CreateIndex("dbo.IngredientRatings", "User_UserId");
            CreateIndex("dbo.IngredientRatings", "Ingredient_FilterItemId");
            CreateIndex("dbo.Logs", "User_UserId");
            CreateIndex("dbo.RecipeRatings", "User_UserId");
            CreateIndex("dbo.RecipeRatings", "Recipe_FilterItemId");
            CreateIndex("dbo.RecipeTags", "Recipe_FilterItemId");
            CreateIndex("dbo.RecipeTags", "AssignedBy_UserId");
            DropColumn("dbo.FilterItems", "SupplierId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.FilterItems", "SupplierId", c => c.Int());
            DropIndex("dbo.RecipeTags", new[] { "AssignedBy_UserId" });
            DropIndex("dbo.RecipeTags", new[] { "Recipe_FilterItemId" });
            DropIndex("dbo.RecipeRatings", new[] { "Recipe_FilterItemId" });
            DropIndex("dbo.RecipeRatings", new[] { "User_UserId" });
            DropIndex("dbo.Logs", new[] { "User_UserId" });
            DropIndex("dbo.IngredientRatings", new[] { "Ingredient_FilterItemId" });
            DropIndex("dbo.IngredientRatings", new[] { "User_UserId" });
            DropIndex("dbo.IngredientPrices", new[] { "Currency_CurrencyId" });
            DropIndex("dbo.IngredientPrices", new[] { "Supplier_SupplierId" });
            DropIndex("dbo.IngredientPrices", new[] { "Ingredient_FilterItemId" });
            DropIndex("dbo.Currencies", new[] { "Rate_ConversionRateId" });
            DropIndex("dbo.Countries", new[] { "DefaultCurrency_CurrencyId" });
            DropIndex("dbo.Addresses", new[] { "Country_CountryId" });
            DropIndex("dbo.Addresses", new[] { "User_UserId" });
            DropForeignKey("dbo.RecipeTags", "AssignedBy_UserId", "dbo.Users");
            DropForeignKey("dbo.RecipeTags", "Recipe_FilterItemId", "dbo.FilterItems");
            DropForeignKey("dbo.RecipeRatings", "Recipe_FilterItemId", "dbo.FilterItems");
            DropForeignKey("dbo.RecipeRatings", "User_UserId", "dbo.Users");
            DropForeignKey("dbo.Logs", "User_UserId", "dbo.Users");
            DropForeignKey("dbo.IngredientRatings", "Ingredient_FilterItemId", "dbo.FilterItems");
            DropForeignKey("dbo.IngredientRatings", "User_UserId", "dbo.Users");
            DropForeignKey("dbo.IngredientPrices", "Currency_CurrencyId", "dbo.Currencies");
            DropForeignKey("dbo.IngredientPrices", "Supplier_SupplierId", "dbo.Suppliers");
            DropForeignKey("dbo.IngredientPrices", "Ingredient_FilterItemId", "dbo.FilterItems");
            DropForeignKey("dbo.Currencies", "Rate_ConversionRateId", "dbo.ConversionRates");
            DropForeignKey("dbo.Countries", "DefaultCurrency_CurrencyId", "dbo.Currencies");
            DropForeignKey("dbo.Addresses", "Country_CountryId", "dbo.Countries");
            DropForeignKey("dbo.Addresses", "User_UserId", "dbo.Users");
            AlterColumn("dbo.RecipeTags", "AssignedBy_UserId", c => c.Int());
            AlterColumn("dbo.RecipeTags", "Recipe_FilterItemId", c => c.Int());
            AlterColumn("dbo.RecipeRatings", "Recipe_FilterItemId", c => c.Int());
            AlterColumn("dbo.RecipeRatings", "User_UserId", c => c.Int());
            AlterColumn("dbo.Logs", "User_UserId", c => c.Int());
            AlterColumn("dbo.IngredientRatings", "Ingredient_FilterItemId", c => c.Int());
            AlterColumn("dbo.IngredientRatings", "User_UserId", c => c.Int());
            AlterColumn("dbo.IngredientPrices", "Currency_CurrencyId", c => c.Int());
            AlterColumn("dbo.IngredientPrices", "Ingredient_FilterItemId", c => c.Int());
            AlterColumn("dbo.Currencies", "Rate_ConversionRateId", c => c.Int());
            AlterColumn("dbo.Currencies", "Postfix", c => c.String());
            AlterColumn("dbo.Currencies", "Prefix", c => c.String());
            AlterColumn("dbo.Currencies", "Name", c => c.String());
            AlterColumn("dbo.Currencies", "ISO", c => c.String());
            AlterColumn("dbo.Countries", "DefaultCurrency_CurrencyId", c => c.Int());
            AlterColumn("dbo.Countries", "PostalCodeSample", c => c.String());
            AlterColumn("dbo.Countries", "PostalCodeValidatorRegularExpression", c => c.String());
            AlterColumn("dbo.Countries", "DefaultCultureName", c => c.String());
            AlterColumn("dbo.Countries", "Name", c => c.String());
            AlterColumn("dbo.Countries", "ISO", c => c.String());
            AlterColumn("dbo.Addresses", "Country_CountryId", c => c.Int());
            AlterColumn("dbo.Addresses", "User_UserId", c => c.Int());
            AlterColumn("dbo.Addresses", "Name", c => c.String());
            AlterColumn("dbo.Addresses", "PostalCode", c => c.String());
            AlterColumn("dbo.Addresses", "Town", c => c.String());
            AlterColumn("dbo.Addresses", "Province", c => c.String());
            DropColumn("dbo.IngredientPrices", "Date");
            DropColumn("dbo.IngredientPrices", "Store");
            DropColumn("dbo.Users", "NickName");
            DropColumn("dbo.Users", "LastName");
            DropColumn("dbo.Users", "Email");
            DropTable("dbo.Suppliers");
            RenameColumn(table: "dbo.IngredientPrices", name: "Supplier_SupplierId", newName: "Supplier_FilterItemId");
            CreateIndex("dbo.RecipeTags", "AssignedBy_UserId");
            CreateIndex("dbo.RecipeTags", "Recipe_FilterItemId");
            CreateIndex("dbo.RecipeRatings", "Recipe_FilterItemId");
            CreateIndex("dbo.RecipeRatings", "User_UserId");
            CreateIndex("dbo.Logs", "User_UserId");
            CreateIndex("dbo.IngredientRatings", "Ingredient_FilterItemId");
            CreateIndex("dbo.IngredientRatings", "User_UserId");
            CreateIndex("dbo.IngredientPrices", "Currency_CurrencyId");
            CreateIndex("dbo.IngredientPrices", "Supplier_FilterItemId");
            CreateIndex("dbo.IngredientPrices", "Ingredient_FilterItemId");
            CreateIndex("dbo.Currencies", "Rate_ConversionRateId");
            CreateIndex("dbo.Countries", "DefaultCurrency_CurrencyId");
            CreateIndex("dbo.Addresses", "Country_CountryId");
            CreateIndex("dbo.Addresses", "User_UserId");
            AddForeignKey("dbo.RecipeTags", "AssignedBy_UserId", "dbo.Users", "UserId");
            AddForeignKey("dbo.RecipeTags", "Recipe_FilterItemId", "dbo.FilterItems", "FilterItemId");
            AddForeignKey("dbo.RecipeRatings", "Recipe_FilterItemId", "dbo.FilterItems", "FilterItemId");
            AddForeignKey("dbo.RecipeRatings", "User_UserId", "dbo.Users", "UserId");
            AddForeignKey("dbo.Logs", "User_UserId", "dbo.Users", "UserId");
            AddForeignKey("dbo.IngredientRatings", "Ingredient_FilterItemId", "dbo.FilterItems", "FilterItemId");
            AddForeignKey("dbo.IngredientRatings", "User_UserId", "dbo.Users", "UserId");
            AddForeignKey("dbo.IngredientPrices", "Currency_CurrencyId", "dbo.Currencies", "CurrencyId");
            AddForeignKey("dbo.IngredientPrices", "Supplier_FilterItemId", "dbo.FilterItems", "FilterItemId");
            AddForeignKey("dbo.IngredientPrices", "Ingredient_FilterItemId", "dbo.FilterItems", "FilterItemId");
            AddForeignKey("dbo.Currencies", "Rate_ConversionRateId", "dbo.ConversionRates", "ConversionRateId");
            AddForeignKey("dbo.Countries", "DefaultCurrency_CurrencyId", "dbo.Currencies", "CurrencyId");
            AddForeignKey("dbo.Addresses", "Country_CountryId", "dbo.Countries", "CountryId");
            AddForeignKey("dbo.Addresses", "User_UserId", "dbo.Users", "UserId");
        }
    }
}
