namespace MVCRehber.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Rehber : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Kisis",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Ad = c.String(),
                        Soyad = c.String(),
                        tel_no = c.String(),
                        email = c.String(),
                        SehirId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Sehirs", t => t.SehirId, cascadeDelete: true)
                .Index(t => t.SehirId);
            
            CreateTable(
                "dbo.Sehirs",
                c => new
                    {
                        SehirId = c.Int(nullable: false, identity: true),
                        SehirAdi = c.String(),
                        UlkeAdi = c.String(),
                    })
                .PrimaryKey(t => t.SehirId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Kisis", "SehirId", "dbo.Sehirs");
            DropIndex("dbo.Kisis", new[] { "SehirId" });
            DropTable("dbo.Sehirs");
            DropTable("dbo.Kisis");
        }
    }
}
