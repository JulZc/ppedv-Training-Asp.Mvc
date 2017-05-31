namespace VideothekData.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class SeedMembershipTypes : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO[dbo].[MembershipTypes] ([Id], [Name], [SignUpFee], [DurationInMonth], [DiscountRate]) VALUES(1, N'Pay as you go ', 0, 0, 0)");
            Sql("INSERT INTO[dbo].[MembershipTypes] ([Id], [Name], [SignUpFee], [DurationInMonth], [DiscountRate]) VALUES(2, N'Monthly', 30, 1, 10)");
            Sql("INSERT INTO[dbo].[MembershipTypes] ([Id], [Name], [SignUpFee], [DurationInMonth], [DiscountRate]) VALUES(3, N'Quarterly', 90, 3, 15)");
            Sql("INSERT INTO[dbo].[MembershipTypes] ([Id], [Name], [SignUpFee], [DurationInMonth], [DiscountRate]) VALUES(4, N'Annaul', 300, 12, 20)");
        }

        public override void Down()
        {
            Sql("DELETE FROM [dbo].[MembershipTypes] WHERE Id IN (1, 2, 3, 4)");
        }
    }
}
