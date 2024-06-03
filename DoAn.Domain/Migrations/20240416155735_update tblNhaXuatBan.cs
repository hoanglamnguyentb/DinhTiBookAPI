using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DoAn.Domain.Migrations
{
    /// <inheritdoc />
    public partial class updatetblNhaXuatBan : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IdNhaXUatBan",
                table: "SanPham",
                newName: "IdNhaXuatBan");

            migrationBuilder.AddColumn<string>(
                name: "MaNXB",
                table: "NhaXuatBan",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MaNXB",
                table: "NhaXuatBan");

            migrationBuilder.RenameColumn(
                name: "IdNhaXuatBan",
                table: "SanPham",
                newName: "IdNhaXUatBan");
        }
    }
}
