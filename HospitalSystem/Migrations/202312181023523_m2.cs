namespace HospitalSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class m2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Patients", "CurRoom_ID", "dbo.Rooms");
            DropIndex("dbo.Patients", new[] { "CurRoom_ID" });
            DropColumn("dbo.Patients", "CurRoom_ID");
            DropTable("dbo.Rooms");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Rooms",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Code = c.String(),
                        Location = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            AddColumn("dbo.Patients", "CurRoom_ID", c => c.Int());
            CreateIndex("dbo.Patients", "CurRoom_ID");
            AddForeignKey("dbo.Patients", "CurRoom_ID", "dbo.Rooms", "ID");
        }
    }
}
