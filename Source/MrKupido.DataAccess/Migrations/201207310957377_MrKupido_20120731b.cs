namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MrKupido_20120731b : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Users", "NickName", c => c.String(maxLength: 50));
            AlterColumn("dbo.Users", "DateOfBirth", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Users", "DateOfBirth", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Users", "NickName", c => c.String(nullable: false, maxLength: 50));
        }
    }
}
