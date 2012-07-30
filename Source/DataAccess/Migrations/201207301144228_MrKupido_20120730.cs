namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MrKupido_20120730 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Addresses",
                c => new
                    {
                        AddressId = c.Int(nullable: false, identity: true),
                        Province = c.String(),
                        Town = c.String(),
                        PostalCode = c.String(),
                        AddressLine = c.String(),
                        Name = c.String(),
                        Deleted = c.DateTime(),
                        User_UserId = c.Int(),
                        Country_CountryId = c.Int(),
                    })
                .PrimaryKey(t => t.AddressId)
                .ForeignKey("dbo.Users", t => t.User_UserId)
                .ForeignKey("dbo.Countries", t => t.Country_CountryId)
                .Index(t => t.User_UserId)
                .Index(t => t.Country_CountryId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        CultureName = c.String(nullable: false, maxLength: 7),
                        FirstName = c.String(nullable: false, maxLength: 50),
                        FullName = c.String(nullable: false, maxLength: 110),
                        DateOfBirth = c.DateTime(nullable: false),
                        Height = c.Single(nullable: false),
                        Weight = c.Single(nullable: false),
                        NewsletterFlags = c.Int(nullable: false),
                        PrimaryAddressId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.UserId);
            
            CreateTable(
                "dbo.Countries",
                c => new
                    {
                        CountryId = c.Int(nullable: false, identity: true),
                        ISO = c.String(),
                        Name = c.String(),
                        DefaultCultureName = c.String(),
                        PostalCodeValidatorRegularExpression = c.String(),
                        PostalCodeSample = c.String(),
                        VATMultiplier = c.Single(nullable: false),
                        DefaultCurrency_CurrencyId = c.Int(),
                    })
                .PrimaryKey(t => t.CountryId)
                .ForeignKey("dbo.Currencies", t => t.DefaultCurrency_CurrencyId)
                .Index(t => t.DefaultCurrency_CurrencyId);
            
            CreateTable(
                "dbo.Currencies",
                c => new
                    {
                        CurrencyId = c.Int(nullable: false, identity: true),
                        ISO = c.String(),
                        Name = c.String(),
                        Prefix = c.String(),
                        Postfix = c.String(),
                        DefaultDecimalPlaces = c.Int(nullable: false),
                        Rate_ConversionRateId = c.Int(),
                    })
                .PrimaryKey(t => t.CurrencyId)
                .ForeignKey("dbo.ConversionRates", t => t.Rate_ConversionRateId)
                .Index(t => t.Rate_ConversionRateId);
            
            CreateTable(
                "dbo.ConversionRates",
                c => new
                    {
                        ConversionRateId = c.Int(nullable: false, identity: true),
                        Rate = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ConversionRateId);
            
            CreateTable(
                "dbo.Conditions",
                c => new
                    {
                        ConditionId = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.ConditionId);
            
            CreateTable(
                "dbo.FilterItems",
                c => new
                    {
                        FilterItemId = c.Int(nullable: false, identity: true),
                        NameEng = c.String(maxLength: 100),
                        NameHun = c.String(nullable: false, maxLength: 100),
                        UniqueNameEng = c.String(nullable: false, maxLength: 100),
                        UniqueNameHun = c.String(nullable: false, maxLength: 100),
                        Index = c.Int(),
                        ClassName = c.String(),
                        ElementId = c.Int(),
                        RecipeId = c.Int(),
                        ServingTemperature = c.Int(),
                        PreparationTime = c.Time(),
                        CookingTime = c.Time(),
                        TotalTime = c.Time(),
                        EstimatedPrice = c.Decimal(precision: 18, scale: 2),
                        EstimatedPriceDate = c.DateTime(),
                        Rating = c.Single(),
                        RatingCount = c.Int(),
                        RatingDate = c.DateTime(),
                        IngredientId = c.Int(),
                        ExpirationTime = c.Time(),
                        StorageTemperature = c.Single(),
                        NutritionId = c.Int(),
                        Amount = c.Single(),
                        RecipeNutritionId = c.Int(),
                        IngredientNutritionId = c.Int(),
                        SupplierId = c.Int(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                        Element_FilterItemId = c.Int(),
                        Recipe_FilterItemId = c.Int(),
                        Ingredient_FilterItemId = c.Int(),
                    })
                .PrimaryKey(t => t.FilterItemId)
                .ForeignKey("dbo.FilterItems", t => t.Element_FilterItemId)
                .ForeignKey("dbo.FilterItems", t => t.Recipe_FilterItemId)
                .ForeignKey("dbo.FilterItems", t => t.Ingredient_FilterItemId)
                .Index(t => t.Element_FilterItemId)
                .Index(t => t.Recipe_FilterItemId)
                .Index(t => t.Ingredient_FilterItemId);
            
            CreateTable(
                "dbo.IngredientPrices",
                c => new
                    {
                        IngredientPriceId = c.Int(nullable: false, identity: true),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Ingredient_FilterItemId = c.Int(),
                        Supplier_FilterItemId = c.Int(),
                        Currency_CurrencyId = c.Int(),
                    })
                .PrimaryKey(t => t.IngredientPriceId)
                .ForeignKey("dbo.FilterItems", t => t.Ingredient_FilterItemId)
                .ForeignKey("dbo.FilterItems", t => t.Supplier_FilterItemId)
                .ForeignKey("dbo.Currencies", t => t.Currency_CurrencyId)
                .Index(t => t.Ingredient_FilterItemId)
                .Index(t => t.Supplier_FilterItemId)
                .Index(t => t.Currency_CurrencyId);
            
            CreateTable(
                "dbo.IngredientRatings",
                c => new
                    {
                        IngredientRatingId = c.Int(nullable: false, identity: true),
                        Rating = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false),
                        User_UserId = c.Int(),
                        Ingredient_FilterItemId = c.Int(),
                    })
                .PrimaryKey(t => t.IngredientRatingId)
                .ForeignKey("dbo.Users", t => t.User_UserId)
                .ForeignKey("dbo.FilterItems", t => t.Ingredient_FilterItemId)
                .Index(t => t.User_UserId)
                .Index(t => t.Ingredient_FilterItemId);
            
            CreateTable(
                "dbo.Logs",
                c => new
                    {
                        LogId = c.Int(nullable: false, identity: true),
                        Message = c.String(nullable: false),
                        User_UserId = c.Int(),
                    })
                .PrimaryKey(t => t.LogId)
                .ForeignKey("dbo.Users", t => t.User_UserId)
                .Index(t => t.User_UserId);
            
            CreateTable(
                "dbo.RecipeRatings",
                c => new
                    {
                        RecipeRatingId = c.Int(nullable: false, identity: true),
                        Rating = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false),
                        User_UserId = c.Int(),
                        Recipe_FilterItemId = c.Int(),
                    })
                .PrimaryKey(t => t.RecipeRatingId)
                .ForeignKey("dbo.Users", t => t.User_UserId)
                .ForeignKey("dbo.FilterItems", t => t.Recipe_FilterItemId)
                .Index(t => t.User_UserId)
                .Index(t => t.Recipe_FilterItemId);
            
            AddColumn("dbo.RecipeTags", "AssignedAt", c => c.DateTime(nullable: false));
            AddColumn("dbo.RecipeTags", "Recipe_FilterItemId", c => c.Int());
            AddColumn("dbo.RecipeTags", "AssignedBy_UserId", c => c.Int());
            AddForeignKey("dbo.RecipeTags", "Recipe_FilterItemId", "dbo.FilterItems", "FilterItemId");
            AddForeignKey("dbo.RecipeTags", "AssignedBy_UserId", "dbo.Users", "UserId");
            CreateIndex("dbo.RecipeTags", "Recipe_FilterItemId");
            CreateIndex("dbo.RecipeTags", "AssignedBy_UserId");
            DropTable("dbo.Recipes");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Recipes",
                c => new
                    {
                        RecipeId = c.Int(nullable: false, identity: true),
                        DisplayNameEng = c.String(maxLength: 100),
                        UniqueNameEng = c.String(nullable: false, maxLength: 100),
                        DisplayNameHun = c.String(),
                        UniqueNameHun = c.String(),
                    })
                .PrimaryKey(t => t.RecipeId);
            
            DropIndex("dbo.RecipeTags", new[] { "AssignedBy_UserId" });
            DropIndex("dbo.RecipeTags", new[] { "Recipe_FilterItemId" });
            DropIndex("dbo.RecipeRatings", new[] { "Recipe_FilterItemId" });
            DropIndex("dbo.RecipeRatings", new[] { "User_UserId" });
            DropIndex("dbo.Logs", new[] { "User_UserId" });
            DropIndex("dbo.IngredientRatings", new[] { "Ingredient_FilterItemId" });
            DropIndex("dbo.IngredientRatings", new[] { "User_UserId" });
            DropIndex("dbo.IngredientPrices", new[] { "Currency_CurrencyId" });
            DropIndex("dbo.IngredientPrices", new[] { "Supplier_FilterItemId" });
            DropIndex("dbo.IngredientPrices", new[] { "Ingredient_FilterItemId" });
            DropIndex("dbo.FilterItems", new[] { "Ingredient_FilterItemId" });
            DropIndex("dbo.FilterItems", new[] { "Recipe_FilterItemId" });
            DropIndex("dbo.FilterItems", new[] { "Element_FilterItemId" });
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
            DropForeignKey("dbo.IngredientPrices", "Supplier_FilterItemId", "dbo.FilterItems");
            DropForeignKey("dbo.IngredientPrices", "Ingredient_FilterItemId", "dbo.FilterItems");
            DropForeignKey("dbo.FilterItems", "Ingredient_FilterItemId", "dbo.FilterItems");
            DropForeignKey("dbo.FilterItems", "Recipe_FilterItemId", "dbo.FilterItems");
            DropForeignKey("dbo.FilterItems", "Element_FilterItemId", "dbo.FilterItems");
            DropForeignKey("dbo.Currencies", "Rate_ConversionRateId", "dbo.ConversionRates");
            DropForeignKey("dbo.Countries", "DefaultCurrency_CurrencyId", "dbo.Currencies");
            DropForeignKey("dbo.Addresses", "Country_CountryId", "dbo.Countries");
            DropForeignKey("dbo.Addresses", "User_UserId", "dbo.Users");
            DropColumn("dbo.RecipeTags", "AssignedBy_UserId");
            DropColumn("dbo.RecipeTags", "Recipe_FilterItemId");
            DropColumn("dbo.RecipeTags", "AssignedAt");
            DropTable("dbo.RecipeRatings");
            DropTable("dbo.Logs");
            DropTable("dbo.IngredientRatings");
            DropTable("dbo.IngredientPrices");
            DropTable("dbo.FilterItems");
            DropTable("dbo.Conditions");
            DropTable("dbo.ConversionRates");
            DropTable("dbo.Currencies");
            DropTable("dbo.Countries");
            DropTable("dbo.Users");
            DropTable("dbo.Addresses");
        }
    }
}
