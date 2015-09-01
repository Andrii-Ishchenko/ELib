namespace ELib.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Person_And_ApplicationUser_Entities_Updated : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Person", "AplicationUserId", "dbo.ApplicationUsers");
            DropIndex("dbo.Person", new[] { "AplicationUserId" });
            DropColumn("dbo.Person", "AplicationUserId");
            AlterColumn("dbo.Person", "FirstName", c => c.String(maxLength: 50));
            AddColumn("dbo.ApplicationUsers", "PersonId", c => c.Int(nullable: false));
            CreateIndex("dbo.ApplicationUsers", "PersonId");
            AddForeignKey("dbo.ApplicationUsers", "PersonId", "dbo.Person", "Id", cascadeDelete: true);
            DropColumn("dbo.Person", "Email");
            DropColumn("dbo.Person", "Phone");
            DropColumn("dbo.Person", "Login");
            DropColumn("dbo.Person", "RegistrationDate");
            DropColumn("dbo.Person", "LastActivationDate");
          
        }
        
        public override void Down()
        {
            AddColumn("dbo.Person", "AplicationUserId", c => c.String(nullable: false, maxLength: 128));
            AddColumn("dbo.Person", "LastActivationDate", c => c.DateTime(storeType: "date"));
            AddColumn("dbo.Person", "RegistrationDate", c => c.DateTime(nullable: false, storeType: "date"));
            AddColumn("dbo.Person", "Login", c => c.String(nullable: false, maxLength: 50, unicode: false));
            AddColumn("dbo.Person", "Phone", c => c.String(maxLength: 30));
            AddColumn("dbo.Person", "Email", c => c.String(maxLength: 100));
            DropForeignKey("dbo.ApplicationUsers", "PersonId", "dbo.Person");
            DropIndex("dbo.ApplicationUsers", new[] { "PersonId" });
            AlterColumn("dbo.Person", "FirstName", c => c.String(nullable: false, maxLength: 50));
            DropColumn("dbo.ApplicationUsers", "PersonId");
            CreateIndex("dbo.Person", "AplicationUserId");
            AddForeignKey("dbo.Person", "AplicationUserId", "dbo.ApplicationUsers", "Id");
        }
    }
}
