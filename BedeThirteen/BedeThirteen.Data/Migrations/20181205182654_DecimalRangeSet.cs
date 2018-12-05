namespace BedeThirteen.Data.Migrations
{
    using System;
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class DecimalRangeSet : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: new Guid("2bd37350-b648-4d2f-b511-188897edf963"));

            migrationBuilder.DeleteData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: new Guid("3d449b15-e7a7-4344-97e1-354585461680"));

            migrationBuilder.DeleteData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: new Guid("9310f845-318a-4f1b-b1fd-c845df9e78c9"));

            migrationBuilder.DeleteData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: new Guid("ff4d9d9a-1f00-47cc-b589-5c467a343b67"));

            migrationBuilder.DeleteData(
                table: "TransactionTypes",
                keyColumn: "Id",
                keyValue: new Guid("047669c6-0d3c-4b53-8cbe-c4c67b3cfa50"));

            migrationBuilder.DeleteData(
                table: "TransactionTypes",
                keyColumn: "Id",
                keyValue: new Guid("1d67ab84-1ac0-438a-9c0a-6136e6264ba2"));

            migrationBuilder.DeleteData(
                table: "TransactionTypes",
                keyColumn: "Id",
                keyValue: new Guid("79d5466a-6dec-466e-94e4-68aa5b89bb5a"));

            migrationBuilder.DeleteData(
                table: "TransactionTypes",
                keyColumn: "Id",
                keyValue: new Guid("fa1355ae-76be-40ec-9cd3-d252e5088f66"));

            migrationBuilder.AlterColumn<decimal>(
                name: "Amount",
                table: "Transactions",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal));

            migrationBuilder.AlterColumn<decimal>(
                name: "Balance",
                table: "AspNetUsers",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal));

            migrationBuilder.InsertData(
                table: "Currencies",
                columns: new[] { "Id", "CreatedOn", "DeletedOn", "IsDeleted", "ModifiedOn", "Name" },
                values: new object[,]
                {
                    { new Guid("00823139-1422-4f85-aaf7-6a077e866985"), null, null, false, null, "EUR" },
                    { new Guid("a426bb10-1572-495b-b19a-cab14876e96e"), null, null, false, null, "USD" },
                    { new Guid("27ec70df-cd0c-4d55-b273-47542b34f239"), null, null, false, null, "BGN" },
                    { new Guid("5ad45dbc-18ac-4193-b6af-290cbedb5b34"), null, null, false, null, "GBP" }
                });

            migrationBuilder.InsertData(
                table: "TransactionTypes",
                columns: new[] { "Id", "CreatedOn", "DeletedOn", "IsDeleted", "ModifiedOn", "Name" },
                values: new object[,]
                {
                    { new Guid("1e9d12bb-3624-4873-b791-414062c57289"), null, null, false, null, "Withdraw" },
                    { new Guid("90f2c7c7-6879-484c-9fab-8c5826772f27"), null, null, false, null, "Deposit" },
                    { new Guid("6f39da3b-5b8f-42ba-afbc-70b569b563b0"), null, null, false, null, "Win" },
                    { new Guid("a566914c-7e8a-4456-baec-a32252d8d5e7"), null, null, false, null, "Stake" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: new Guid("00823139-1422-4f85-aaf7-6a077e866985"));

            migrationBuilder.DeleteData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: new Guid("27ec70df-cd0c-4d55-b273-47542b34f239"));

            migrationBuilder.DeleteData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: new Guid("5ad45dbc-18ac-4193-b6af-290cbedb5b34"));

            migrationBuilder.DeleteData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: new Guid("a426bb10-1572-495b-b19a-cab14876e96e"));

            migrationBuilder.DeleteData(
                table: "TransactionTypes",
                keyColumn: "Id",
                keyValue: new Guid("1e9d12bb-3624-4873-b791-414062c57289"));

            migrationBuilder.DeleteData(
                table: "TransactionTypes",
                keyColumn: "Id",
                keyValue: new Guid("6f39da3b-5b8f-42ba-afbc-70b569b563b0"));

            migrationBuilder.DeleteData(
                table: "TransactionTypes",
                keyColumn: "Id",
                keyValue: new Guid("90f2c7c7-6879-484c-9fab-8c5826772f27"));

            migrationBuilder.DeleteData(
                table: "TransactionTypes",
                keyColumn: "Id",
                keyValue: new Guid("a566914c-7e8a-4456-baec-a32252d8d5e7"));

            migrationBuilder.AlterColumn<decimal>(
                name: "Amount",
                table: "Transactions",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "Balance",
                table: "AspNetUsers",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.InsertData(
                table: "Currencies",
                columns: new[] { "Id", "CreatedOn", "DeletedOn", "IsDeleted", "ModifiedOn", "Name" },
                values: new object[,]
                {
                    { new Guid("ff4d9d9a-1f00-47cc-b589-5c467a343b67"), null, null, false, null, "EUR" },
                    { new Guid("9310f845-318a-4f1b-b1fd-c845df9e78c9"), null, null, false, null, "USD" },
                    { new Guid("2bd37350-b648-4d2f-b511-188897edf963"), null, null, false, null, "BGN" },
                    { new Guid("3d449b15-e7a7-4344-97e1-354585461680"), null, null, false, null, "GBP" }
                });

            migrationBuilder.InsertData(
                table: "TransactionTypes",
                columns: new[] { "Id", "CreatedOn", "DeletedOn", "IsDeleted", "ModifiedOn", "Name" },
                values: new object[,]
                {
                    { new Guid("1d67ab84-1ac0-438a-9c0a-6136e6264ba2"), null, null, false, null, "Withdraw" },
                    { new Guid("047669c6-0d3c-4b53-8cbe-c4c67b3cfa50"), null, null, false, null, "Deposit" },
                    { new Guid("79d5466a-6dec-466e-94e4-68aa5b89bb5a"), null, null, false, null, "Win" },
                    { new Guid("fa1355ae-76be-40ec-9cd3-d252e5088f66"), null, null, false, null, "Stake" }
                });
        }
    }
}
