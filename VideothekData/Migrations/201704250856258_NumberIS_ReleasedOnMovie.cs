namespace VideothekData.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NumberIS_ReleasedOnMovie : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Movies", "NumberInStock", c => c.Int(nullable: false));
            AddColumn("dbo.Movies", "Released", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Movies", "Released");
            DropColumn("dbo.Movies", "NumberInStock");
        }
    }
}
