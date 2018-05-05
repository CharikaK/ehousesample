using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Ehouse.Data.Migrations
{
    public partial class updateEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Photographers_Bookings_BookingId",
                table: "Photographers");

            migrationBuilder.DropIndex(
                name: "IX_Photographers_BookingId",
                table: "Photographers");

            migrationBuilder.DropIndex(
                name: "IX_Bookings_UserId",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "BookingId",
                table: "Photographers");

            migrationBuilder.RenameColumn(
                name: "Category",
                table: "Bookings",
                newName: "Postcode");

            migrationBuilder.AddColumn<int>(
                name: "PContactNo",
                table: "Photographers",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "PEmail",
                table: "Photographers",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "photographerId",
                table: "Bookings",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BookingId = table.Column<int>(nullable: true),
                    Desc = table.Column<string>(nullable: true),
                    PhotographerId = table.Column<int>(nullable: true),
                    Price = table.Column<float>(nullable: false),
                    Type = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Category_Bookings_BookingId",
                        column: x => x.BookingId,
                        principalTable: "Bookings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Category_Photographers_PhotographerId",
                        column: x => x.PhotographerId,
                        principalTable: "Photographers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_UserId",
                table: "Bookings",
                column: "UserId",
                unique: true,
                filter: "[UserId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_photographerId",
                table: "Bookings",
                column: "photographerId");

            migrationBuilder.CreateIndex(
                name: "IX_Category_BookingId",
                table: "Category",
                column: "BookingId");

            migrationBuilder.CreateIndex(
                name: "IX_Category_PhotographerId",
                table: "Category",
                column: "PhotographerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_Photographers_photographerId",
                table: "Bookings",
                column: "photographerId",
                principalTable: "Photographers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_Photographers_photographerId",
                table: "Bookings");

            migrationBuilder.DropTable(
                name: "Category");

            migrationBuilder.DropIndex(
                name: "IX_Bookings_UserId",
                table: "Bookings");

            migrationBuilder.DropIndex(
                name: "IX_Bookings_photographerId",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "PContactNo",
                table: "Photographers");

            migrationBuilder.DropColumn(
                name: "PEmail",
                table: "Photographers");

            migrationBuilder.DropColumn(
                name: "photographerId",
                table: "Bookings");

            migrationBuilder.RenameColumn(
                name: "Postcode",
                table: "Bookings",
                newName: "Category");

            migrationBuilder.AddColumn<int>(
                name: "BookingId",
                table: "Photographers",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Photographers_BookingId",
                table: "Photographers",
                column: "BookingId");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_UserId",
                table: "Bookings",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Photographers_Bookings_BookingId",
                table: "Photographers",
                column: "BookingId",
                principalTable: "Bookings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
