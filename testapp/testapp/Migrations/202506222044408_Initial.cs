namespace testapp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.Todos", newName: "TodoDB");
        }
    }
}
