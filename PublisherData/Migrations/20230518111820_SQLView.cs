using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PublisherData.Migrations
{
    public partial class SQLView : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                @"CREATE VIEW  BookWithCoverView
                  AS
                  SELECT b.Title, c.DesignIdeas
                  FROM Books b JOIN Covers c
                  on b.BookId = c.BookId
            ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP VIEW  AuthorsByArtist");
        }
    }
}
