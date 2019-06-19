namespace Dominio.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fk_barrio_en_vivienda : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Vivienda", name: "Barrio_BarrioId", newName: "BarrioId");
            RenameIndex(table: "dbo.Vivienda", name: "IX_Barrio_BarrioId", newName: "IX_BarrioId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Vivienda", name: "IX_BarrioId", newName: "IX_Barrio_BarrioId");
            RenameColumn(table: "dbo.Vivienda", name: "BarrioId", newName: "Barrio_BarrioId");
        }
    }
}
