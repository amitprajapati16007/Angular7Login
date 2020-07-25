using Microsoft.EntityFrameworkCore.Migrations;

namespace AspCoreBl.Migrations
{
    public partial class socialligin : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "isSocialLogin",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isSocialLogin",
                table: "AspNetUsers");
        }
    }
}
