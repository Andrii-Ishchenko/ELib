namespace ELib.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Download_And_View_Count_For_Book_And_BookInstance_Added : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Book", "TotalDownloadCount", c => c.Int(nullable: false));
            AddColumn("dbo.Book", "TotalViewCount", c => c.Int(nullable: false));
            AddColumn("dbo.BookInstance", "DownloadCount", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.BookInstance", "DownloadCount");
            DropColumn("dbo.Book", "TotalViewCount");
            DropColumn("dbo.Book", "TotalDownloadCount");
        }
    }
}
