using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinanceTrackerWeb.Migrations
{
    /// <inheritdoc />
    public partial class AddMonthlyLimitToCategories2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "MonthlyLimit",
                table: "Categories",
                type: "numeric",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MonthlyLimit",
                table: "Categories");
        }
    }
}
