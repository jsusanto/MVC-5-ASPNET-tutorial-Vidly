namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeedUsers : DbMigration
    {
        public override void Up()
        {
            Sql(@"
INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'67a59f0d-4a47-4144-b35e-42ef01520d42', N'guest@jsusanto.com', 0, N'AE5NnV7hGE0Wn3SdZXhKcZkBdTSlAzCrJd77n5jhJGS+lO+0G8FBHCgisJNmySNeqg==', N'ed624dc9-bc58-4422-810e-3d039e3159f5', NULL, 0, 0, NULL, 1, 0, N'guest@jsusanto.com')
INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'b61cd977-24fd-4025-85e6-e92623886355', N'admin@jsusanto.com', 0, N'ADnJoinCWbaPME4/DsQZWnJ/aDDi3xr5b/i6DnB/TU0QC48gZJ1SlsN42UKqWUrQQA==', N'634e3fcf-1fc8-4c4b-83dc-40dc5db100ee', NULL, 0, 0, NULL, 1, 0, N'admin@jsusanto.com')

INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'9ec9c492-a961-40d4-b0d4-6b2614cbe2bc', N'CanManageMovies')

INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'b61cd977-24fd-4025-85e6-e92623886355', N'9ec9c492-a961-40d4-b0d4-6b2614cbe2bc')

");
           
        }
        
        public override void Down()
        {
        }
    }
}
