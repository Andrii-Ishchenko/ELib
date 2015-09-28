namespace ELib.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Change_Date_To_DateTime_And_Add_Default_Date : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Book", "AdditionDate", c => c.DateTime(defaultValueSql: "GETDATE()"));
            AlterColumn("dbo.BookInstance", "InsertDate", c => c.DateTime(defaultValueSql: "GETDATE()"));
            AlterColumn("dbo.Comment", "CommentDate", c => c.DateTime(defaultValueSql: "GETDATE()"));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Comment", "CommentDate", c => c.DateTime(nullable: false, storeType: "date"));
            AlterColumn("dbo.BookInstance", "InsertDate", c => c.DateTime(nullable: false, storeType: "date"));
            AlterColumn("dbo.Book", "AdditionDate", c => c.DateTime(nullable: false, storeType: "date"));
        }
    }
}
