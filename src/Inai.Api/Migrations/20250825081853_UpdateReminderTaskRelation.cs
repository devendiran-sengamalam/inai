using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Inai.Api.Migrations
{
    /// <inheritdoc />
    public partial class UpdateReminderTaskRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "TaskItemId1",
                table: "Reminders",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Reminders_TaskItemId1",
                table: "Reminders",
                column: "TaskItemId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Reminders_Tasks_TaskItemId1",
                table: "Reminders",
                column: "TaskItemId1",
                principalTable: "Tasks",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reminders_Tasks_TaskItemId1",
                table: "Reminders");

            migrationBuilder.DropIndex(
                name: "IX_Reminders_TaskItemId1",
                table: "Reminders");

            migrationBuilder.DropColumn(
                name: "TaskItemId1",
                table: "Reminders");
        }
    }
}
