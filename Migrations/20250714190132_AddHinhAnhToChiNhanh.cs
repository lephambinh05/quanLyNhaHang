using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace nhaHang.Migrations
{
    /// <inheritdoc />
    public partial class AddHinhAnhToChiNhanh : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "HinhAnh",
                table: "ChiNhanhs",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HinhAnh",
                table: "ChiNhanhs");
        }
    }
}
