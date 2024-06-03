using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DoAn.Domain.Migrations
{
    /// <inheritdoc />
    public partial class big_update : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "SanPham",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "SanPham",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedID",
                table: "SanPham",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeleteBy",
                table: "SanPham",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "DeleteId",
                table: "SanPham",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeleteTime",
                table: "SanPham",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDelete",
                table: "SanPham",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "SanPham",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedDate",
                table: "SanPham",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedID",
                table: "SanPham",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "NhomDoTuoi",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "NhomDoTuoi",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedID",
                table: "NhomDoTuoi",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeleteBy",
                table: "NhomDoTuoi",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "DeleteId",
                table: "NhomDoTuoi",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeleteTime",
                table: "NhomDoTuoi",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDelete",
                table: "NhomDoTuoi",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "NhomDoTuoi",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedDate",
                table: "NhomDoTuoi",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedID",
                table: "NhomDoTuoi",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "NhaXuatBan",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "NhaXuatBan",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedID",
                table: "NhaXuatBan",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeleteBy",
                table: "NhaXuatBan",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "DeleteId",
                table: "NhaXuatBan",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeleteTime",
                table: "NhaXuatBan",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDelete",
                table: "NhaXuatBan",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "NhaXuatBan",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedDate",
                table: "NhaXuatBan",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedID",
                table: "NhaXuatBan",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "HinhAnh",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "HinhAnh",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedID",
                table: "HinhAnh",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeleteBy",
                table: "HinhAnh",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "DeleteId",
                table: "HinhAnh",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeleteTime",
                table: "HinhAnh",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDelete",
                table: "HinhAnh",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "HinhAnh",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedDate",
                table: "HinhAnh",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedID",
                table: "HinhAnh",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "FileManager",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "FileManager",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedID",
                table: "FileManager",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeleteBy",
                table: "FileManager",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "DeleteId",
                table: "FileManager",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeleteTime",
                table: "FileManager",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDelete",
                table: "FileManager",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "FileManager",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedDate",
                table: "FileManager",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedID",
                table: "FileManager",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "DanhMucs",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "DanhMucs",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedID",
                table: "DanhMucs",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeleteBy",
                table: "DanhMucs",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "DeleteId",
                table: "DanhMucs",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeleteTime",
                table: "DanhMucs",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDelete",
                table: "DanhMucs",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "DanhMucs",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedDate",
                table: "DanhMucs",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedID",
                table: "DanhMucs",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "AppUserRole",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "AppUserRole",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedID",
                table: "AppUserRole",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeleteBy",
                table: "AppUserRole",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "DeleteId",
                table: "AppUserRole",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeleteTime",
                table: "AppUserRole",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDelete",
                table: "AppUserRole",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "AppUserRole",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedDate",
                table: "AppUserRole",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedID",
                table: "AppUserRole",
                type: "uniqueidentifier",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "SanPham");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "SanPham");

            migrationBuilder.DropColumn(
                name: "CreatedID",
                table: "SanPham");

            migrationBuilder.DropColumn(
                name: "DeleteBy",
                table: "SanPham");

            migrationBuilder.DropColumn(
                name: "DeleteId",
                table: "SanPham");

            migrationBuilder.DropColumn(
                name: "DeleteTime",
                table: "SanPham");

            migrationBuilder.DropColumn(
                name: "IsDelete",
                table: "SanPham");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "SanPham");

            migrationBuilder.DropColumn(
                name: "UpdatedDate",
                table: "SanPham");

            migrationBuilder.DropColumn(
                name: "UpdatedID",
                table: "SanPham");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "NhomDoTuoi");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "NhomDoTuoi");

            migrationBuilder.DropColumn(
                name: "CreatedID",
                table: "NhomDoTuoi");

            migrationBuilder.DropColumn(
                name: "DeleteBy",
                table: "NhomDoTuoi");

            migrationBuilder.DropColumn(
                name: "DeleteId",
                table: "NhomDoTuoi");

            migrationBuilder.DropColumn(
                name: "DeleteTime",
                table: "NhomDoTuoi");

            migrationBuilder.DropColumn(
                name: "IsDelete",
                table: "NhomDoTuoi");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "NhomDoTuoi");

            migrationBuilder.DropColumn(
                name: "UpdatedDate",
                table: "NhomDoTuoi");

            migrationBuilder.DropColumn(
                name: "UpdatedID",
                table: "NhomDoTuoi");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "NhaXuatBan");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "NhaXuatBan");

            migrationBuilder.DropColumn(
                name: "CreatedID",
                table: "NhaXuatBan");

            migrationBuilder.DropColumn(
                name: "DeleteBy",
                table: "NhaXuatBan");

            migrationBuilder.DropColumn(
                name: "DeleteId",
                table: "NhaXuatBan");

            migrationBuilder.DropColumn(
                name: "DeleteTime",
                table: "NhaXuatBan");

            migrationBuilder.DropColumn(
                name: "IsDelete",
                table: "NhaXuatBan");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "NhaXuatBan");

            migrationBuilder.DropColumn(
                name: "UpdatedDate",
                table: "NhaXuatBan");

            migrationBuilder.DropColumn(
                name: "UpdatedID",
                table: "NhaXuatBan");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "HinhAnh");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "HinhAnh");

            migrationBuilder.DropColumn(
                name: "CreatedID",
                table: "HinhAnh");

            migrationBuilder.DropColumn(
                name: "DeleteBy",
                table: "HinhAnh");

            migrationBuilder.DropColumn(
                name: "DeleteId",
                table: "HinhAnh");

            migrationBuilder.DropColumn(
                name: "DeleteTime",
                table: "HinhAnh");

            migrationBuilder.DropColumn(
                name: "IsDelete",
                table: "HinhAnh");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "HinhAnh");

            migrationBuilder.DropColumn(
                name: "UpdatedDate",
                table: "HinhAnh");

            migrationBuilder.DropColumn(
                name: "UpdatedID",
                table: "HinhAnh");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "FileManager");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "FileManager");

            migrationBuilder.DropColumn(
                name: "CreatedID",
                table: "FileManager");

            migrationBuilder.DropColumn(
                name: "DeleteBy",
                table: "FileManager");

            migrationBuilder.DropColumn(
                name: "DeleteId",
                table: "FileManager");

            migrationBuilder.DropColumn(
                name: "DeleteTime",
                table: "FileManager");

            migrationBuilder.DropColumn(
                name: "IsDelete",
                table: "FileManager");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "FileManager");

            migrationBuilder.DropColumn(
                name: "UpdatedDate",
                table: "FileManager");

            migrationBuilder.DropColumn(
                name: "UpdatedID",
                table: "FileManager");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "DanhMucs");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "DanhMucs");

            migrationBuilder.DropColumn(
                name: "CreatedID",
                table: "DanhMucs");

            migrationBuilder.DropColumn(
                name: "DeleteBy",
                table: "DanhMucs");

            migrationBuilder.DropColumn(
                name: "DeleteId",
                table: "DanhMucs");

            migrationBuilder.DropColumn(
                name: "DeleteTime",
                table: "DanhMucs");

            migrationBuilder.DropColumn(
                name: "IsDelete",
                table: "DanhMucs");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "DanhMucs");

            migrationBuilder.DropColumn(
                name: "UpdatedDate",
                table: "DanhMucs");

            migrationBuilder.DropColumn(
                name: "UpdatedID",
                table: "DanhMucs");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "AppUserRole");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "AppUserRole");

            migrationBuilder.DropColumn(
                name: "CreatedID",
                table: "AppUserRole");

            migrationBuilder.DropColumn(
                name: "DeleteBy",
                table: "AppUserRole");

            migrationBuilder.DropColumn(
                name: "DeleteId",
                table: "AppUserRole");

            migrationBuilder.DropColumn(
                name: "DeleteTime",
                table: "AppUserRole");

            migrationBuilder.DropColumn(
                name: "IsDelete",
                table: "AppUserRole");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "AppUserRole");

            migrationBuilder.DropColumn(
                name: "UpdatedDate",
                table: "AppUserRole");

            migrationBuilder.DropColumn(
                name: "UpdatedID",
                table: "AppUserRole");
        }
    }
}
