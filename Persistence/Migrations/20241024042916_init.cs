using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "main");

            migrationBuilder.CreateTable(
                name: "candidates",
                schema: "main",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    first_name = table.Column<string>(type: "text", nullable: false),
                    last_name = table.Column<string>(type: "text", nullable: false),
                    email = table.Column<string>(type: "text", nullable: false),
                    comment = table.Column<string>(type: "text", nullable: false),
                    phone_number = table.Column<string>(type: "text", nullable: true),
                    available_from = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    available_to = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    linkedin_url = table.Column<string>(type: "text", nullable: true),
                    github_url = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_candidates", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "users",
                schema: "main",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    username = table.Column<string>(type: "text", nullable: false),
                    password_hash = table.Column<string>(type: "text", nullable: false),
                    balance = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "payment",
                schema: "main",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    amount = table.Column<decimal>(type: "numeric", nullable: false),
                    operation_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    operation_type_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_payment", x => x.id);
                    table.CheckConstraint("CK_NegativeValue_Negative", "\"amount\" < 0");
                    table.ForeignKey(
                        name: "FK_payment_users_UserId",
                        column: x => x.UserId,
                        principalSchema: "main",
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_candidates_email",
                schema: "main",
                table: "candidates",
                column: "email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_payment_UserId",
                schema: "main",
                table: "payment",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_users_username",
                schema: "main",
                table: "users",
                column: "username",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "candidates",
                schema: "main");

            migrationBuilder.DropTable(
                name: "payment",
                schema: "main");

            migrationBuilder.DropTable(
                name: "users",
                schema: "main");
        }
    }
}
