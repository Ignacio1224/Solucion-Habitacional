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
                        estado = c.String(nullable: false),
                        calle = c.String(nullable: false),
                        nro_puerta = c.Int(nullable: false),
                        descripcion = c.String(nullable: false),
                        cant_banio = c.Int(nullable: false),
                        cant_dormitorio = c.Int(nullable: false),
                        metraje = c.Decimal(nullable: false, precision: 18, scale: 2),
                        anio_construccion = c.Int(nullable: false),
                        precio_final = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Parametro_ParametroId = c.Int(),
                        Barrio_BarrioId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ViviendaId)
                .ForeignKey("dbo.Parametro", t => t.Parametro_ParametroId)
                .ForeignKey("dbo.Barrio", t => t.Barrio_BarrioId, cascadeDelete: true)
                .Index(t => t.Parametro_ParametroId)
                .Index(t => t.Barrio_BarrioId);
            
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
                "dbo.Sorteo",
                c => new
                    {
                        SorteoId = c.Int(nullable: false),
                        fecha = c.DateTime(nullable: false),
                        realizado = c.Boolean(nullable: false),
                        Postulante_UsuarioId = c.Int(),
                        Postulante_UsuarioId1 = c.Int(),
                    })
                .PrimaryKey(t => t.SorteoId)
                .ForeignKey("dbo.Usuario", t => t.Postulante_UsuarioId)
                .ForeignKey("dbo.Usuario", t => t.Postulante_UsuarioId1)
                .ForeignKey("dbo.Vivienda", t => t.SorteoId)
                .Index(t => t.SorteoId)
                .Index(t => t.Postulante_UsuarioId)
                .Index(t => t.Postulante_UsuarioId1);
            
            CreateTable(
                "dbo.Usuario",
                c => new
                    {
                        UsuarioId = c.Int(nullable: false, identity: true),
                        cedula = c.String(nullable: false, maxLength: 9),
                        clave = c.String(nullable: false),
                        nombre = c.String(maxLength: 50),
                        apellido = c.String(maxLength: 50),
                        email = c.String(maxLength: 254),
                        fecha_nac = c.DateTime(),
                        SorteoID = c.Int(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                        Sorteo_SorteoId = c.Int(),
                    })
                .PrimaryKey(t => t.UsuarioId)
                .ForeignKey("dbo.Sorteo", t => t.SorteoID, cascadeDelete: true)
                .ForeignKey("dbo.Sorteo", t => t.Sorteo_SorteoId)
                .Index(t => t.email, unique: true)
                .Index(t => t.SorteoID)
                .Index(t => t.Sorteo_SorteoId);
            
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
            DropForeignKey("dbo.Vivienda", "Barrio_BarrioId", "dbo.Barrio");
            DropForeignKey("dbo.Sorteo", "SorteoId", "dbo.Vivienda");
            DropForeignKey("dbo.Usuario", "Sorteo_SorteoId", "dbo.Sorteo");
            DropForeignKey("dbo.Sorteo", "Postulante_UsuarioId1", "dbo.Usuario");
            DropForeignKey("dbo.Sorteo", "Postulante_UsuarioId", "dbo.Usuario");
            DropForeignKey("dbo.Usuario", "SorteoID", "dbo.Sorteo");
            DropForeignKey("dbo.Vivienda", "Parametro_ParametroId", "dbo.Parametro");
            DropIndex("dbo.ViviendaUsada", new[] { "ViviendaId" });
            DropIndex("dbo.ViviendaNueva", new[] { "ViviendaId" });
            DropIndex("dbo.Usuario", new[] { "Sorteo_SorteoId" });
            DropIndex("dbo.Usuario", new[] { "SorteoID" });
            DropIndex("dbo.Usuario", new[] { "email" });
            DropIndex("dbo.Sorteo", new[] { "Postulante_UsuarioId1" });
            DropIndex("dbo.Sorteo", new[] { "Postulante_UsuarioId" });
            DropIndex("dbo.Sorteo", new[] { "SorteoId" });
            DropIndex("dbo.Parametro", new[] { "nombre_parametro" });
            DropIndex("dbo.Vivienda", new[] { "Barrio_BarrioId" });
            DropIndex("dbo.Vivienda", new[] { "Parametro_ParametroId" });
            DropIndex("dbo.Barrio", new[] { "nombre_barrio" });
            DropTable("dbo.ViviendaUsada");
            DropTable("dbo.ViviendaNueva");
            DropTable("dbo.Usuario");
            DropTable("dbo.Sorteo");
            DropTable("dbo.Parametro");
            DropTable("dbo.Vivienda");
            DropTable("dbo.Barrio");
        }
    }
}
