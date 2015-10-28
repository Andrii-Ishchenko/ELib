namespace ELib.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class LinkCommentandPersonbyUserId : DbMigration
    {
        public override void Up()
        {
            AddForeignKey("dbo.Comment", "UserId", "dbo.Person", "Id", cascadeDelete: false);
            AddForeignKey("dbo.Comment", "BookId", "dbo.Book", "Id", cascadeDelete: false);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Comment", "BookId", "dbo.Book");
            DropForeignKey("dbo.Comment", "UserId", "dbo.Person");
        }
    }
}
