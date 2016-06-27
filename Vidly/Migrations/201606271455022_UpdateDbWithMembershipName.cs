namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateDbWithMembershipName : DbMigration
    {
        public override void Up()
        {
            Sql("UPDATE MembershipTypes SET MembershipName = 'Pay as You Go' WHERE DiscountRate=0;");
            Sql("UPDATE MembershipTypes SET MembershipName = 'Monthly' WHERE DiscountRate=10;");
            Sql("UPDATE MembershipTypes SET MembershipName = 'Quarterly' WHERE DiscountRate=15;");
            Sql("UPDATE MembershipTypes SET MembershipName = 'Annual' WHERE DiscountRate=20;");
        }
        
        public override void Down()
        {
        }
    }
}
