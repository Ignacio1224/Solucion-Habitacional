namespace Dominio.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class cambio_de_nombre_atributos : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Vivienda", "cant_banio", c => c.Int(nullable: false));
            AddColumn("dbo.Vivienda", "cant_dormitorio", c => c.Int(nullable: false));
            DropColumn("dbo.Vivienda", "banios");
            DropColumn("dbo.Vivienda", "dormitorios");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Vivienda", "dormitorios", c => c.Int(nullable: false));
            AddColumn("dbo.Vivienda", "banios", c => c.Int(nullable: false));
            DropColumn("dbo.Vivienda", "cant_dormitorio");
            DropColumn("dbo.Vivienda", "cant_banio");
        }
    }
}
