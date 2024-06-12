using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DoAn.Domain.Migrations
{
    /// <inheritdoc />
    public partial class SoLuongDaBan : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SoLuongDaBan",
                table: "SanPham",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SoLuongDaBan",
                table: "SanPham");
        }
    }
}
