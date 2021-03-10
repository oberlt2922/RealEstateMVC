using Microsoft.EntityFrameworkCore.Migrations;

namespace RealEstateMVC.Migrations
{
    public partial class imageDataUrl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "imageDataUrl",
                table: "Image",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "imageDataUrl",
                table: "Image");
        }
    }
}
