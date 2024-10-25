using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinanceFlow.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddInstallmentAndRecurrenceInEntityAccount : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Status",
                table: "Accounts",
                newName: "Recurrence");

            migrationBuilder.AddColumn<int>(
                name: "Installment",
                table: "Accounts",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Installment",
                table: "Accounts");

            migrationBuilder.RenameColumn(
                name: "Recurrence",
                table: "Accounts",
                newName: "Status");
        }
    }
}
