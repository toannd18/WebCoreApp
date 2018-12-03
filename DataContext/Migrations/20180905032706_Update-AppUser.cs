using Microsoft.EntityFrameworkCore.Migrations;

namespace DataContext.Migrations
{
    public partial class UpdateAppUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AlterColumn<string>(
                name: "PasswordHash",
                table: "AppUser",
                unicode: false,
                nullable: true,
                oldClrType: typeof(string),
                oldUnicode: false,
                oldMaxLength: 50);

            migrationBuilder.AddForeignKey(
                name: "FK_AppUser_TblBp",
                table: "AppUser",
                column: "Ma_BP",
                principalTable: "tbl_BP",
                principalColumn: "Ma_BP",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AppUser_TblCv",
                table: "AppUser",
                column: "Ma_CV",
                principalTable: "tbl_CV",
                principalColumn: "Ma_CV",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AppUser_TblTo",
                table: "AppUser",
                column: "Ma_TO",
                principalTable: "tbl_TO",
                principalColumn: "Ma_TO",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppUser_TblBp",
                table: "AppUser");

            migrationBuilder.DropForeignKey(
                name: "FK_AppUser_TblCv",
                table: "AppUser");

            migrationBuilder.DropForeignKey(
                name: "FK_AppUser_TblTo",
                table: "AppUser");

            migrationBuilder.AlterColumn<string>(
                name: "PasswordHash",
                table: "AppUser",
                unicode: false,
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldUnicode: false,
                oldNullable: true);

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
    }
}
