using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskManager.Migrations
{
    /// <inheritdoc />
    public partial class addedAuthorTask : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "FinishedDate",
                table: "TaskModel",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "TaskModel",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AuthorId",
                table: "TaskModel",
                type: "text",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TaskModel_AuthorId",
                table: "TaskModel",
                column: "AuthorId");

            migrationBuilder.AddForeignKey(
                name: "FK_TaskModel_AspNetUsers_AuthorId",
                table: "TaskModel",
                column: "AuthorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TaskModel_AspNetUsers_AuthorId",
                table: "TaskModel");

            migrationBuilder.DropIndex(
                name: "IX_TaskModel_AuthorId",
                table: "TaskModel");

            migrationBuilder.DropColumn(
                name: "AuthorId",
                table: "TaskModel");

            migrationBuilder.AlterColumn<DateTime>(
                name: "FinishedDate",
                table: "TaskModel",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "TaskModel",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");
        }
    }
}
