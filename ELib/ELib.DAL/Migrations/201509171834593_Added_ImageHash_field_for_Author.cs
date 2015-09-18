namespace ELib.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Added_ImageHash_field_for_Author : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Author", "ImageHash", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Author", "ImageHash");
        }
    }
}
