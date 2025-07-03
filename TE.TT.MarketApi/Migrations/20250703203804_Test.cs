using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TE.TT.MarketApi.Migrations
{
    /// <inheritdoc />
    public partial class Test : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MappingBase_Assets_AssetId",
                table: "MappingBase");

            migrationBuilder.DropForeignKey(
                name: "FK_MappingBase_TradingHours_TradingHoursId",
                table: "MappingBase");

            migrationBuilder.DropForeignKey(
                name: "FK_TradingHours_MappingBase_MappingId",
                table: "TradingHours");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MappingBase",
                table: "MappingBase");

            migrationBuilder.DropIndex(
                name: "IX_MappingBase_TradingHoursId",
                table: "MappingBase");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "MappingBase");

            migrationBuilder.DropColumn(
                name: "TradingHoursId",
                table: "MappingBase");

            migrationBuilder.RenameTable(
                name: "MappingBase",
                newName: "MappingSimulation");

            migrationBuilder.RenameColumn(
                name: "MappingId",
                table: "TradingHours",
                newName: "SimulationMappingId");

            migrationBuilder.RenameIndex(
                name: "IX_TradingHours_MappingId",
                table: "TradingHours",
                newName: "IX_TradingHours_SimulationMappingId");

            migrationBuilder.RenameIndex(
                name: "IX_MappingBase_AssetId",
                table: "MappingSimulation",
                newName: "IX_MappingSimulation_AssetId");

            migrationBuilder.AddColumn<Guid>(
                name: "AlpacaMappingId",
                table: "TradingHours",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<Guid>(
                name: "DxfeedMappingId",
                table: "TradingHours",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<Guid>(
                name: "OandaMappingId",
                table: "TradingHours",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MappingSimulation",
                table: "MappingSimulation",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "MappingAlpaca",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Symbol = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Exchange = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DefualtOrderSize = table.Column<int>(type: "int", nullable: true),
                    MaxOrderSize = table.Column<int>(type: "int", nullable: true),
                    AssetId = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    UpdateData = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MappingAlpaca", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MappingAlpaca_Assets_AssetId",
                        column: x => x.AssetId,
                        principalTable: "Assets",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "MappingDxfeed",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Symbol = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Exchange = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DefualtOrderSize = table.Column<int>(type: "int", nullable: true),
                    MaxOrderSize = table.Column<int>(type: "int", nullable: true),
                    AssetId = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    UpdateData = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MappingDxfeed", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MappingDxfeed_Assets_AssetId",
                        column: x => x.AssetId,
                        principalTable: "Assets",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "MappingOanda",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Symbol = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Exchange = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DefualtOrderSize = table.Column<int>(type: "int", nullable: true),
                    MaxOrderSize = table.Column<int>(type: "int", nullable: true),
                    AssetId = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    UpdateData = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MappingOanda", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MappingOanda_Assets_AssetId",
                        column: x => x.AssetId,
                        principalTable: "Assets",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_TradingHours_AlpacaMappingId",
                table: "TradingHours",
                column: "AlpacaMappingId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TradingHours_DxfeedMappingId",
                table: "TradingHours",
                column: "DxfeedMappingId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TradingHours_OandaMappingId",
                table: "TradingHours",
                column: "OandaMappingId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MappingAlpaca_AssetId",
                table: "MappingAlpaca",
                column: "AssetId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MappingDxfeed_AssetId",
                table: "MappingDxfeed",
                column: "AssetId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MappingOanda_AssetId",
                table: "MappingOanda",
                column: "AssetId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_MappingSimulation_Assets_AssetId",
                table: "MappingSimulation",
                column: "AssetId",
                principalTable: "Assets",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TradingHours_MappingAlpaca_AlpacaMappingId",
                table: "TradingHours",
                column: "AlpacaMappingId",
                principalTable: "MappingAlpaca",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TradingHours_MappingDxfeed_DxfeedMappingId",
                table: "TradingHours",
                column: "DxfeedMappingId",
                principalTable: "MappingDxfeed",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TradingHours_MappingOanda_OandaMappingId",
                table: "TradingHours",
                column: "OandaMappingId",
                principalTable: "MappingOanda",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TradingHours_MappingSimulation_SimulationMappingId",
                table: "TradingHours",
                column: "SimulationMappingId",
                principalTable: "MappingSimulation",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MappingSimulation_Assets_AssetId",
                table: "MappingSimulation");

            migrationBuilder.DropForeignKey(
                name: "FK_TradingHours_MappingAlpaca_AlpacaMappingId",
                table: "TradingHours");

            migrationBuilder.DropForeignKey(
                name: "FK_TradingHours_MappingDxfeed_DxfeedMappingId",
                table: "TradingHours");

            migrationBuilder.DropForeignKey(
                name: "FK_TradingHours_MappingOanda_OandaMappingId",
                table: "TradingHours");

            migrationBuilder.DropForeignKey(
                name: "FK_TradingHours_MappingSimulation_SimulationMappingId",
                table: "TradingHours");

            migrationBuilder.DropTable(
                name: "MappingAlpaca");

            migrationBuilder.DropTable(
                name: "MappingDxfeed");

            migrationBuilder.DropTable(
                name: "MappingOanda");

            migrationBuilder.DropIndex(
                name: "IX_TradingHours_AlpacaMappingId",
                table: "TradingHours");

            migrationBuilder.DropIndex(
                name: "IX_TradingHours_DxfeedMappingId",
                table: "TradingHours");

            migrationBuilder.DropIndex(
                name: "IX_TradingHours_OandaMappingId",
                table: "TradingHours");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MappingSimulation",
                table: "MappingSimulation");

            migrationBuilder.DropColumn(
                name: "AlpacaMappingId",
                table: "TradingHours");

            migrationBuilder.DropColumn(
                name: "DxfeedMappingId",
                table: "TradingHours");

            migrationBuilder.DropColumn(
                name: "OandaMappingId",
                table: "TradingHours");

            migrationBuilder.RenameTable(
                name: "MappingSimulation",
                newName: "MappingBase");

            migrationBuilder.RenameColumn(
                name: "SimulationMappingId",
                table: "TradingHours",
                newName: "MappingId");

            migrationBuilder.RenameIndex(
                name: "IX_TradingHours_SimulationMappingId",
                table: "TradingHours",
                newName: "IX_TradingHours_MappingId");

            migrationBuilder.RenameIndex(
                name: "IX_MappingSimulation_AssetId",
                table: "MappingBase",
                newName: "IX_MappingBase_AssetId");

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "MappingBase",
                type: "varchar(21)",
                maxLength: 21,
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<Guid>(
                name: "TradingHoursId",
                table: "MappingBase",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MappingBase",
                table: "MappingBase",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_MappingBase_TradingHoursId",
                table: "MappingBase",
                column: "TradingHoursId");

            migrationBuilder.AddForeignKey(
                name: "FK_MappingBase_Assets_AssetId",
                table: "MappingBase",
                column: "AssetId",
                principalTable: "Assets",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MappingBase_TradingHours_TradingHoursId",
                table: "MappingBase",
                column: "TradingHoursId",
                principalTable: "TradingHours",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TradingHours_MappingBase_MappingId",
                table: "TradingHours",
                column: "MappingId",
                principalTable: "MappingBase",
                principalColumn: "Id");
        }
    }
}
