namespace Dominio.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class segunda_migracion : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.ViviendaNueva", "id_vivienda_nueva");
            DropColumn("dbo.ViviendaUsada", "id_vivienda_usada");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ViviendaUsada", "id_vivienda_usada", c => c.Int(nullable: false));
            AddColumn("dbo.ViviendaNueva", "id_vivienda_nueva", c => c.Int(nullable: false));
        }
    }
}
