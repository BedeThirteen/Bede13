using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BedeThirteen.Data.Migrations
{
    public partial class AddedTransactionTypeSeeding : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.InsertData(
                table: "Currencies",
                columns: new[] { "Id", "CreatedOn", "DeletedOn", "IsDeleted", "ModifiedOn", "Name" },
                values: new object[,]
                {
                    { new Guid("a3e67dbc-0a53-4a11-9aeb-50929027fe86"), null, null, false, null, "EUR" },
                    { new Guid("488c936d-f3f4-48e3-88bf-cbfcc1adf81a"), null, null, false, null, "USD" },
                    { new Guid("1549fd8c-7235-4248-97eb-230dc28b1942"), null, null, false, null, "BGN" },
                    { new Guid("5253e4f2-035e-4f9c-8ec8-cd0f0696f78c"), null, null, false, null, "GBP" }
                });

            migrationBuilder.InsertData(
                table: "TransactionTypes",
                columns: new[] { "Id", "CreatedOn", "DeletedOn", "IsDeleted", "ModifiedOn", "Name" },
                values: new object[,]
                {
                    { new Guid("7865ad1d-2bd6-4be0-a0af-996b6ee7376c"), null, null, false, null, "Deposit" },
                    { new Guid("cecf24b6-6be9-4dd2-a835-67641eae8d66"), null, null, false, null, "Win" },
                    { new Guid("4bd92933-6825-43fc-9a13-3f997d60daa7"), null, null, false, null, "Stake" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: new Guid("1549fd8c-7235-4248-97eb-230dc28b1942"));

            migrationBuilder.DeleteData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: new Guid("488c936d-f3f4-48e3-88bf-cbfcc1adf81a"));

            migrationBuilder.DeleteData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: new Guid("5253e4f2-035e-4f9c-8ec8-cd0f0696f78c"));

            migrationBuilder.DeleteData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: new Guid("a3e67dbc-0a53-4a11-9aeb-50929027fe86"));

            migrationBuilder.DeleteData(
                table: "TransactionTypes",
                keyColumn: "Id",
                keyValue: new Guid("4bd92933-6825-43fc-9a13-3f997d60daa7"));

            migrationBuilder.DeleteData(
                table: "TransactionTypes",
                keyColumn: "Id",
                keyValue: new Guid("7865ad1d-2bd6-4be0-a0af-996b6ee7376c"));

            migrationBuilder.DeleteData(
                table: "TransactionTypes",
                keyColumn: "Id",
                keyValue: new Guid("cecf24b6-6be9-4dd2-a835-67641eae8d66"));

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
        }
    }
}
