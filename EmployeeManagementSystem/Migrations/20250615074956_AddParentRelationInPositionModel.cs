using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EmployeeManagementSystem.Migrations
{
    /// <inheritdoc />
    public partial class AddParentRelationInPositionModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Positions_ParentId",
                table: "Positions",
                column: "ParentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Positions_Positions_ParentId",
                table: "Positions",
                column: "ParentId",
                principalTable: "Positions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Positions_Positions_ParentId",
                table: "Positions");

            migrationBuilder.DropIndex(
                name: "IX_Positions_ParentId",
                table: "Positions");
        }
    }
}
