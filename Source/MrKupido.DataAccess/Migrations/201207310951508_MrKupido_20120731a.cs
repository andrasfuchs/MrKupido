namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MrKupido_20120731a : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Users", "PrimaryAddressId", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Users", "PrimaryAddressId", c => c.Int(nullable: false));
        }
    }
}
