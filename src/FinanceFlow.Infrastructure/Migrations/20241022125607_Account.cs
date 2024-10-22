using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinanceFlow.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Account : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tags_Accounts_AccountID",
                table: "Tags");

            migrationBuilder.DropForeignKey(
                name: "FK_Tags_Expenses_ExpenseId",
                table: "Tags");

            migrationBuilder.DropTable(
                name: "Expenses");

            migrationBuilder.DropIndex(
                name: "IX_Tags_ExpenseId",
                table: "Tags");

            migrationBuilder.DropColumn(
                name: "ExpenseId",
                table: "Tags");

            migrationBuilder.DropColumn(
                name: "Last_Payment_Date",
                table: "Recurrences");

            migrationBuilder.RenameColumn(
                name: "AccountID",
                table: "Tags",
                newName: "AccountId");

            migrationBuilder.RenameIndex(
                name: "IX_Tags_AccountID",
                table: "Tags",
                newName: "IX_Tags_AccountId");

            migrationBuilder.AlterColumn<long>(
                name: "AccountId",
                table: "Tags",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Tags_Accounts_AccountId",
                table: "Tags",
                column: "AccountId",
                principalTable: "Accounts",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tags_Accounts_AccountId",
                table: "Tags");

            migrationBuilder.RenameColumn(
                name: "AccountId",
                table: "Tags",
                newName: "AccountID");

            migrationBuilder.RenameIndex(
                name: "IX_Tags_AccountId",
                table: "Tags",
                newName: "IX_Tags_AccountID");

            migrationBuilder.AlterColumn<long>(
                name: "AccountID",
                table: "Tags",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddColumn<long>(
                name: "ExpenseId",
                table: "Tags",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<DateTime>(
                name: "Last_Payment_Date",
                table: "Recurrences",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Expenses",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    Create_at = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Description = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PaymentType = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Expenses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Expenses_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Tags_ExpenseId",
                table: "Tags",
                column: "ExpenseId");

            migrationBuilder.CreateIndex(
                name: "IX_Expenses_UserId",
                table: "Expenses",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tags_Accounts_AccountID",
                table: "Tags",
                column: "AccountID",
                principalTable: "Accounts",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Tags_Expenses_ExpenseId",
                table: "Tags",
                column: "ExpenseId",
                principalTable: "Expenses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
