namespace ELib.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedfavs : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Favorite",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BookId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                        AdditionDate = c.DateTime(nullable: false,defaultValueSql:"GETDATE()"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Person", t => t.UserId)
                .ForeignKey("dbo.Book", t => t.BookId)
                .Index(t => t.BookId)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Favorite", "BookId", "dbo.Book");
            DropForeignKey("dbo.Favorite", "UserId", "dbo.Person");
            DropIndex("dbo.Favorite", new[] { "UserId" });
            DropIndex("dbo.Favorite", new[] { "BookId" });
            DropTable("dbo.Favorite");
        }
    }
}
