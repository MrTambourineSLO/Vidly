namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeedUsers : DbMigration
    {
        public override void Up()
        {
            Sql(@"INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'14452569-8f81-47af-93a7-9628fb806941', N'guest@vidly.com', 0, N'AH3Kw/B9SpbcxrC4pOpXsTz2KD40CNp6FE8Wjx7HEfnRwLo8yKvysOznE2qSY7iD2A==', N'a8684544-e093-4de8-9547-c4068327b0c0', NULL, 0, 0, NULL, 1, 0, N'guest@vidly.com')
                  INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'76045d25-1da2-46ac-abb9-f57861dce8c8', N'admin@vidly.com', 0, N'APkpO/bR1HlLN21fMljQ0VQOCMqeb7pO+/rXN8qjDaY7vc4XKB6K5olC0y6fEEfeMA==', N'41a41d5b-a6f3-4c7c-acc2-6903c71b215b', NULL, 0, 0, NULL, 1, 0, N'admin@vidly.com')
                  INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'544832b5-7b52-4b41-876e-859912b36f8e', N'CanManageMovies')                  
                  INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'76045d25-1da2-46ac-abb9-f57861dce8c8', N'544832b5-7b52-4b41-876e-859912b36f8e')
                  ");
        }
        
        public override void Down()
        {
        }
    }
}
