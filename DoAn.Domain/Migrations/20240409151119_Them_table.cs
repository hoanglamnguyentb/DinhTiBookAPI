using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DoAn.Domain.Migrations
{
    /// <inheritdoc />
    public partial class Them_table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "HinhAnh",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdSach = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Image_url = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HinhAnh", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NhaXuatBan",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TenNXB = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NhaXuatBan", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NhomDoTuoi",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TenNhom = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    MoTaDoTuoi = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NhomDoTuoi", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SanPham",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TenSach = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    TenTacGia = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    IdNhaXUatBan = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NamXuatBan = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdDanhMuc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GiaTien = table.Column<double>(type: "float", nullable: false),
                    SoLuongTon = table.Column<int>(type: "int", nullable: false),
                    MoTaSach = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdNhomDoTuoi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NoiBat = table.Column<bool>(type: "bit", nullable: false),
                    GiamGia = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SanPham", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HinhAnh");

            migrationBuilder.DropTable(
                name: "NhaXuatBan");

            migrationBuilder.DropTable(
                name: "NhomDoTuoi");

            migrationBuilder.DropTable(
                name: "SanPham");
        }
    }
}
