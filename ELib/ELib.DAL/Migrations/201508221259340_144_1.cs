namespace ELib.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _144_1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Book", "SumRatingValue", c => c.Int(nullable: false));
            AddColumn("dbo.Comment", "SumLike", c => c.Int(nullable: false));
            AddColumn("dbo.Comment", "SumDisLike", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Comment", "SumDisLike");
            DropColumn("dbo.Comment", "SumLike");
            DropColumn("dbo.Book", "SumRatingValue");
        }
    }
}
