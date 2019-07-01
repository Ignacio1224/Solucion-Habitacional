namespace Dominio.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class last_Migration : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Postulante", name: "Sorteo_SorteoId", newName: "SorteoGanado_SorteoId");
            RenameIndex(table: "dbo.Postulante", name: "IX_Sorteo_SorteoId", newName: "IX_SorteoGanado_SorteoId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Postulante", name: "IX_SorteoGanado_SorteoId", newName: "IX_Sorteo_SorteoId");
            RenameColumn(table: "dbo.Postulante", name: "SorteoGanado_SorteoId", newName: "Sorteo_SorteoId");
        }
    }
}
