namespace Dominio.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Barrio",
                c => new
                    {
                        BarrioId = c.Int(nullable: false, identity: true),
                        nombre_barrio = c.String(nullable: false, maxLength: 50),
                        descripcion = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.BarrioId)
                .Index(t => t.nombre_barrio, unique: true);
            
            CreateTable(
                "dbo.Vivienda",
                c => new
                    {
                        ViviendaId = c.Int(nullable: false),
                        estado = c.Int(nullable: false),
                        calle = c.String(nullable: false),
                        nro_puerta = c.Int(nullable: false),
                        descripcion = c.String(nullable: false),
                        BarrioId = c.Int(nullable: false),
                        cant_banio = c.Int(nullable: false),
                        cant_dormitorio = c.Int(nullable: false),
                        metraje = c.Decimal(nullable: false, precision: 18, scale: 2),
                        anio_construccion = c.Int(nullable: false),
                        moneda = c.String(),
                        precio_final = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.ViviendaId)
                .ForeignKey("dbo.Barrio", t => t.BarrioId, cascadeDelete: true)
                .Index(t => t.BarrioId);
            
            CreateTable(
                "dbo.Sorteo",
                c => new
                    {
                        SorteoId = c.Int(nullable: false),
                        fecha = c.DateTime(nullable: false),
                        realizado = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.SorteoId)
                .ForeignKey("dbo.Postulante", t => t.SorteoId)
                .ForeignKey("dbo.Vivienda", t => t.SorteoId)
                .Index(t => t.SorteoId);
            
            CreateTable(
                "dbo.Usuario",
                c => new
                    {
                        UsuarioId = c.Int(nullable: false, identity: true),
                        cedula = c.String(nullable: false, maxLength: 9),
                        clave = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.UsuarioId);
            
            CreateTable(
                "dbo.Parametro",
                c => new
                    {
                        ParametroId = c.Int(nullable: false, identity: true),
                        nombre_parametro = c.String(nullable: false, maxLength: 254),
                        valor = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.ParametroId)
                .Index(t => t.nombre_parametro, unique: true);
            
            CreateTable(
                "dbo.Postulante_Sorteo",
                c => new
                    {
                        PostulanteId = c.Int(nullable: false),
                        SorteoId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.PostulanteId, t.SorteoId })
                .ForeignKey("dbo.Postulante", t => t.PostulanteId, cascadeDelete: true)
                .ForeignKey("dbo.Sorteo", t => t.SorteoId, cascadeDelete: true)
                .Index(t => t.PostulanteId)
                .Index(t => t.SorteoId);
            
            CreateTable(
                "dbo.Postulante",
                c => new
                    {
                        UsuarioId = c.Int(nullable: false),
                        nombre = c.String(nullable: false, maxLength: 50),
                        apellido = c.String(nullable: false, maxLength: 50),
                        email = c.String(nullable: false, maxLength: 254),
                        fecha_nac = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.UsuarioId)
                .ForeignKey("dbo.Usuario", t => t.UsuarioId)
                .Index(t => t.UsuarioId)
                .Index(t => t.email, unique: true);
            
            CreateTable(
                "dbo.ViviendaNueva",
                c => new
                    {
                        ViviendaId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ViviendaId)
                .ForeignKey("dbo.Vivienda", t => t.ViviendaId)
                .Index(t => t.ViviendaId);
            
            CreateTable(
                "dbo.ViviendaUsada",
                c => new
                    {
                        ViviendaId = c.Int(nullable: false),
                        monto_contribucion = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.ViviendaId)
                .ForeignKey("dbo.Vivienda", t => t.ViviendaId)
                .Index(t => t.ViviendaId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ViviendaUsada", "ViviendaId", "dbo.Vivienda");
            DropForeignKey("dbo.ViviendaNueva", "ViviendaId", "dbo.Vivienda");
            DropForeignKey("dbo.Postulante", "UsuarioId", "dbo.Usuario");
            DropForeignKey("dbo.Vivienda", "BarrioId", "dbo.Barrio");
            DropForeignKey("dbo.Sorteo", "SorteoId", "dbo.Vivienda");
            DropForeignKey("dbo.Sorteo", "SorteoId", "dbo.Postulante");
            DropForeignKey("dbo.Postulante_Sorteo", "SorteoId", "dbo.Sorteo");
            DropForeignKey("dbo.Postulante_Sorteo", "PostulanteId", "dbo.Postulante");
            DropIndex("dbo.ViviendaUsada", new[] { "ViviendaId" });
            DropIndex("dbo.ViviendaNueva", new[] { "ViviendaId" });
            DropIndex("dbo.Postulante", new[] { "email" });
            DropIndex("dbo.Postulante", new[] { "UsuarioId" });
            DropIndex("dbo.Postulante_Sorteo", new[] { "SorteoId" });
            DropIndex("dbo.Postulante_Sorteo", new[] { "PostulanteId" });
            DropIndex("dbo.Parametro", new[] { "nombre_parametro" });
            DropIndex("dbo.Sorteo", new[] { "SorteoId" });
            DropIndex("dbo.Vivienda", new[] { "BarrioId" });
            DropIndex("dbo.Barrio", new[] { "nombre_barrio" });
            DropTable("dbo.ViviendaUsada");
            DropTable("dbo.ViviendaNueva");
            DropTable("dbo.Postulante");
            DropTable("dbo.Postulante_Sorteo");
            DropTable("dbo.Parametro");
            DropTable("dbo.Usuario");
            DropTable("dbo.Sorteo");
            DropTable("dbo.Vivienda");
            DropTable("dbo.Barrio");
        }
    }
}
