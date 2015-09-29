namespace ELib.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Added_Category_entity : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Category",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        ParentId = c.Int(),
                        Level = c.Int(nullable: false),
                        BookCount = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Book", "CategoryId", c => c.Int(nullable: false));
            CreateIndex("dbo.Book", "CategoryId");
            AddForeignKey("dbo.Book", "CategoryId", "dbo.Category", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Book", "CategoryId", "dbo.Category");
            DropIndex("dbo.Book", new[] { "CategoryId" });
            DropColumn("dbo.Book", "CategoryId");
            DropTable("dbo.Category");
        }
    }
}
