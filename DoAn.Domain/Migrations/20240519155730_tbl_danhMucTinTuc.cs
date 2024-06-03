﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DoAn.Domain.Migrations
{
    /// <inheritdoc />
    public partial class tbl_danhMucTinTuc : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DanhMucTinTuc",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MaDanhMuc = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TenDanhMuc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    CreatedID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    UpdatedID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDelete = table.Column<bool>(type: "bit", nullable: true),
                    DeleteTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeleteId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeleteBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DanhMucTinTuc", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DanhMucTinTuc");
        }
    }
}
