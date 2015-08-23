namespace ELib.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SumRatingValue_Field_Added_To_Book_And_SumLike_SumDisLike_Fields_Added_To_Comment : DbMigration
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
