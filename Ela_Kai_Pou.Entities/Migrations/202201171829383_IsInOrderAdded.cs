namespace Ela_Kai_Pou.Entities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IsInOrderAdded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "IsInOrder", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "IsInOrder");
        }
    }
}
