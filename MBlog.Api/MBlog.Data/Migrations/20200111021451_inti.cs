using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MBlog.Data.Migrations
{
    public partial class inti : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Topics",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsDelete = table.Column<bool>(nullable: false),
                    CreateDateTime = table.Column<DateTime>(nullable: false, defaultValueSql: "GETUTCDATE()"),
                    UpdateDateTime = table.Column<DateTime>(nullable: true, defaultValueSql: "GETUTCDATE()"),
                    TopicName = table.Column<string>(maxLength: 50, nullable: true),
                    TopicDetail = table.Column<string>(maxLength: 300, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Topics", x => x.Id);
                });

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
                    FullName = table.Column<string>(maxLength: 100, nullable: true),
                    About = table.Column<string>(maxLength: 100, nullable: true),
                    ActiveStatus = table.Column<int>(nullable: false),
                    Role = table.Column<int>(nullable: false),
                    ImageProfile = table.Column<byte[]>(nullable: true),
                    ImageProfilePath = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.UniqueConstraint("AK_Users_Email", x => x.Email);
                });

            migrationBuilder.CreateTable(
                name: "Blogs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsDelete = table.Column<bool>(nullable: false),
                    CreateDateTime = table.Column<DateTime>(nullable: false, defaultValueSql: "GETUTCDATE()"),
                    UpdateDateTime = table.Column<DateTime>(nullable: true, defaultValueSql: "GETUTCDATE()"),
                    OwnerId = table.Column<int>(nullable: false),
                    Title = table.Column<string>(nullable: true),
                    Detail = table.Column<string>(nullable: true),
                    ImageHead = table.Column<byte[]>(nullable: true),
                    ImagePath = table.Column<string>(nullable: true),
                    TopicId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Blogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Blogs_Users_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Blogs_Topics_TopicId",
                        column: x => x.TopicId,
                        principalTable: "Topics",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Followings",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsDelete = table.Column<bool>(nullable: false),
                    CreateDateTime = table.Column<DateTime>(nullable: false, defaultValueSql: "GETUTCDATE()"),
                    UpdateDateTime = table.Column<DateTime>(nullable: true, defaultValueSql: "GETUTCDATE()"),
                    FollowingId = table.Column<int>(nullable: false),
                    FollowingUserId = table.Column<int>(nullable: true),
                    FollowerId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Followings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Followings_Users_FollowerId",
                        column: x => x.FollowerId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Followings_Users_FollowingUserId",
                        column: x => x.FollowingUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Favorites",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsDelete = table.Column<bool>(nullable: false),
                    CreateDateTime = table.Column<DateTime>(nullable: false, defaultValueSql: "GETUTCDATE()"),
                    UpdateDateTime = table.Column<DateTime>(nullable: true, defaultValueSql: "GETUTCDATE()"),
                    BlogId = table.Column<int>(nullable: false),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Favorites", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Favorites_Blogs_BlogId",
                        column: x => x.BlogId,
                        principalTable: "Blogs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Favorites_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Blogs_OwnerId",
                table: "Blogs",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Blogs_TopicId",
                table: "Blogs",
                column: "TopicId");

            migrationBuilder.CreateIndex(
                name: "IX_Favorites_BlogId",
                table: "Favorites",
                column: "BlogId");

            migrationBuilder.CreateIndex(
                name: "IX_Favorites_UserId",
                table: "Favorites",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Followings_FollowerId",
                table: "Followings",
                column: "FollowerId");

            migrationBuilder.CreateIndex(
                name: "IX_Followings_FollowingUserId",
                table: "Followings",
                column: "FollowingUserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Favorites");

            migrationBuilder.DropTable(
                name: "Followings");

            migrationBuilder.DropTable(
                name: "Blogs");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Topics");
        }
    }
}
