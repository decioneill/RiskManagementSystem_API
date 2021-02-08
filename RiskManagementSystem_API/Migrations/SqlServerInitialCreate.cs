/*using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RiskManagementSystem_API.Migrations.SqlServerMigrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            BuildUser(migrationBuilder);
            BuildProject(migrationBuilder);
            BuildTeamMember(migrationBuilder);
            BuildRisk(migrationBuilder);
            BuildRiskOwner(migrationBuilder);
            BuildRiskProperty(migrationBuilder);
            BuildRiskStatusHistory(migrationBuilder);
            BuildMitigation(migrationBuilder);
            BuildMitigationRisk(migrationBuilder);
        }

        private static void BuildUser(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(nullable: false),
                    UserName = table.Column<string>(nullable: false),
                    RiskManager = table.Column<bool>(nullable: false),
                    Admin = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<byte[]>(nullable: false),
                    PasswordSalt = table.Column<byte[]>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });
        }

        private static void BuildTeamMember(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TeamMember",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProjectName = table.Column<string>(nullable: false),
                    UserId = table.Column<int>(nullable: false),
                    TeamLeader = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeamMember", x => x.Id);
                });
        }

        private static void BuildProject(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Project",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Project", x => x.Id);
                });
        }

        private static void BuildRisk(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Risk",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(nullable: true),
                    ShortDescription = table.Column<string>(nullable: true),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Risk", x => x.Id);
                });
        }

        private static void BuildRiskOwner(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RiskOwner",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RiskId = table.Column<int>(nullable: true),
                    UserId = table.Column<int>(nullable: true),
                    Priority = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RiskOwner", x => x.Id);
                });
        }

        private static void BuildRiskStatusHistory(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RiskStatusHistory",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RiskId = table.Column<int>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    StatusChangedBy = table.Column<int>(nullable: false), // userId
                    StatusChangeDate = table.Column<DateTime>(nullable: false),
                    ReviewDate = table.Column<DateTime>(nullable: false), // next date for review
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RiskStatusHistory", x => x.Id);
                });
        }

        private static void BuildRiskProperty(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RiskProperty",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RiskId = table.Column<int>(nullable: false),
                    PropertyId = table.Column<int>(nullable: false),
                    PropertyValue = table.Column<string>(nullable: true),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RiskProperty", x => x.Id);
                });
        }

        private static void BuildMitigation(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Mitigation",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    CurrentStatus = table.Column<int>(nullable: false),
                    ReviewDate = table.Column<DateTime>(nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mitigation", x => x.Id);
                });
        }

        private static void BuildMitigationRisk(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MitigationRisk",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MitigationId = table.Column<int>(nullable: false),
                    RiskId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MitigationRisk", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "User");
            migrationBuilder.DropTable(
                name: "Project");
            migrationBuilder.DropTable(
                name: "TeamMember");
            migrationBuilder.DropTable(
                name: "Risk");
            migrationBuilder.DropTable(
                name: "RiskOwner");
            migrationBuilder.DropTable(
                name: "RiskProperty");
            migrationBuilder.DropTable(
                name: "RiskStatusHistory");
            migrationBuilder.DropTable(
                name: "Mitigation");
            migrationBuilder.DropTable(
                name: "MitigationRisk");
        }
    }
}*/
