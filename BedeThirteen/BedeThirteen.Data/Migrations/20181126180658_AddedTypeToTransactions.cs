using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BedeThirteen.Data.Migrations
{
    public partial class AddedTypeToTransactions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_TransactionTypes_TransactionTypeId",
                table: "Transactions");

            migrationBuilder.DropIndex(
                name: "IX_Transactions_TransactionTypeId",
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

            migrationBuilder.AlterColumn<string>(
                name: "TransactionTypeId",
                table: "Transactions",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "TransactionTypeId1",
                table: "Transactions",
                nullable: true);

            migrationBuilder.InsertData(
                table: "Currencies",
                columns: new[] { "Id", "CreatedOn", "DeletedOn", "IsDeleted", "ModifiedOn", "Name" },
                values: new object[,]
                {
                    { new Guid("2de33907-3501-4f53-bf6b-585c0f8b9103"), null, null, false, null, "EUR" },
                    { new Guid("bfa5694a-de5a-4e7c-aa0f-40b67c47bb71"), null, null, false, null, "USD" },
                    { new Guid("b5d8df24-0b7f-4e60-a606-b5be93452d16"), null, null, false, null, "BGN" },
                    { new Guid("9d8f48de-0b14-4421-bf96-06a3751c8644"), null, null, false, null, "GBP" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_TransactionTypeId1",
                table: "Transactions",
                column: "TransactionTypeId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_TransactionTypes_TransactionTypeId1",
                table: "Transactions",
                column: "TransactionTypeId1",
                principalTable: "TransactionTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_TransactionTypes_TransactionTypeId1",
                table: "Transactions");

            migrationBuilder.DropIndex(
                name: "IX_Transactions_TransactionTypeId1",
                table: "Transactions");

            migrationBuilder.DeleteData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: new Guid("2de33907-3501-4f53-bf6b-585c0f8b9103"));

            migrationBuilder.DeleteData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: new Guid("9d8f48de-0b14-4421-bf96-06a3751c8644"));

            migrationBuilder.DeleteData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: new Guid("b5d8df24-0b7f-4e60-a606-b5be93452d16"));

            migrationBuilder.DeleteData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: new Guid("bfa5694a-de5a-4e7c-aa0f-40b67c47bb71"));

            migrationBuilder.DropColumn(
                name: "TransactionTypeId1",
                table: "Transactions");

            migrationBuilder.AlterColumn<Guid>(
                name: "TransactionTypeId",
                table: "Transactions",
                nullable: true,
                oldClrType: typeof(string));

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

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_TransactionTypeId",
                table: "Transactions",
                column: "TransactionTypeId");

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
