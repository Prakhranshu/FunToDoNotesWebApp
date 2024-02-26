using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RepositoryLayer.Migrations
{
    public partial class UserModified : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "fname",
                table: "UserTable");

            migrationBuilder.DropColumn(
                name: "lname",
                table: "UserTable");

            migrationBuilder.RenameColumn(
                name: "UserPassword",
                table: "UserTable",
                newName: "Userpassword");

            migrationBuilder.RenameColumn(
                name: "UserEmail",
                table: "UserTable",
                newName: "Useremail");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateTime",
                table: "UserTable",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Firstname",
                table: "UserTable",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "LastLoginTime",
                table: "UserTable",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Lastname",
                table: "UserTable",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreateTime",
                table: "UserTable");

            migrationBuilder.DropColumn(
                name: "Firstname",
                table: "UserTable");

            migrationBuilder.DropColumn(
                name: "LastLoginTime",
                table: "UserTable");

            migrationBuilder.DropColumn(
                name: "Lastname",
                table: "UserTable");

            migrationBuilder.RenameColumn(
                name: "Userpassword",
                table: "UserTable",
                newName: "UserPassword");

            migrationBuilder.RenameColumn(
                name: "Useremail",
                table: "UserTable",
                newName: "UserEmail");

            migrationBuilder.AddColumn<string>(
                name: "fname",
                table: "UserTable",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "lname",
                table: "UserTable",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
