namespace TaskManagerProject.RepositoryEF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class taskCommentDel : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.TaskComments", "TaskId", "dbo.MyTasks");
            DropIndex("dbo.TaskComments", new[] { "TaskId" });
            AlterColumn("dbo.TaskComments", "TaskId", c => c.Int());
            CreateIndex("dbo.TaskComments", "TaskId");
            AddForeignKey("dbo.TaskComments", "TaskId", "dbo.MyTasks", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TaskComments", "TaskId", "dbo.MyTasks");
            DropIndex("dbo.TaskComments", new[] { "TaskId" });
            AlterColumn("dbo.TaskComments", "TaskId", c => c.Int(nullable: false));
            CreateIndex("dbo.TaskComments", "TaskId");
            AddForeignKey("dbo.TaskComments", "TaskId", "dbo.MyTasks", "ID", cascadeDelete: true);
        }
    }
}
