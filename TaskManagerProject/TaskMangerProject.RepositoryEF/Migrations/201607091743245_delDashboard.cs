namespace TaskManagerProject.RepositoryEF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class delDashboard : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.DashboardWidgets", "UserId", "dbo.MyUsers");
            DropIndex("dbo.DashboardWidgets", new[] { "UserId" });
            DropTable("dbo.DashboardWidgets");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.DashboardWidgets",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Position = c.Int(nullable: false),
                        Size = c.Int(nullable: false),
                        DisplayName = c.String(),
                        IsVisible = c.Boolean(nullable: false),
                        UserId = c.Int(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        DateCreated = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateIndex("dbo.DashboardWidgets", "UserId");
            AddForeignKey("dbo.DashboardWidgets", "UserId", "dbo.MyUsers", "ID", cascadeDelete: true);
        }
    }
}
