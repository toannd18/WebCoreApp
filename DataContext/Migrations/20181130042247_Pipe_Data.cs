using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataContext.Migrations
{
    public partial class Pipe_Data : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Isometrics",
                columns: table => new
                {
                    DrawName = table.Column<string>(unicode: false, nullable: false),
                    Rev = table.Column<string>(maxLength: 4, nullable: true),
                    Unit = table.Column<string>(unicode: false, maxLength: 4, nullable: false),
                    PipeClass = table.Column<string>(unicode: false, maxLength: 4, nullable: false),
                    Line = table.Column<string>(unicode: false, maxLength: 4, nullable: false),
                    Size = table.Column<decimal>(type: "decimal(5,2)", nullable: false),
                    Type = table.Column<string>(maxLength: 4, nullable: false),
                    Material = table.Column<string>(maxLength: 20, nullable: false),
                    Project = table.Column<Guid>(nullable: false),
                    Remark = table.Column<string>(maxLength: 255, nullable: true),
                    UserCreated = table.Column<string>(nullable: true),
                    UserUpdated = table.Column<string>(nullable: true),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateUpdated = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Isometrics", x => x.DrawName);
                });

            migrationBuilder.CreateTable(
                name: "MaterialPipes",
                columns: table => new
                {
                    Name = table.Column<string>(unicode: false, maxLength: 20, nullable: false),
                    Description = table.Column<string>(maxLength: 255, nullable: true),
                    UserCreated = table.Column<string>(nullable: true),
                    UserUpdated = table.Column<string>(nullable: true),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateUpdated = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaterialPipes", x => x.Name);
                });

            migrationBuilder.CreateTable(
                name: "TypeJoints",
                columns: table => new
                {
                    Type = table.Column<string>(unicode: false, maxLength: 4, nullable: false),
                    Description = table.Column<string>(maxLength: 255, nullable: true),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateUpdated = table.Column<DateTime>(nullable: false),
                    UserCreated = table.Column<string>(nullable: true),
                    UserUpdated = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypeJoints", x => x.Type);
                });

            migrationBuilder.CreateTable(
                name: "Welders",
                columns: table => new
                {
                    Id = table.Column<string>(unicode: false, maxLength: 20, nullable: false),
                    Name = table.Column<string>(maxLength: 20, nullable: false),
                    BrithDay = table.Column<DateTime>(type: "Date", nullable: false),
                    Status = table.Column<bool>(nullable: false),
                    Remark = table.Column<string>(maxLength: 255, nullable: true),
                    UserCreated = table.Column<string>(nullable: true),
                    UserUpdated = table.Column<string>(nullable: true),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateUpdated = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Welders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "IsoJoints",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Joint = table.Column<string>(maxLength: 4, nullable: false),
                    Size = table.Column<decimal>(type: "decimal(5,2)", nullable: false),
                    TypeJoint = table.Column<string>(maxLength: 4, nullable: false),
                    DrawName = table.Column<string>(nullable: true),
                    Rev = table.Column<string>(maxLength: 4, nullable: true),
                    Heate1 = table.Column<string>(maxLength: 20, nullable: true),
                    Heate2 = table.Column<string>(maxLength: 20, nullable: true),
                    WeldingDate = table.Column<DateTime>(type: "Date", nullable: true),
                    Welder1 = table.Column<string>(nullable: true),
                    Welder2 = table.Column<string>(nullable: true),
                    Welder3 = table.Column<string>(nullable: true),
                    Welder4 = table.Column<string>(nullable: true),
                    SF = table.Column<byte>(type: "tinyint", nullable: false),
                    Status = table.Column<byte>(type: "tinyint", nullable: false),
                    UserCreated = table.Column<string>(nullable: true),
                    UserUpdated = table.Column<string>(nullable: true),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateUpdated = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IsoJoints", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IsoJoints_Isometrics_DrawName",
                        column: x => x.DrawName,
                        principalTable: "Isometrics",
                        principalColumn: "DrawName",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "WelderCertifications",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Certification = table.Column<string>(unicode: false, maxLength: 20, nullable: false),
                    Description = table.Column<string>(maxLength: 255, nullable: true),
                    IdWelder = table.Column<string>(nullable: true),
                    CerDate = table.Column<DateTime>(type: "Date", nullable: false),
                    UserCreated = table.Column<string>(nullable: true),
                    UserUpdated = table.Column<string>(nullable: true),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateUpdated = table.Column<DateTime>(nullable: false),
                    Remark = table.Column<string>(maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WelderCertifications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WelderCertifications_Welders_IdWelder",
                        column: x => x.IdWelder,
                        principalTable: "Welders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_IsoJoints_DrawName",
                table: "IsoJoints",
                column: "DrawName");

            migrationBuilder.CreateIndex(
                name: "IX_WelderCertifications_IdWelder",
                table: "WelderCertifications",
                column: "IdWelder");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IsoJoints");

            migrationBuilder.DropTable(
                name: "MaterialPipes");

            migrationBuilder.DropTable(
                name: "TypeJoints");

            migrationBuilder.DropTable(
                name: "WelderCertifications");

            migrationBuilder.DropTable(
                name: "Isometrics");

            migrationBuilder.DropTable(
                name: "Welders");
        }
    }
}
