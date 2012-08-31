namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MrKupido_20120831a : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.FilterItems", "NameEng", c => c.String(maxLength: 150));
            AlterColumn("dbo.FilterItems", "NameHun", c => c.String(nullable: false, maxLength: 150));
            AlterColumn("dbo.FilterItems", "UniqueNameEng", c => c.String(maxLength: 150));
            AlterColumn("dbo.FilterItems", "UniqueNameHun", c => c.String(nullable: false, maxLength: 150));
            AlterColumn("dbo.FilterItems", "ClassName", c => c.String(maxLength: 150));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.FilterItems", "ClassName", c => c.String());
            AlterColumn("dbo.FilterItems", "UniqueNameHun", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.FilterItems", "UniqueNameEng", c => c.String());
            AlterColumn("dbo.FilterItems", "NameHun", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.FilterItems", "NameEng", c => c.String());
        }
    }
}
