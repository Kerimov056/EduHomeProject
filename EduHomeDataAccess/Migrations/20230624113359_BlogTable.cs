﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EduHomeDataAccess.Migrations
{
    public partial class BlogTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Blogs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImagePath = table.Column<string>(type: "nvarchar(400)", maxLength: 400, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(90)", maxLength: 90, nullable: false),
                    PersonName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Data_Time = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MessageNum = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Blogs", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Blogs");
        }
    }
}
