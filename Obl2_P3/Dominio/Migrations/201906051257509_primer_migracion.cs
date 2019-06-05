namespace Dominio.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class primer_migracion : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Barrio",
                c => new
                    {
                        id_barrio = c.Int(nullable: false, identity: true),
                        nombre_barrio = c.String(nullable: false, maxLength: 50),
                        descripcion = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.id_barrio)
                .Index(t => t.nombre_barrio, unique: true);
            
            CreateTable(
                "dbo.Vivienda",
                c => new
                    {
                        id_vivienda = c.Int(nullable: false),
                        estado = c.String(nullable: false),
                        calle = c.String(nullable: false),
                        nro_puerta = c.Int(nullable: false),
                        descripcion = c.String(nullable: false),
                        banios = c.Int(nullable: false),
                        dormitorios = c.Int(nullable: false),
                        metraje = c.Decimal(nullable: false, precision: 18, scale: 2),
                        anio_construccion = c.Int(nullable: false),
                        precio_final = c.Decimal(nullable: false, precision: 18, scale: 2),
                        moneda_nombre_parametro = c.String(nullable: false, maxLength: 128),
                        barrio_id_barrio = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id_vivienda)
                .ForeignKey("dbo.Parametro", t => t.moneda_nombre_parametro, cascadeDelete: true)
                .ForeignKey("dbo.Sorteo", t => t.id_vivienda)
                .ForeignKey("dbo.Barrio", t => t.barrio_id_barrio, cascadeDelete: true)
                .Index(t => t.id_vivienda)
                .Index(t => t.moneda_nombre_parametro)
                .Index(t => t.barrio_id_barrio);
            
            CreateTable(
                "dbo.Parametro",
                c => new
                    {
                        nombre_parametro = c.String(nullable: false, maxLength: 128),
                        valor = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.nombre_parametro);
            
            CreateTable(
                "dbo.Sorteo",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        fecha = c.DateTime(nullable: false),
                        realizado = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.Usuario",
                c => new
                    {
                        cedula = c.String(nullable: false, maxLength: 9),
                        clave = c.String(nullable: false),
                        id_postulante = c.Int(),
                        nombre = c.String(maxLength: 50),
                        apellido = c.String(maxLength: 50),
                        email = c.String(maxLength: 254),
                        fecha_nac = c.DateTime(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                        sorteo_id = c.Int(),
                    })
                .PrimaryKey(t => t.cedula)
                .ForeignKey("dbo.Sorteo", t => t.sorteo_id)
                .Index(t => t.email, unique: true)
                .Index(t => t.sorteo_id);
            
            CreateTable(
                "dbo.SorteoPostulante",
                c => new
                    {
                        Sorteo_id = c.Int(nullable: false),
                        Postulante_cedula = c.String(nullable: false, maxLength: 9),
                    })
                .PrimaryKey(t => new { t.Sorteo_id, t.Postulante_cedula })
                .ForeignKey("dbo.Sorteo", t => t.Sorteo_id, cascadeDelete: true)
                .ForeignKey("dbo.Usuario", t => t.Postulante_cedula, cascadeDelete: true)
                .Index(t => t.Sorteo_id)
                .Index(t => t.Postulante_cedula);
            
            CreateTable(
                "dbo.ViviendaNueva",
                c => new
                    {
                        id_vivienda = c.Int(nullable: false),
                        id_vivienda_nueva = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id_vivienda)
                .ForeignKey("dbo.Vivienda", t => t.id_vivienda)
                .Index(t => t.id_vivienda);
            
            CreateTable(
                "dbo.ViviendaUsada",
                c => new
                    {
                        id_vivienda = c.Int(nullable: false),
                        id_vivienda_usada = c.Int(nullable: false),
                        monto_contribucion = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.id_vivienda)
                .ForeignKey("dbo.Vivienda", t => t.id_vivienda)
                .Index(t => t.id_vivienda);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ViviendaUsada", "id_vivienda", "dbo.Vivienda");
            DropForeignKey("dbo.ViviendaNueva", "id_vivienda", "dbo.Vivienda");
            DropForeignKey("dbo.Vivienda", "barrio_id_barrio", "dbo.Barrio");
            DropForeignKey("dbo.Vivienda", "id_vivienda", "dbo.Sorteo");
            DropForeignKey("dbo.SorteoPostulante", "Postulante_cedula", "dbo.Usuario");
            DropForeignKey("dbo.SorteoPostulante", "Sorteo_id", "dbo.Sorteo");
            DropForeignKey("dbo.Usuario", "sorteo_id", "dbo.Sorteo");
            DropForeignKey("dbo.Vivienda", "moneda_nombre_parametro", "dbo.Parametro");
            DropIndex("dbo.ViviendaUsada", new[] { "id_vivienda" });
            DropIndex("dbo.ViviendaNueva", new[] { "id_vivienda" });
            DropIndex("dbo.SorteoPostulante", new[] { "Postulante_cedula" });
            DropIndex("dbo.SorteoPostulante", new[] { "Sorteo_id" });
            DropIndex("dbo.Usuario", new[] { "sorteo_id" });
            DropIndex("dbo.Usuario", new[] { "email" });
            DropIndex("dbo.Vivienda", new[] { "barrio_id_barrio" });
            DropIndex("dbo.Vivienda", new[] { "moneda_nombre_parametro" });
            DropIndex("dbo.Vivienda", new[] { "id_vivienda" });
            DropIndex("dbo.Barrio", new[] { "nombre_barrio" });
            DropTable("dbo.ViviendaUsada");
            DropTable("dbo.ViviendaNueva");
            DropTable("dbo.SorteoPostulante");
            DropTable("dbo.Usuario");
            DropTable("dbo.Sorteo");
            DropTable("dbo.Parametro");
            DropTable("dbo.Vivienda");
            DropTable("dbo.Barrio");
        }
    }
}
