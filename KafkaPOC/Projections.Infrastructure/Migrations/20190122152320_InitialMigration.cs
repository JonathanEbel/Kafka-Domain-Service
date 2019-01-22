using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Projections.Infrastructure.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Usages",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    IdentityUserId = table.Column<Guid>(nullable: false),
                    Username = table.Column<string>(nullable: true),
                    LastLogin = table.Column<DateTime>(nullable: false),
                    LastBid = table.Column<DateTime>(nullable: false),
                    FullName = table.Column<string>(nullable: true),
                    OrgName = table.Column<string>(nullable: true),
                    UserId = table.Column<Guid>(nullable: false),
                    OrgId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usages", x => x.ID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Usages_FullName",
                table: "Usages",
                column: "FullName");

            migrationBuilder.CreateIndex(
                name: "IX_Usages_ID",
                table: "Usages",
                column: "ID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Usages_OrgId",
                table: "Usages",
                column: "OrgId");

            migrationBuilder.CreateIndex(
                name: "IX_Usages_OrgName",
                table: "Usages",
                column: "OrgName");

            migrationBuilder.CreateIndex(
                name: "IX_Usages_UserId",
                table: "Usages",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Usages");
        }
    }
}
