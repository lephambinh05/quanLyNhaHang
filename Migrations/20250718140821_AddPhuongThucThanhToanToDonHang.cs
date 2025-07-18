﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace nhaHang.Migrations
{
    /// <inheritdoc />
    public partial class AddPhuongThucThanhToanToDonHang : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PhuongThucThanhToan",
                table: "DonHangs",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PhuongThucThanhToan",
                table: "DonHangs");
        }
    }
}
