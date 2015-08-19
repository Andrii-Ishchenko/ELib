namespace ELib.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FileFormat_Removed : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.BookFormat", "FormatId", "dbo.FileFormat");
            DropForeignKey("dbo.BookFormat", "BookId", "dbo.Book");
            DropIndex("dbo.BookFormat", new[] { "FormatId" });
            
            DropTable("dbo.BookFormat");

            CreateTable(
                "dbo.BookFormat",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    BookId = c.Int(nullable: false),
                    FileHash = c.String(),
                    InsertDate = c.DateTime(nullable: false, storeType: "date"),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Book", t => t.BookId);

            DropTable("dbo.FileFormat");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.BookFormat", "BookId", "dbo.Book");
            DropTable("dbo.BookFormat");

            CreateTable(
                "dbo.FileFormat",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Name = c.String(nullable: false, maxLength: 10),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.BookFormat",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    BookId = c.Int(nullable: false),
                    FormatId = c.Int(nullable: false),
                    FilePath = c.String(),
                    InsertDate = c.DateTime(nullable: false, storeType: "date"),
                })
                .PrimaryKey(t => t.Id);

            CreateIndex("dbo.BookFormat", "FormatId");
            AddForeignKey("dbo.BookFormat", "BookId", "dbo.Book", "Id");
            AddForeignKey("dbo.BookFormat", "FormatId", "dbo.FileFormat", "Id");
        }
    }
}
