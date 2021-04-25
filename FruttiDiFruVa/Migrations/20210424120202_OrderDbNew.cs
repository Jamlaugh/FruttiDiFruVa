using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FruttiDiFruVa.Migrations
{
    public partial class OrderDbNew : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Recipient = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Amount = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeliveryDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Articles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Checked = table.Column<bool>(type: "bit", nullable: false),
                    ArticleName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MainArticleNumber = table.Column<int>(type: "int", nullable: false),
                    DetailArticleNumber = table.Column<int>(type: "int", nullable: false),
                    PackageSize = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ArticleGroupNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ArticleGroupName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OriginCountry = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TradeClass = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Colli = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Caliber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Variety = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OwnBrand = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SearchQuery = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RowVersion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OrderID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Articles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Articles_Orders_OrderID",
                        column: x => x.OrderID,
                        principalTable: "Orders",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Articles_OrderID",
                table: "Articles",
                column: "OrderID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Articles");

            migrationBuilder.DropTable(
                name: "Orders");
        }
    }
}
