using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HomeLoanAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddRemarksToLoanApplication : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Remarks",
                table: "LoanApplications",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Remarks",
                table: "LoanApplications");
        }
    }
}
