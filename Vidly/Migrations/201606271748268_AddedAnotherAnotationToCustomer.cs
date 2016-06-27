namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedAnotherAnotationToCustomer : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.MembershipTypes", "MembershipName", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.MembershipTypes", "MembershipName", c => c.String());
        }
    }
}
