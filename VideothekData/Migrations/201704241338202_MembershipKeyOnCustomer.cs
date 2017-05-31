namespace VideothekData.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MembershipKeyOnCustomer : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Customers", "IsSubscribedToNewsletter", c => c.Boolean(nullable: false));

            AddColumn("dbo.Customers", "MembershipId", c => c.Byte(nullable: false));
            CreateIndex("dbo.Customers", "MembershipId");
            AddForeignKey("dbo.Customers", "MembershipId", "dbo.MembershipTypes", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Customers", "MembershipId", "dbo.MembershipTypes");
            DropIndex("dbo.Customers", new[] { "MembershipId" });
            DropColumn("dbo.Customers", "MembershipId");
            DropColumn("dbo.Customers", "IsSubscribedToNewsletter");
        }
    }
}
