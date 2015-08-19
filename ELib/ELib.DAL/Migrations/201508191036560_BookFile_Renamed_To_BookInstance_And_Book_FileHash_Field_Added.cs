namespace ELib.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BookFile_Renamed_To_BookInstance_And_Book_FileHash_Field_Added : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.BookFormat", newName: "BookInstance");
            AddColumn("dbo.Book", "ImageHash", c => c.String());
            DropColumn("dbo.Book", "Picture");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Book", "Picture", c => c.Binary());
            DropColumn("dbo.Book", "ImageHash");
            RenameTable(name: "dbo.BookInstance", newName: "BookFormat");
        }
    }
}
