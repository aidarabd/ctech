using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class constraintfixed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "CK_NegativeValue_Negative",
                schema: "main",
                table: "payment");

            migrationBuilder.AddCheckConstraint(
                name: "CK_NegativeValue_Negative",
                schema: "main",
                table: "users",
                sql: "\"balance\" > 0");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "CK_NegativeValue_Negative",
                schema: "main",
                table: "users");

            migrationBuilder.AddCheckConstraint(
                name: "CK_NegativeValue_Negative",
                schema: "main",
                table: "payment",
                sql: "\"amount\" < 0");
        }
    }
}
