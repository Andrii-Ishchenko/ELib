namespace ELib.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_AdditionDate_And_Cahnge_PublishYear_Type_In_Book_Entity : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Book", "AdditionDate", c => c.DateTime(nullable: false, storeType: "date"));
            DropColumn("dbo.Book", "PublishYear");
            AddColumn("dbo.Book", "PublishYear", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Book", "AdditionDate");
            DropColumn("dbo.Book", "PublishYear");
            AddColumn("dbo.Book", "PublishYear", c => c.DateTime(storeType: "date"));
        }
    }
}
