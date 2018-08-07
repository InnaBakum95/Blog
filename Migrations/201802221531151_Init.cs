namespace NewBlogAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BlogAdminTags",
                c => new
                    {
                        AdminId = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                        TagValue = c.String(nullable: false, maxLength: 256, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => new { t.AdminId, t.TagValue })
                .ForeignKey("dbo.AspNetUsers", t => t.AdminId, cascadeDelete: true)
                .Index(t => t.AdminId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                        Contacts = c.String(unicode: false),
                        Image = c.Binary(),
                        Email = c.String(maxLength: 256, storeType: "nvarchar"),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(unicode: false),
                        SecurityStamp = c.String(unicode: false),
                        PhoneNumber = c.String(unicode: false),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(precision: 0),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                        ClaimType = c.String(unicode: false),
                        ClaimValue = c.String(unicode: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                        ProviderKey = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                        UserId = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                        RoleId = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.BlogArticleComments",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        AuthorName = c.String(nullable: false, maxLength: 50, storeType: "nvarchar"),
                        CommentText = c.String(nullable: false, unicode: false),
                        BlogArticleId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.BlogArticles", t => t.BlogArticleId, cascadeDelete: true)
                .Index(t => t.BlogArticleId);
            
            CreateTable(
                "dbo.BlogArticles",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        NameOfArticle = c.String(nullable: false, maxLength: 200, storeType: "nvarchar"),
                        DiscriptionOfArticle = c.String(maxLength: 500, storeType: "nvarchar"),
                        Image = c.String(unicode: false),
                        ArticleText = c.String(nullable: false, unicode: false),
                        CreateDate = c.DateTime(nullable: false, precision: 0),
                        BlogAdminId = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.BlogAdminId, cascadeDelete: true)
                .Index(t => t.BlogAdminId);
            
            CreateTable(
                "dbo.BlogArticleFiles",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        FileName = c.String(nullable: false, unicode: false),
                        FileType = c.String(nullable: false, unicode: false),
                        FileContent = c.Binary(nullable: false),
                        ArticleId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.BlogArticles", t => t.ArticleId, cascadeDelete: true)
                .Index(t => t.ArticleId);
            
            CreateTable(
                "dbo.BlogArticleTags",
                c => new
                    {
                        BlogArticleId = c.Long(nullable: false),
                        TagValue = c.String(nullable: false, maxLength: 256, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => new { t.BlogArticleId, t.TagValue })
                .ForeignKey("dbo.BlogArticles", t => t.BlogArticleId, cascadeDelete: true)
                .Index(t => t.BlogArticleId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                        Name = c.String(nullable: false, maxLength: 256, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.BlogArticleTags", "BlogArticleId", "dbo.BlogArticles");
            DropForeignKey("dbo.BlogArticleFiles", "ArticleId", "dbo.BlogArticles");
            DropForeignKey("dbo.BlogArticleComments", "BlogArticleId", "dbo.BlogArticles");
            DropForeignKey("dbo.BlogArticles", "BlogAdminId", "dbo.AspNetUsers");
            DropForeignKey("dbo.BlogAdminTags", "AdminId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.BlogArticleTags", new[] { "BlogArticleId" });
            DropIndex("dbo.BlogArticleFiles", new[] { "ArticleId" });
            DropIndex("dbo.BlogArticles", new[] { "BlogAdminId" });
            DropIndex("dbo.BlogArticleComments", new[] { "BlogArticleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.BlogAdminTags", new[] { "AdminId" });
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.BlogArticleTags");
            DropTable("dbo.BlogArticleFiles");
            DropTable("dbo.BlogArticles");
            DropTable("dbo.BlogArticleComments");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.BlogAdminTags");
        }
    }
}
