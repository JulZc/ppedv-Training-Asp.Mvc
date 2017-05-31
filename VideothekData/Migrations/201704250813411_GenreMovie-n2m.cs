namespace VideothekData.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class GenreMovien2m : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MovieGenre",
                c => new
                    {
                        MovieId = c.Int(nullable: false),
                        GenreId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.MovieId, t.GenreId })
                .ForeignKey("dbo.Movies", t => t.MovieId, cascadeDelete: true)
                .ForeignKey("dbo.Genres", t => t.GenreId, cascadeDelete: true)
                .Index(t => t.MovieId)
                .Index(t => t.GenreId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MovieGenre", "GenreId", "dbo.Genres");
            DropForeignKey("dbo.MovieGenre", "MovieId", "dbo.Movies");
            DropIndex("dbo.MovieGenre", new[] { "GenreId" });
            DropIndex("dbo.MovieGenre", new[] { "MovieId" });
            DropTable("dbo.MovieGenre");
        }
    }
}
