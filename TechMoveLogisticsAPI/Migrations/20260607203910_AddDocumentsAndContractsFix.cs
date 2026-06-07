using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TechMoveLogisticsAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddDocumentsAndContractsFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FileUrl",
                table: "UploadedDocuments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FileUrl",
                table: "UploadedDocuments");
        }
    }
}
