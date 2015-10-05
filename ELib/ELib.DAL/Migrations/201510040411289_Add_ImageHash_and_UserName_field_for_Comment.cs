namespace ELib.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_ImageHash_and_UserName_field_for_Comment : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Comment", "UserName", c => c.String());
            AddColumn("dbo.Comment", "ImageHash", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Comment", "ImageHash");
            DropColumn("dbo.Comment", "UserName");
        }
    }
}
