namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MrKupido_20121101c : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Logs", "Parameters", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Logs", "Parameters", c => c.String(nullable: false));
        }
    }
}
