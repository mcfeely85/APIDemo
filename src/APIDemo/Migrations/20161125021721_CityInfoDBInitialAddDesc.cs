using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace APIDemo.Migrations
{
    public partial class CityInfoDBInitialAddDesc : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Description",
                table: "PointsOfInterest",
                newName: "Description1");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Description1",
                table: "PointsOfInterest",
                newName: "Description");
        }
    }
}
