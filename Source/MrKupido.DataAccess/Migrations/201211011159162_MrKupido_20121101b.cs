namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MrKupido_20121101b : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Logs", "FormattedMessage", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Logs", "FormattedMessage", c => c.String(nullable: false));
        }
    }
}
