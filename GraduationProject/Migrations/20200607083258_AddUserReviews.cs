using Microsoft.EntityFrameworkCore.Migrations;

namespace GraduationProject.Migrations
{
    public partial class AddUserReviews : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserReviewID",
                table: "Users_Reviews",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserReviewName",
                table: "Users_Reviews",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserReviewID",
                table: "Users_Reviews");

            migrationBuilder.DropColumn(
                name: "UserReviewName",
                table: "Users_Reviews");
        }
    }
}
