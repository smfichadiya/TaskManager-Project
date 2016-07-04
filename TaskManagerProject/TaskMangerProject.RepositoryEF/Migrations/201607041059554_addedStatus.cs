namespace TaskManagerProject.RepositoryEF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedStatus : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MyTasks", "status", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.MyTasks", "status");
        }
    }
}
