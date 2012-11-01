namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MrKupido_20121101a : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Logs", "User_UserId", "dbo.Users");
            DropIndex("dbo.Logs", new[] { "User_UserId" });
            AddColumn("dbo.Logs", "UtcTime", c => c.DateTime(nullable: false));
            AddColumn("dbo.Logs", "IPAddress", c => c.String(nullable: false));
            AddColumn("dbo.Logs", "SessionId", c => c.String(nullable: false));
            AddColumn("dbo.Logs", "Action", c => c.String(nullable: false));
            AddColumn("dbo.Logs", "Parameters", c => c.String(nullable: false));
            AddColumn("dbo.Logs", "FormattedMessage", c => c.String(nullable: false));
            DropColumn("dbo.Logs", "Message");
            DropColumn("dbo.Logs", "User_UserId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Logs", "User_UserId", c => c.Int(nullable: false));
            AddColumn("dbo.Logs", "Message", c => c.String(nullable: false));
            DropColumn("dbo.Logs", "FormattedMessage");
            DropColumn("dbo.Logs", "Parameters");
            DropColumn("dbo.Logs", "Action");
            DropColumn("dbo.Logs", "SessionId");
            DropColumn("dbo.Logs", "IPAddress");
            DropColumn("dbo.Logs", "UtcTime");
            CreateIndex("dbo.Logs", "User_UserId");
            AddForeignKey("dbo.Logs", "User_UserId", "dbo.Users", "UserId", cascadeDelete: true);
        }
    }
}
