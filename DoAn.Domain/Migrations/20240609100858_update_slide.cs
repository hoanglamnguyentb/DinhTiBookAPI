﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DoAn.Domain.Migrations
{
    /// <inheritdoc />
    public partial class update_slide : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TenBanner",
                table: "QLSlide",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TenBanner",
                table: "QLSlide");
        }
    }
}
