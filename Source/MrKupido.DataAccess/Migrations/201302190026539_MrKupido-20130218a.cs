namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MrKupido20130218a : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Users", "AvatarUrl");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Users", "AvatarUrl", c => c.String(maxLength: 110));
        }
    }
}
