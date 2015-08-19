namespace ELib.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AuthorGenre",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AuthorId = c.Int(nullable: false),
                        GenreId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Author", t => t.AuthorId)
                .ForeignKey("dbo.Genre", t => t.GenreId)
                .Index(t => t.AuthorId)
                .Index(t => t.GenreId);
            
            CreateTable(
                "dbo.Author",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false, maxLength: 50),
                        LastName = c.String(nullable: false, maxLength: 50),
                        DateOfBirth = c.DateTime(),
                        DeathDate = c.DateTime(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.BookAuthor",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BookId = c.Int(nullable: false),
                        AuthorId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Book", t => t.BookId)
                .ForeignKey("dbo.Author", t => t.AuthorId)
                .Index(t => t.BookId)
                .Index(t => t.AuthorId);
            
            CreateTable(
                "dbo.Book",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 200),
                        PublishLangId = c.Int(nullable: false),
                        OriginalLangId = c.Int(),
                        TotalPages = c.Int(),
                        Isbn = c.String(maxLength: 20, unicode: false),
                        PublisherId = c.Int(nullable: false),
                        SubgenreId = c.Int(nullable: false),
                        PublishYear = c.DateTime(storeType: "date"),
                        Picture = c.Binary(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Language", t => t.PublishLangId)
                .ForeignKey("dbo.Language", t => t.OriginalLangId)
                .ForeignKey("dbo.Publisher", t => t.PublisherId)
                .ForeignKey("dbo.Subgenres", t => t.SubgenreId)
                .Index(t => t.PublishLangId)
                .Index(t => t.OriginalLangId)
                .Index(t => t.PublisherId)
                .Index(t => t.SubgenreId);
            
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
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.FileFormat", t => t.FormatId)
                .ForeignKey("dbo.Book", t => t.BookId)
                .Index(t => t.BookId)
                .Index(t => t.FormatId);
            
            CreateTable(
                "dbo.FileFormat",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 10),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.BookGenre",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BookId = c.Int(nullable: false),
                        GenreId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Genre", t => t.GenreId)
                .ForeignKey("dbo.Book", t => t.BookId)
                .Index(t => t.BookId)
                .Index(t => t.GenreId);
            
            CreateTable(
                "dbo.Genre",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 40),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Language",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 20),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Publisher",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 40),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.RatingBook",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BookId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                        ValueRating = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Person", t => t.UserId)
                .ForeignKey("dbo.Book", t => t.BookId)
                .Index(t => t.BookId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Person",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false, maxLength: 50),
                        LastName = c.String(maxLength: 50),
                        Email = c.String(maxLength: 100),
                        Phone = c.String(maxLength: 30),
                        Login = c.String(nullable: false, maxLength: 50, unicode: false),
                        Password = c.String(nullable: false, maxLength: 50, unicode: false),
                        RegistrationDate = c.DateTime(nullable: false, storeType: "date"),
                        LastActivationDate = c.DateTime(storeType: "date"),
                        RoleId = c.Int(nullable: false),
                        Photo = c.Binary(),
                        SmallPhoto = c.Binary(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.PersonRole", t => t.RoleId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.PersonRole",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 20),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.RatingComment",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CommentId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                        IsLike = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Comment", t => t.CommentId)
                .ForeignKey("dbo.Person", t => t.UserId)
                .Index(t => t.CommentId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Comment",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Text = c.String(maxLength: 400),
                        BookId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                        CommentDate = c.DateTime(nullable: false, storeType: "date"),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.UserBookStatus",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        BookId = c.Int(nullable: false),
                        StatusBook = c.Boolean(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Person", t => t.UserId)
                .ForeignKey("dbo.Book", t => t.BookId)
                .Index(t => t.UserId)
                .Index(t => t.BookId);
            
            CreateTable(
                "dbo.Subgenres",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 40),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.BookAuthor", "AuthorId", "dbo.Author");
            DropForeignKey("dbo.UserBookStatus", "BookId", "dbo.Book");
            DropForeignKey("dbo.Book", "SubgenreId", "dbo.Subgenres");
            DropForeignKey("dbo.RatingBook", "BookId", "dbo.Book");
            DropForeignKey("dbo.UserBookStatus", "UserId", "dbo.Person");
            DropForeignKey("dbo.RatingComment", "UserId", "dbo.Person");
            DropForeignKey("dbo.RatingComment", "CommentId", "dbo.Comment");
            DropForeignKey("dbo.RatingBook", "UserId", "dbo.Person");
            DropForeignKey("dbo.Person", "RoleId", "dbo.PersonRole");
            DropForeignKey("dbo.Book", "PublisherId", "dbo.Publisher");
            DropForeignKey("dbo.Book", "OriginalLangId", "dbo.Language");
            DropForeignKey("dbo.Book", "PublishLangId", "dbo.Language");
            DropForeignKey("dbo.BookGenre", "BookId", "dbo.Book");
            DropForeignKey("dbo.BookGenre", "GenreId", "dbo.Genre");
            DropForeignKey("dbo.AuthorGenre", "GenreId", "dbo.Genre");
            DropForeignKey("dbo.BookFormat", "BookId", "dbo.Book");
            DropForeignKey("dbo.BookFormat", "FormatId", "dbo.FileFormat");
            DropForeignKey("dbo.BookAuthor", "BookId", "dbo.Book");
            DropForeignKey("dbo.AuthorGenre", "AuthorId", "dbo.Author");
            DropIndex("dbo.UserBookStatus", new[] { "BookId" });
            DropIndex("dbo.UserBookStatus", new[] { "UserId" });
            DropIndex("dbo.RatingComment", new[] { "UserId" });
            DropIndex("dbo.RatingComment", new[] { "CommentId" });
            DropIndex("dbo.Person", new[] { "RoleId" });
            DropIndex("dbo.RatingBook", new[] { "UserId" });
            DropIndex("dbo.RatingBook", new[] { "BookId" });
            DropIndex("dbo.BookGenre", new[] { "GenreId" });
            DropIndex("dbo.BookGenre", new[] { "BookId" });
            DropIndex("dbo.BookFormat", new[] { "FormatId" });
            DropIndex("dbo.BookFormat", new[] { "BookId" });
            DropIndex("dbo.Book", new[] { "SubgenreId" });
            DropIndex("dbo.Book", new[] { "PublisherId" });
            DropIndex("dbo.Book", new[] { "OriginalLangId" });
            DropIndex("dbo.Book", new[] { "PublishLangId" });
            DropIndex("dbo.BookAuthor", new[] { "AuthorId" });
            DropIndex("dbo.BookAuthor", new[] { "BookId" });
            DropIndex("dbo.AuthorGenre", new[] { "GenreId" });
            DropIndex("dbo.AuthorGenre", new[] { "AuthorId" });
            DropTable("dbo.Subgenres");
            DropTable("dbo.UserBookStatus");
            DropTable("dbo.Comment");
            DropTable("dbo.RatingComment");
            DropTable("dbo.PersonRole");
            DropTable("dbo.Person");
            DropTable("dbo.RatingBook");
            DropTable("dbo.Publisher");
            DropTable("dbo.Language");
            DropTable("dbo.Genre");
            DropTable("dbo.BookGenre");
            DropTable("dbo.FileFormat");
            DropTable("dbo.BookFormat");
            DropTable("dbo.Book");
            DropTable("dbo.BookAuthor");
            DropTable("dbo.Author");
            DropTable("dbo.AuthorGenre");
        }
    }
}
