using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WasteRecyclingManagementApi.Data.Migrations
{
    public partial class ckoccupied : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddCheckConstraint(
                name: "CK_Container_Occupied",
                table: "Containers",
                sql: "[Occupied] <= [TotalCapacity]");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "CK_Container_Occupied",
                table: "Containers");
        }
    }
}
