namespace Dominio.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class index_on_usuario : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Postulante", "UsuarioId", "dbo.Usuario");
            DropPrimaryKey("dbo.Usuario");
            AlterColumn("dbo.Usuario", "UsuarioId", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Usuario", "UsuarioId");
            AddForeignKey("dbo.Postulante", "UsuarioId", "dbo.Usuario", "UsuarioId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Postulante", "UsuarioId", "dbo.Usuario");
            DropPrimaryKey("dbo.Usuario");
            AlterColumn("dbo.Usuario", "UsuarioId", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.Usuario", "UsuarioId");
            AddForeignKey("dbo.Postulante", "UsuarioId", "dbo.Usuario", "UsuarioId");
        }
    }
}
