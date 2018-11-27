using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BedeThirteen.Data.Migrations
{
    public partial class TransactionTypeFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_TransactionTypes_TransactionTypeId",
                table: "Transactions");

            migrationBuilder.DeleteData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: new Guid("9c160898-9e61-4c42-a2a9-cc72fd5c5bc6"));

            migrationBuilder.DeleteData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: new Guid("d63926c8-2134-4cde-b93a-3126ac7f8e31"));

            migrationBuilder.DeleteData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: new Guid("f1b04f02-25b5-4527-80b3-5c32bcc164b9"));

            migrationBuilder.DeleteData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: new Guid("f5bbe226-9f4d-4636-a4a7-36e372f4b2dc"));

            migrationBuilder.AlterColumn<Guid>(
                name: "TransactionTypeId",
                table: "Transactions",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "Currencies",
                columns: new[] { "Id", "CreatedOn", "DeletedOn", "IsDeleted", "ModifiedOn", "Name" },
                values: new object[,]
                {
                    { new Guid("63cc4be3-29fa-4481-8108-fc94f5a52241"), null, null, false, null, "EUR" },
                    { new Guid("7bcad791-bf67-4fe3-996e-7f6fb2600b08"), null, null, false, null, "USD" },
                    { new Guid("0e211220-bdc7-4d85-bef0-271525edc8bb"), null, null, false, null, "BGN" },
                    { new Guid("76a03fa8-ef1b-43e5-aee7-5edb9e5c2882"), null, null, false, null, "GBP" }
                });

            migrationBuilder.InsertData(
                table: "TransactionTypes",
                columns: new[] { "Id", "CreatedOn", "DeletedOn", "IsDeleted", "ModifiedOn", "Name" },
                values: new object[] { new Guid("d6d1151f-e908-4da6-a485-b8318f1b5e47"), null, null, false, null, "Deposit" });

            migrationBuilder.CreateIndex(
                name: "IX_TransactionTypes_Name",
                table: "TransactionTypes",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Currencies_Name",
                table: "Currencies",
                column: "Name",
                unique: true,
                filter: "[Name] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_TransactionTypes_TransactionTypeId",
                table: "Transactions",
                column: "TransactionTypeId",
                principalTable: "TransactionTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_TransactionTypes_TransactionTypeId",
                table: "Transactions");

            migrationBuilder.DropIndex(
                name: "IX_TransactionTypes_Name",
                table: "TransactionTypes");

            migrationBuilder.DropIndex(
                name: "IX_Currencies_Name",
                table: "Currencies");

            migrationBuilder.DeleteData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: new Guid("0e211220-bdc7-4d85-bef0-271525edc8bb"));

            migrationBuilder.DeleteData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: new Guid("63cc4be3-29fa-4481-8108-fc94f5a52241"));

            migrationBuilder.DeleteData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: new Guid("76a03fa8-ef1b-43e5-aee7-5edb9e5c2882"));

            migrationBuilder.DeleteData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: new Guid("7bcad791-bf67-4fe3-996e-7f6fb2600b08"));

            migrationBuilder.DeleteData(
                table: "TransactionTypes",
                keyColumn: "Id",
                keyValue: new Guid("d6d1151f-e908-4da6-a485-b8318f1b5e47"));

            migrationBuilder.AlterColumn<Guid>(
                name: "TransactionTypeId",
                table: "Transactions",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.InsertData(
                table: "Currencies",
                columns: new[] { "Id", "CreatedOn", "DeletedOn", "IsDeleted", "ModifiedOn", "Name" },
                values: new object[,]
                {
                    { new Guid("f5bbe226-9f4d-4636-a4a7-36e372f4b2dc"), null, null, false, null, "EUR" },
                    { new Guid("d63926c8-2134-4cde-b93a-3126ac7f8e31"), null, null, false, null, "USD" },
                    { new Guid("9c160898-9e61-4c42-a2a9-cc72fd5c5bc6"), null, null, false, null, "BGN" },
                    { new Guid("f1b04f02-25b5-4527-80b3-5c32bcc164b9"), null, null, false, null, "GBP" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_TransactionTypes_TransactionTypeId",
                table: "Transactions",
                column: "TransactionTypeId",
                principalTable: "TransactionTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
