namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MkKupido_20120805a : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.FilterItems", "NameEng", c => c.String());
            AlterColumn("dbo.FilterItems", "UniqueNameEng", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.FilterItems", "UniqueNameEng", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.FilterItems", "NameEng", c => c.String(maxLength: 100));
        }
    }
}
