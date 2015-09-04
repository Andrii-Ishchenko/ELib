namespace ELib.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Link_Person_And_ApplicationUser_By_ApplicationUserId : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ApplicationUsers", "PersonId", "dbo.Person");
            DropIndex("dbo.ApplicationUsers", new[] { "PersonId" });
            AddColumn("dbo.Person", "ApplicationUserId", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.Person", "ApplicationUserId");
            AddForeignKey("dbo.Person", "ApplicationUserId", "dbo.ApplicationUsers", "Id", cascadeDelete: true);
            DropColumn("dbo.ApplicationUsers", "PersonId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ApplicationUsers", "PersonId", c => c.Int(nullable: false));
            DropForeignKey("dbo.Person", "ApplicationUserId", "dbo.ApplicationUsers");
            DropIndex("dbo.Person", new[] { "ApplicationUserId" });
            DropColumn("dbo.Person", "ApplicationUserId");
            CreateIndex("dbo.ApplicationUsers", "PersonId");
            AddForeignKey("dbo.ApplicationUsers", "PersonId", "dbo.Person", "Id", cascadeDelete: true);
        }
    }
}
