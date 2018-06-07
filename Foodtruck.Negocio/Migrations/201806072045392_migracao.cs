namespace Foodtruck.Negocio.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migracao : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Pedidos", "Encerrado");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Pedidos", "Encerrado", c => c.Boolean(nullable: false));
        }
    }
}
