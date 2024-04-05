using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Практика1.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Clients",
                columns: table => new
                {
                    ID_Client = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FirstName = table.Column<string>(type: "text", nullable: false),
                    SecondName = table.Column<string>(type: "text", nullable: false),
                    Patronymic = table.Column<string>(type: "text", nullable: true),
                    Address = table.Column<string>(type: "text", nullable: true),
                    Phone = table.Column<string>(type: "text", nullable: true),
                    Mail = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Clients_pkey", x => x.ID_Client);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    ID_Employee = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FirstName = table.Column<string>(type: "text", nullable: false),
                    SecondName = table.Column<string>(type: "text", nullable: false),
                    Patronymic = table.Column<string>(type: "text", nullable: true),
                    Post = table.Column<string>(type: "text", nullable: true),
                    Salary = table.Column<decimal>(type: "money", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Employees_pkey", x => x.ID_Employee);
                });

            migrationBuilder.CreateTable(
                name: "Providers",
                columns: table => new
                {
                    ID_Provider = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    ContactPerson = table.Column<string>(type: "text", nullable: false),
                    Phone = table.Column<string>(type: "text", nullable: true),
                    Address = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Providers_pkey", x => x.ID_Provider);
                });

            migrationBuilder.CreateTable(
                name: "Services",
                columns: table => new
                {
                    ID_Service = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Services_pkey", x => x.ID_Service);
                });

            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    ID_Employee = table.Column<long>(type: "bigint", nullable: false),
                    Login = table.Column<string>(type: "text", nullable: false),
                    Password = table.Column<string>(type: "text", nullable: false),
                    Role = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Accounts_pkey", x => x.ID_Employee);
                    table.ForeignKey(
                        name: "Accounts_ID_Employee_fkey",
                        column: x => x.ID_Employee,
                        principalTable: "Employees",
                        principalColumn: "ID_Employee");
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    ID_Order = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ID_Client = table.Column<long>(type: "bigint", nullable: false),
                    ID_Employee = table.Column<long>(type: "bigint", nullable: false),
                    Date = table.Column<DateOnly>(type: "date", nullable: true),
                    Status = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Orders_pkey", x => x.ID_Order);
                    table.ForeignKey(
                        name: "Orders_ID_Client_fkey",
                        column: x => x.ID_Client,
                        principalTable: "Clients",
                        principalColumn: "ID_Client");
                    table.ForeignKey(
                        name: "Orders_ID_Employee_fkey",
                        column: x => x.ID_Employee,
                        principalTable: "Employees",
                        principalColumn: "ID_Employee");
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    ID_Product = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ID_Provider = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Type = table.Column<string>(type: "text", nullable: true),
                    Price = table.Column<decimal>(type: "money", nullable: true),
                    Count = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Products_pkey", x => x.ID_Product);
                    table.ForeignKey(
                        name: "Products_ID_Provider_fkey",
                        column: x => x.ID_Provider,
                        principalTable: "Providers",
                        principalColumn: "ID_Provider");
                });

            migrationBuilder.CreateTable(
                name: "Services_Orders",
                columns: table => new
                {
                    ID_Order = table.Column<long>(type: "bigint", nullable: false),
                    ID_Service = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Services_Orders_pkey", x => new { x.ID_Order, x.ID_Service });
                    table.ForeignKey(
                        name: "Services_Orders_ID_Order_fkey",
                        column: x => x.ID_Order,
                        principalTable: "Orders",
                        principalColumn: "ID_Order");
                    table.ForeignKey(
                        name: "Services_Orders_ID_Service_fkey",
                        column: x => x.ID_Service,
                        principalTable: "Services",
                        principalColumn: "ID_Service");
                });

            migrationBuilder.CreateTable(
                name: "Products_Orders",
                columns: table => new
                {
                    ID_Order = table.Column<long>(type: "bigint", nullable: false),
                    ID_Product = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Products_Orders_pkey", x => new { x.ID_Order, x.ID_Product });
                    table.ForeignKey(
                        name: "Products_Orders_ID_Order_fkey",
                        column: x => x.ID_Order,
                        principalTable: "Orders",
                        principalColumn: "ID_Order");
                    table.ForeignKey(
                        name: "Products_Orders_ID_Product_fkey",
                        column: x => x.ID_Product,
                        principalTable: "Products",
                        principalColumn: "ID_Product");
                });

            migrationBuilder.CreateTable(
                name: "Reviews",
                columns: table => new
                {
                    ID_Review = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ID_Client = table.Column<long>(type: "bigint", nullable: false),
                    ID_Product = table.Column<long>(type: "bigint", nullable: false),
                    Rating = table.Column<long>(type: "bigint", nullable: false),
                    Comment = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Reviews_pkey", x => x.ID_Review);
                    table.ForeignKey(
                        name: "Reviews_ID_Client_fkey",
                        column: x => x.ID_Client,
                        principalTable: "Clients",
                        principalColumn: "ID_Client");
                    table.ForeignKey(
                        name: "Reviews_ID_Product_fkey",
                        column: x => x.ID_Product,
                        principalTable: "Products",
                        principalColumn: "ID_Product");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Orders_ID_Client",
                table: "Orders",
                column: "ID_Client");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_ID_Employee",
                table: "Orders",
                column: "ID_Employee");

            migrationBuilder.CreateIndex(
                name: "IX_Products_ID_Provider",
                table: "Products",
                column: "ID_Provider");

            migrationBuilder.CreateIndex(
                name: "IX_Products_Orders_ID_Product",
                table: "Products_Orders",
                column: "ID_Product");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_ID_Client",
                table: "Reviews",
                column: "ID_Client");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_ID_Product",
                table: "Reviews",
                column: "ID_Product");

            migrationBuilder.CreateIndex(
                name: "IX_Services_Orders_ID_Service",
                table: "Services_Orders",
                column: "ID_Service");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Accounts");

            migrationBuilder.DropTable(
                name: "Products_Orders");

            migrationBuilder.DropTable(
                name: "Reviews");

            migrationBuilder.DropTable(
                name: "Services_Orders");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Services");

            migrationBuilder.DropTable(
                name: "Providers");

            migrationBuilder.DropTable(
                name: "Clients");

            migrationBuilder.DropTable(
                name: "Employees");
        }
    }
}
