using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cars",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Make = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    Model = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    Color = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    ModelYear = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    MotorNumber = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    ChassisNumber = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cars", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DummyEntity",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    DummyPropertyOne = table.Column<string>(type: "text", nullable: true),
                    DummyPropertyTwo = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DummyEntity", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cars_ChassisNumber",
                table: "Cars",
                column: "ChassisNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Cars_MotorNumber",
                table: "Cars",
                column: "MotorNumber",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cars");

            migrationBuilder.DropTable(
                name: "DummyEntity");
        }
    }
}
