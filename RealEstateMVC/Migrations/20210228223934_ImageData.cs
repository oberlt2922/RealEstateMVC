using Microsoft.EntityFrameworkCore.Migrations;

namespace RealEstateMVC.Migrations
{
    public partial class ImageData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ImageDate",
                table: "Image",
                newName: "ImageData");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ImageData",
                table: "Image",
                newName: "ImageDate");
        }
    }
}
