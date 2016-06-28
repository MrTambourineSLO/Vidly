namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateGenres : DbMigration
    {
        public override void Up()
        {
            //Comedy,Action,Family,Romance
            Sql("INSERT INTO Genres(Id,GenreName) VALUES(1, 'Comedy')");
            Sql("INSERT INTO Genres(Id,GenreName) VALUES(2, 'Action')");
            Sql("INSERT INTO Genres(Id,GenreName) VALUES(3, 'Family')");
            Sql("INSERT INTO Genres(Id,GenreName) VALUES(4, 'Romance')");
        }
        
        public override void Down()
        {
        }
    }
}
