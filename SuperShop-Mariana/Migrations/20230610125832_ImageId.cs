using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SuperShop_Mariana.Migrations
{
    public partial class ImageId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "products");

            migrationBuilder.AddColumn<Guid>(
                name: "ImageId",
                table: "products",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageId",
                table: "products");

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "products",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
