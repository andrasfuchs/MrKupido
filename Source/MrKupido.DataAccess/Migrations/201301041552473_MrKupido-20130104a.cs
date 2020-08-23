namespace DataAccess.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class MrKupido20130104a : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "FirstLoginUtc", c => c.DateTime(nullable: false));
            AddColumn("dbo.Users", "LastLoginUtc", c => c.DateTime(nullable: false));
        }

        public override void Down()
        {
            DropColumn("dbo.Users", "LastLoginUtc");
            DropColumn("dbo.Users", "FirstLoginUtc");
        }
    }
}
