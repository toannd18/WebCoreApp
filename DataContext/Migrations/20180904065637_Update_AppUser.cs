using Microsoft.EntityFrameworkCore.Migrations;

namespace DataContext.Migrations
{
    public partial class Update_AppUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppUser_tbl_BP",
                table: "AppUser");

            migrationBuilder.DropForeignKey(
                name: "FK_AppUser_tbl_CV",
                table: "AppUser");

            migrationBuilder.DropForeignKey(
                name: "FK_AppUser_tbl_TO",
                table: "AppUser");

            migrationBuilder.AddForeignKey(
                name: "FK_AppUser_tbl_BP_Ma_BP",
                table: "AppUser",
                column: "Ma_BP",
                principalTable: "tbl_BP",
                principalColumn: "Ma_BP",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AppUser_tbl_CV_Ma_CV",
                table: "AppUser",
                column: "Ma_CV",
                principalTable: "tbl_CV",
                principalColumn: "Ma_CV",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AppUser_tbl_TO_Ma_TO",
                table: "AppUser",
                column: "Ma_TO",
                principalTable: "tbl_TO",
                principalColumn: "Ma_TO",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppUser_tbl_BP_Ma_BP",
                table: "AppUser");

            migrationBuilder.DropForeignKey(
                name: "FK_AppUser_tbl_CV_Ma_CV",
                table: "AppUser");

            migrationBuilder.DropForeignKey(
                name: "FK_AppUser_tbl_TO_Ma_TO",
                table: "AppUser");

            migrationBuilder.AddForeignKey(
                name: "FK_AppUser_tbl_BP",
                table: "AppUser",
                column: "Ma_BP",
                principalTable: "tbl_BP",
                principalColumn: "Ma_BP",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AppUser_tbl_CV",
                table: "AppUser",
                column: "Ma_CV",
                principalTable: "tbl_CV",
                principalColumn: "Ma_CV",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AppUser_tbl_TO",
                table: "AppUser",
                column: "Ma_TO",
                principalTable: "tbl_TO",
                principalColumn: "Ma_TO",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
