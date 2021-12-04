using Microsoft.EntityFrameworkCore.Migrations;

namespace DAO.Migrations
{
    public partial class init2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Incidents_Cameras_CameraId",
                table: "Incidents");

            migrationBuilder.DropIndex(
                name: "IX_Incidents_CameraId",
                table: "Incidents");

            migrationBuilder.AlterColumn<int>(
                name: "CameraId",
                table: "Incidents",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "CameraId",
                table: "Incidents",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Incidents_CameraId",
                table: "Incidents",
                column: "CameraId");

            migrationBuilder.AddForeignKey(
                name: "FK_Incidents_Cameras_CameraId",
                table: "Incidents",
                column: "CameraId",
                principalTable: "Cameras",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
