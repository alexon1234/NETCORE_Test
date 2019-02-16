using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace webapi.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Payments",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreditCardNumber_Value = table.Column<string>(nullable: false),
                    CardHolder_Value = table.Column<string>(nullable: false),
                    ExpirationDate_Value = table.Column<DateTime>(nullable: false),
                    Ammount_Value = table.Column<decimal>(nullable: false),
                    SecurityCode_Value = table.Column<string>(maxLength: 3, nullable: true),
                    PaymentStatus = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payments", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Payments");
        }
    }
}
