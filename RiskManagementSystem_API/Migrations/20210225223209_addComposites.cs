using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RiskManagementSystem_API.Migrations
{
    public partial class addComposites : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_TeamMembers",
                table: "TeamMembers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RiskProperties",
                table: "RiskProperties");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RiskOwners",
                table: "RiskOwners");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MitigationRisks",
                table: "MitigationRisks");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "TeamMembers");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "RiskProperties");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "RiskOwners");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "MitigationRisks");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TeamMembers",
                table: "TeamMembers",
                columns: new[] { "ProjectId", "UserId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_RiskProperties",
                table: "RiskProperties",
                columns: new[] { "RiskId", "PropertyId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_RiskOwners",
                table: "RiskOwners",
                columns: new[] { "RiskId", "UserId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_MitigationRisks",
                table: "MitigationRisks",
                columns: new[] { "MitigationId", "RiskId" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_TeamMembers",
                table: "TeamMembers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RiskProperties",
                table: "RiskProperties");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RiskOwners",
                table: "RiskOwners");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MitigationRisks",
                table: "MitigationRisks");

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "TeamMembers",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "RiskProperties",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "RiskOwners",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "MitigationRisks",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_TeamMembers",
                table: "TeamMembers",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RiskProperties",
                table: "RiskProperties",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RiskOwners",
                table: "RiskOwners",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MitigationRisks",
                table: "MitigationRisks",
                column: "Id");
        }
    }
}
