using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PublisherData.Migrations
{
    public partial class StoreProc : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                @"CREATE PROCEDURE dbo.AuthorsPublishedinYearRange
                     @yearstart int,
                     @yearend int
                  AS
                  SELECT Distinct Authors.* FROM authors
                  LEFT JOIN Books ON Authors.Id = books.authorId
            ");

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
             "DROP PROCEDURE dbo.AuthorsPublishedinYearRange");

        }
    }
}
