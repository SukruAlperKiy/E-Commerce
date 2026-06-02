using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class mig6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FooterEnAlt",
                table: "EfFooter");

            migrationBuilder.DropColumn(
                name: "FooterLinks",
                table: "EfFooter");

            migrationBuilder.RenameColumn(
                name: "FooterShopBlogContact",
                table: "EfFooter",
                newName: "FooterSaglinks");

            migrationBuilder.RenameColumn(
                name: "FooterSbscribe",
                table: "EfFooter",
                newName: "FooterSagLinkTitle");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FooterSaglinks",
                table: "EfFooter",
                newName: "FooterShopBlogContact");

            migrationBuilder.RenameColumn(
                name: "FooterSagLinkTitle",
                table: "EfFooter",
                newName: "FooterSbscribe");

            migrationBuilder.AddColumn<string>(
                name: "FooterEnAlt",
                table: "EfFooter",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FooterLinks",
                table: "EfFooter",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
