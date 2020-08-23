namespace DataAccess.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class MrKupido_20130315a : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Users", "Height", c => c.Single());
            AlterColumn("dbo.Users", "Weight", c => c.Single());
        }

        public override void Down()
        {
            AlterColumn("dbo.Users", "Weight", c => c.Single(nullable: false));
            AlterColumn("dbo.Users", "Height", c => c.Single(nullable: false));
        }
    }
}
