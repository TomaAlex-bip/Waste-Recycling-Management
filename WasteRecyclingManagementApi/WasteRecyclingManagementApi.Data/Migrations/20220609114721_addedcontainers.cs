using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WasteRecyclingManagementApi.Data.Migrations
{
    public partial class addedcontainers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Container_RecyclingPoints_RecyclingPointId",
                table: "Container");

            migrationBuilder.DropForeignKey(
                name: "FK_Operation_Container_ContainerId",
                table: "Operation");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Container",
                table: "Container");

            migrationBuilder.RenameTable(
                name: "Container",
                newName: "Containers");

            migrationBuilder.RenameIndex(
                name: "IX_Container_RecyclingPointId",
                table: "Containers",
                newName: "IX_Containers_RecyclingPointId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Containers",
                table: "Containers",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Containers_RecyclingPoints_RecyclingPointId",
                table: "Containers",
                column: "RecyclingPointId",
                principalTable: "RecyclingPoints",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Operation_Containers_ContainerId",
                table: "Operation",
                column: "ContainerId",
                principalTable: "Containers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Containers_RecyclingPoints_RecyclingPointId",
                table: "Containers");

            migrationBuilder.DropForeignKey(
                name: "FK_Operation_Containers_ContainerId",
                table: "Operation");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Containers",
                table: "Containers");

            migrationBuilder.RenameTable(
                name: "Containers",
                newName: "Container");

            migrationBuilder.RenameIndex(
                name: "IX_Containers_RecyclingPointId",
                table: "Container",
                newName: "IX_Container_RecyclingPointId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Container",
                table: "Container",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Container_RecyclingPoints_RecyclingPointId",
                table: "Container",
                column: "RecyclingPointId",
                principalTable: "RecyclingPoints",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Operation_Container_ContainerId",
                table: "Operation",
                column: "ContainerId",
                principalTable: "Container",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
