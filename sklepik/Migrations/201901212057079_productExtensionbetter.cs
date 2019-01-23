namespace sklepik.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class productExtensionbetter : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Description = c.String(),
                        Vat = c.Int(nullable: false),
                        IsHidden = c.Boolean(nullable: false),
                        EmailExpert = c.String(),
                        Image = c.Binary(),
                        ImageSmall = c.Binary(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Products");
        }
    }
}
