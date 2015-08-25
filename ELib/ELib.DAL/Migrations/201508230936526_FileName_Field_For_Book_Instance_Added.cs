namespace ELib.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FileName_Field_For_Book_Instance_Added : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.BookInstance", "FileName", c => c.String(maxLength: 400));
        }
        
        public override void Down()
        {
            DropColumn("dbo.BookInstance", "FileName");
        }
    }
}
