using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BedeThirteen.Data.Migrations
{
    public partial class CurrencyNameIsTableNameAndAddedConversionRateColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Currencies_CurrencyId",
                table: "AspNetUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Currencies",
                table: "Currencies");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_CurrencyId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Currencies");

            migrationBuilder.AddColumn<decimal>(
                name: "ConversionRateToUSD",
                table: "Currencies",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "CurrencyName",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Currencies",
                table: "Currencies",
                column: "Name");

            migrationBuilder.UpdateData(
                table: "Currencies",
                keyColumn: "Name",
                keyValue: "BGN",
                column: "ConversionRateToUSD",
                value: 1m);

            migrationBuilder.UpdateData(
                table: "Currencies",
                keyColumn: "Name",
                keyValue: "EUR",
                column: "ConversionRateToUSD",
                value: 1m);

            migrationBuilder.UpdateData(
                table: "Currencies",
                keyColumn: "Name",
                keyValue: "GBP",
                column: "ConversionRateToUSD",
                value: 1m);

            migrationBuilder.UpdateData(
                table: "Currencies",
                keyColumn: "Name",
                keyValue: "USD",
                column: "ConversionRateToUSD",
                value: 1m);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_CurrencyName",
                table: "AspNetUsers",
                column: "CurrencyName");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Currencies_CurrencyName",
                table: "AspNetUsers",
                column: "CurrencyName",
                principalTable: "Currencies",
                principalColumn: "Name",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Currencies_CurrencyName",
                table: "AspNetUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Currencies",
                table: "Currencies");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_CurrencyName",
                table: "AspNetUsers");

            migrationBuilder.DeleteData(
                table: "Currencies",
                keyColumn: "Name",
                keyValue: "BGN");

            migrationBuilder.DeleteData(
                table: "Currencies",
                keyColumn: "Name",
                keyValue: "EUR");

            migrationBuilder.DeleteData(
                table: "Currencies",
                keyColumn: "Name",
                keyValue: "GBP");

            migrationBuilder.DeleteData(
                table: "Currencies",
                keyColumn: "Name",
                keyValue: "USD");

            migrationBuilder.DropColumn(
                name: "ConversionRateToUSD",
                table: "Currencies");

            migrationBuilder.DropColumn(
                name: "CurrencyName",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "Currencies",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_Currencies",
                table: "Currencies",
                column: "Id");

            migrationBuilder.InsertData(
                table: "Currencies",
                columns: new[] { "Id", "CreatedOn", "DeletedOn", "IsDeleted", "ModifiedOn", "Name" },
                values: new object[,]
                {
                    { new Guid("e8a817e6-c21e-452d-9f87-670de776737d"), null, null, false, null, "EUR" },
                    { new Guid("f046df30-72d3-43b7-a83c-f0c9a2ab3941"), null, null, false, null, "USD" },
                    { new Guid("f475b78d-9efe-4208-9ae0-7dd1ec5ed319"), null, null, false, null, "BGN" },
                    { new Guid("da4efe00-2faa-4171-a6f9-958b19a33e8b"), null, null, false, null, "GBP" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_CurrencyId",
                table: "AspNetUsers",
                column: "CurrencyId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Currencies_CurrencyId",
                table: "AspNetUsers",
                column: "CurrencyId",
                principalTable: "Currencies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
