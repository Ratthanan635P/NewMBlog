using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MBlog.Data.Migrations
{
    public partial class inti : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsDelete = table.Column<bool>(nullable: false),
                    CreateDateTime = table.Column<DateTime>(nullable: false, defaultValueSql: "GETUTCDATE()"),
                    UpdateDateTime = table.Column<DateTime>(nullable: true, defaultValueSql: "GETUTCDATE()"),
                    Email = table.Column<string>(maxLength: 100, nullable: false),
                    Password = table.Column<string>(nullable: true),
                    Salt = table.Column<string>(nullable: true),
                    RefeshToken = table.Column<string>(nullable: true),
                    AccessToken = table.Column<string>(nullable: true),
                    FullName = table.Column<string>(maxLength: 100, nullable: false),
                    About = table.Column<string>(maxLength: 100, nullable: false),
                    ActiveStatus = table.Column<int>(nullable: false),
                    Role = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.UniqueConstraint("AK_Users_Email", x => x.Email);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
