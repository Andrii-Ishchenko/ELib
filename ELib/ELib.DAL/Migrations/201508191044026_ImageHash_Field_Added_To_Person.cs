namespace ELib.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ImageHash_Field_Added_To_Person : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Person", "ImageHash", c => c.String());
            DropColumn("dbo.Person", "Photo");
            DropColumn("dbo.Person", "SmallPhoto");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Person", "SmallPhoto", c => c.Binary());
            AddColumn("dbo.Person", "Photo", c => c.Binary());
            DropColumn("dbo.Person", "ImageHash");
        }
    }
}
