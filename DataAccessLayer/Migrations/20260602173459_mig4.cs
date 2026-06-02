using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class mig4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EfFooter",
                columns: table => new
                {
                    FooterId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FooterShopBlogContact = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FooterLinks = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FooterSbscribe = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FooterEnAlt = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EfFooter", x => x.FooterId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EfFooter");
        }
    }
}
