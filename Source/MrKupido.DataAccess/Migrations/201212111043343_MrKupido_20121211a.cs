namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MrKupido_20121211a : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Addresses",
                c => new
                    {
                        AddressId = c.Int(nullable: false, identity: true),
                        Province = c.String(nullable: false),
                        Town = c.String(nullable: false),
                        PostalCode = c.String(nullable: false),
                        AddressLine = c.String(),
                        Name = c.String(nullable: false),
                        Deleted = c.DateTime(),
                        User_UserId = c.Int(nullable: false),
                        Country_CountryId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.AddressId)
                .ForeignKey("dbo.Users", t => t.User_UserId, cascadeDelete: true)
                .ForeignKey("dbo.Countries", t => t.Country_CountryId, cascadeDelete: true)
                .Index(t => t.User_UserId)
                .Index(t => t.Country_CountryId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        Email = c.String(nullable: false, maxLength: 50),
                        CultureName = c.String(nullable: false, maxLength: 7),
                        FirstName = c.String(nullable: false, maxLength: 50),
                        LastName = c.String(nullable: false, maxLength: 50),
                        FullName = c.String(nullable: false, maxLength: 110),
                        NickName = c.String(maxLength: 50),
                        Gender = c.Int(nullable: false),
                        DateOfBirth = c.DateTime(),
                        Height = c.Single(nullable: false),
                        Weight = c.Single(nullable: false),
                        NewsletterFlags = c.Int(nullable: false),
                        PrimaryAddressId = c.Int(),
                    })
                .PrimaryKey(t => t.UserId);
            
            CreateTable(
                "dbo.Countries",
                c => new
                    {
                        CountryId = c.Int(nullable: false, identity: true),
                        ISO = c.String(nullable: false),
                        Name = c.String(nullable: false),
                        DefaultCultureName = c.String(nullable: false),
                        PostalCodeValidatorRegularExpression = c.String(nullable: false),
                        PostalCodeSample = c.String(nullable: false),
                        VATMultiplier = c.Single(nullable: false),
                        DefaultCurrency_CurrencyId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CountryId)
                .ForeignKey("dbo.Currencies", t => t.DefaultCurrency_CurrencyId, cascadeDelete: true)
                .Index(t => t.DefaultCurrency_CurrencyId);
            
            CreateTable(
                "dbo.Currencies",
                c => new
                    {
                        CurrencyId = c.Int(nullable: false, identity: true),
                        ISO = c.String(nullable: false),
                        Name = c.String(nullable: false),
                        Prefix = c.String(nullable: false),
                        Postfix = c.String(nullable: false),
                        DefaultDecimalPlaces = c.Int(nullable: false),
                        Rate_ConversionRateId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CurrencyId)
                .ForeignKey("dbo.ConversionRates", t => t.Rate_ConversionRateId, cascadeDelete: true)
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
                        Type = c.Int(nullable: false),
                        NameEng = c.String(maxLength: 150),
                        NameHun = c.String(nullable: false, maxLength: 150),
                        UniqueNameEng = c.String(maxLength: 150),
                        UniqueNameHun = c.String(nullable: false, maxLength: 150),
                        Index = c.Int(),
                        ClassName = c.String(maxLength: 150),
                        Category = c.Int(),
                        Unit = c.Int(),
                        ServingTemperature = c.Int(),
                        PreparationTime = c.Int(),
                        CookingTime = c.Int(),
                        TotalTime = c.Int(),
                        EstimatedPrice = c.Decimal(precision: 18, scale: 2),
                        EstimatedPriceDate = c.DateTime(),
                        Rating = c.Single(),
                        RatingCount = c.Int(),
                        RatingDate = c.DateTime(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.FilterItemId);
            
            CreateTable(
                "dbo.ImportedRecipes",
                c => new
                    {
                        ImportedRecipeId = c.Int(nullable: false, identity: true),
                        ImportedAt = c.DateTime(nullable: false),
                        State = c.Int(nullable: false),
                        Language = c.String(nullable: false, maxLength: 3),
                        UniqueName = c.String(nullable: false, maxLength: 150),
                        DisplayName = c.String(nullable: false, maxLength: 150),
                        Description = c.String(),
                        Category = c.String(),
                        Uploader = c.String(),
                        UploadedOn = c.DateTime(),
                        Rating = c.Single(nullable: false),
                        CookTime = c.Int(),
                        PreparationTime = c.Int(),
                        TotalTime = c.Int(),
                        Difficulty = c.Int(),
                        PriceCategory = c.Int(),
                        Favourited = c.Int(),
                        Forwarded = c.Int(),
                        Ingredients = c.String(nullable: false),
                        Directions = c.String(nullable: false),
                        OriginalDirections = c.String(nullable: false),
                        Footnotes = c.String(),
                        Tags = c.String(),
                        Servings = c.Int(),
                        UnitSystem = c.String(),
                        NutritionShort = c.String(),
                        NutritionDetailed = c.String(),
                        Reviews = c.String(),
                        ReviewCount = c.Int(),
                        RecipesLikeThis = c.String(),
                    })
                .PrimaryKey(t => t.ImportedRecipeId);
            
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
                .PrimaryKey(t => t.NutritionId)
                .ForeignKey("dbo.FilterItems", t => t.Element_FilterItemId, cascadeDelete: true)
                .Index(t => t.Element_FilterItemId);
            
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
                .PrimaryKey(t => t.IngredientPriceId)
                .ForeignKey("dbo.FilterItems", t => t.Ingredient_FilterItemId, cascadeDelete: true)
                .ForeignKey("dbo.Suppliers", t => t.Supplier_SupplierId, cascadeDelete: true)
                .ForeignKey("dbo.Currencies", t => t.Currency_CurrencyId, cascadeDelete: true)
                .Index(t => t.Ingredient_FilterItemId)
                .Index(t => t.Supplier_SupplierId)
                .Index(t => t.Currency_CurrencyId);
            
            CreateTable(
                "dbo.Suppliers",
                c => new
                    {
                        SupplierId = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.SupplierId);
            
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
                .PrimaryKey(t => t.IngredientRatingId)
                .ForeignKey("dbo.Users", t => t.User_UserId, cascadeDelete: true)
                .ForeignKey("dbo.FilterItems", t => t.Ingredient_FilterItemId, cascadeDelete: true)
                .Index(t => t.User_UserId)
                .Index(t => t.Ingredient_FilterItemId);
            
            CreateTable(
                "dbo.Logs",
                c => new
                    {
                        LogId = c.Int(nullable: false, identity: true),
                        UtcTime = c.DateTime(nullable: false),
                        IPAddress = c.String(nullable: false),
                        SessionId = c.String(nullable: false),
                        Action = c.String(nullable: false),
                        Parameters = c.String(),
                        FormattedMessage = c.String(),
                    })
                .PrimaryKey(t => t.LogId);
            
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
                .PrimaryKey(t => t.RecipeRatingId)
                .ForeignKey("dbo.Users", t => t.User_UserId, cascadeDelete: true)
                .ForeignKey("dbo.FilterItems", t => t.Recipe_FilterItemId, cascadeDelete: true)
                .Index(t => t.User_UserId)
                .Index(t => t.Recipe_FilterItemId);
            
            CreateTable(
                "dbo.RecipeTags",
                c => new
                    {
                        RecipeTagId = c.Int(nullable: false, identity: true),
                        AssignedAt = c.DateTime(nullable: false),
                        Recipe_FilterItemId = c.Int(nullable: false),
                        AssignedBy_UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.RecipeTagId)
                .ForeignKey("dbo.FilterItems", t => t.Recipe_FilterItemId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.AssignedBy_UserId, cascadeDelete: true)
                .Index(t => t.Recipe_FilterItemId)
                .Index(t => t.AssignedBy_UserId);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.RecipeTags", new[] { "AssignedBy_UserId" });
            DropIndex("dbo.RecipeTags", new[] { "Recipe_FilterItemId" });
            DropIndex("dbo.RecipeRatings", new[] { "Recipe_FilterItemId" });
            DropIndex("dbo.RecipeRatings", new[] { "User_UserId" });
            DropIndex("dbo.IngredientRatings", new[] { "Ingredient_FilterItemId" });
            DropIndex("dbo.IngredientRatings", new[] { "User_UserId" });
            DropIndex("dbo.IngredientPrices", new[] { "Currency_CurrencyId" });
            DropIndex("dbo.IngredientPrices", new[] { "Supplier_SupplierId" });
            DropIndex("dbo.IngredientPrices", new[] { "Ingredient_FilterItemId" });
            DropIndex("dbo.Nutritions", new[] { "Element_FilterItemId" });
            DropIndex("dbo.Currencies", new[] { "Rate_ConversionRateId" });
            DropIndex("dbo.Countries", new[] { "DefaultCurrency_CurrencyId" });
            DropIndex("dbo.Addresses", new[] { "Country_CountryId" });
            DropIndex("dbo.Addresses", new[] { "User_UserId" });
            DropForeignKey("dbo.RecipeTags", "AssignedBy_UserId", "dbo.Users");
            DropForeignKey("dbo.RecipeTags", "Recipe_FilterItemId", "dbo.FilterItems");
            DropForeignKey("dbo.RecipeRatings", "Recipe_FilterItemId", "dbo.FilterItems");
            DropForeignKey("dbo.RecipeRatings", "User_UserId", "dbo.Users");
            DropForeignKey("dbo.IngredientRatings", "Ingredient_FilterItemId", "dbo.FilterItems");
            DropForeignKey("dbo.IngredientRatings", "User_UserId", "dbo.Users");
            DropForeignKey("dbo.IngredientPrices", "Currency_CurrencyId", "dbo.Currencies");
            DropForeignKey("dbo.IngredientPrices", "Supplier_SupplierId", "dbo.Suppliers");
            DropForeignKey("dbo.IngredientPrices", "Ingredient_FilterItemId", "dbo.FilterItems");
            DropForeignKey("dbo.Nutritions", "Element_FilterItemId", "dbo.FilterItems");
            DropForeignKey("dbo.Currencies", "Rate_ConversionRateId", "dbo.ConversionRates");
            DropForeignKey("dbo.Countries", "DefaultCurrency_CurrencyId", "dbo.Currencies");
            DropForeignKey("dbo.Addresses", "Country_CountryId", "dbo.Countries");
            DropForeignKey("dbo.Addresses", "User_UserId", "dbo.Users");
            DropTable("dbo.RecipeTags");
            DropTable("dbo.RecipeRatings");
            DropTable("dbo.Logs");
            DropTable("dbo.IngredientRatings");
            DropTable("dbo.Suppliers");
            DropTable("dbo.IngredientPrices");
            DropTable("dbo.Nutritions");
            DropTable("dbo.ImportedRecipes");
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
