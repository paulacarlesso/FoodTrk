namespace Foodtruck.Negocio.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Bebidas",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Tamanho = c.Single(nullable: false),
                        Nome = c.String(),
                        Valor = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Pedidos",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        DataCompra = c.DateTime(nullable: false),
                        Encerrado = c.Boolean(nullable: false),
                        Cliente_Id = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Clientes", t => t.Cliente_Id)
                .Index(t => t.Cliente_Id);
            
            CreateTable(
                "dbo.Clientes",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        CPF = c.String(),
                        Nome = c.String(),
                        Email = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Lanches",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Nome = c.String(),
                        Valor = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PedidoBebidas",
                c => new
                    {
                        Pedido_Id = c.Long(nullable: false),
                        Bebida_Id = c.Long(nullable: false),
                    })
                .PrimaryKey(t => new { t.Pedido_Id, t.Bebida_Id })
                .ForeignKey("dbo.Pedidos", t => t.Pedido_Id, cascadeDelete: true)
                .ForeignKey("dbo.Bebidas", t => t.Bebida_Id, cascadeDelete: true)
                .Index(t => t.Pedido_Id)
                .Index(t => t.Bebida_Id);
            
            CreateTable(
                "dbo.LanchePedidoes",
                c => new
                    {
                        Lanche_Id = c.Long(nullable: false),
                        Pedido_Id = c.Long(nullable: false),
                    })
                .PrimaryKey(t => new { t.Lanche_Id, t.Pedido_Id })
                .ForeignKey("dbo.Lanches", t => t.Lanche_Id, cascadeDelete: true)
                .ForeignKey("dbo.Pedidos", t => t.Pedido_Id, cascadeDelete: true)
                .Index(t => t.Lanche_Id)
                .Index(t => t.Pedido_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.LanchePedidoes", "Pedido_Id", "dbo.Pedidos");
            DropForeignKey("dbo.LanchePedidoes", "Lanche_Id", "dbo.Lanches");
            DropForeignKey("dbo.Pedidos", "Cliente_Id", "dbo.Clientes");
            DropForeignKey("dbo.PedidoBebidas", "Bebida_Id", "dbo.Bebidas");
            DropForeignKey("dbo.PedidoBebidas", "Pedido_Id", "dbo.Pedidos");
            DropIndex("dbo.LanchePedidoes", new[] { "Pedido_Id" });
            DropIndex("dbo.LanchePedidoes", new[] { "Lanche_Id" });
            DropIndex("dbo.PedidoBebidas", new[] { "Bebida_Id" });
            DropIndex("dbo.PedidoBebidas", new[] { "Pedido_Id" });
            DropIndex("dbo.Pedidos", new[] { "Cliente_Id" });
            DropTable("dbo.LanchePedidoes");
            DropTable("dbo.PedidoBebidas");
            DropTable("dbo.Lanches");
            DropTable("dbo.Clientes");
            DropTable("dbo.Pedidos");
            DropTable("dbo.Bebidas");
        }
    }
}
