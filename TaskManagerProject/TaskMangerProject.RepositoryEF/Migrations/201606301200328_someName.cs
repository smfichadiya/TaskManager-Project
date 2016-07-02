namespace TaskManagerProject.RepositoryEF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class someName : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MyUsers", "AppUserId", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.MyUsers", "AppUserId");
        }
    }
}
