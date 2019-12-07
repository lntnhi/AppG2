namespace AppG2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class h : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Username = c.String(nullable: false, maxLength: 128),
                        Password = c.String(),
                        FullName = c.String(),
                    })
                .PrimaryKey(t => t.Username);
            
            AddColumn("dbo.Contacts", "Username", c => c.String(maxLength: 128));
            CreateIndex("dbo.Contacts", "Username");
            AddForeignKey("dbo.Contacts", "Username", "dbo.Users", "Username");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Contacts", "Username", "dbo.Users");
            DropIndex("dbo.Contacts", new[] { "Username" });
            DropColumn("dbo.Contacts", "Username");
            DropTable("dbo.Users");
        }
    }
}
