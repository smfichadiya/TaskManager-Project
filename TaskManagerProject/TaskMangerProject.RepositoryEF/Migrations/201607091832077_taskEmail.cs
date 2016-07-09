namespace TaskManagerProject.RepositoryEF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class taskEmail : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TaskComments", "userEmail", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.TaskComments", "userEmail");
        }
    }
}
