namespace DataAccess.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class MrKupido20130102a : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "AvatarUrl", c => c.String(maxLength: 110));
        }

        public override void Down()
        {
            DropColumn("dbo.Users", "AvatarUrl");
        }
    }
}
