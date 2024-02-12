using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StoreWebApi.Migrations
{
    public partial class assets : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: new Guid("64c00f60-709e-439c-b665-2fdba13b7eb3"));

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: new Guid("736cf5ed-f97e-41e6-bc38-8612cd9bbf7a"));

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: new Guid("b14da30a-9de6-4702-92e3-7d891ea8c409"));

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: new Guid("b7d663be-1dcb-4c52-b850-48afab543a40"));

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: new Guid("cc211be6-2fbc-4742-99ae-49a0e68ecf8d"));

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: new Guid("e398c16f-4fe3-4989-b2a7-39d2119e989c"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("17ee9bc2-3c92-46b7-98ac-f723f634cd49"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("6cdcd2c2-7ba9-4a17-a099-9e048e5cfe3c"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("6fc24cd9-cf55-4073-8eed-de465199aa10"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("754866e3-e3df-4401-9aac-0644e77416aa"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("923b05d5-c9e1-4d60-89b1-aa8352cea9ee"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("d290f631-faf9-43b1-8420-8309569dc07f"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("ec9996d6-c928-4ede-b056-fbc1141c8e1d"));

            migrationBuilder.DeleteData(
                table: "Statuses",
                keyColumn: "Id",
                keyValue: new Guid("15dfb474-b0a6-4445-b768-f8af3fd5825a"));

            migrationBuilder.DeleteData(
                table: "Statuses",
                keyColumn: "Id",
                keyValue: new Guid("c90759ff-a237-4eca-a762-2ca05abb65e7"));

            migrationBuilder.DeleteData(
                table: "Statuses",
                keyColumn: "Id",
                keyValue: new Guid("c946afe4-2e8f-4c97-8296-89ee107486ac"));

            migrationBuilder.InsertData(
                table: "Brands",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("15f6bf84-57ba-4ff6-9672-645cf37edb9c"), "ESP" },
                    { new Guid("3a88ec60-503b-459d-a595-28687cc2ff5a"), "Fender" },
                    { new Guid("48090a23-0aba-47f0-86bb-8876ae59a2cc"), "Ibanez" },
                    { new Guid("92f0af86-9226-4276-b7a9-8b8a57ee4779"), "Marshal" },
                    { new Guid("bd019164-80fa-4400-8531-11d0d8ad0504"), "Line6" },
                    { new Guid("c6ae762a-d7a2-47ba-a8b6-b9f0489f3898"), "Yamaha" }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("253ceb3e-8ed2-43be-8cf4-5518cd968938"), "Electric guitar" },
                    { new Guid("28fed80a-2d73-4567-a02c-7c22bb2b262e"), "Accesories" },
                    { new Guid("351a53b7-ee04-43f5-9dd5-d33c8b0d63ea"), "Proccesors" },
                    { new Guid("43cd6662-387c-41d9-bf42-f94349945e18"), "Amplifiers" },
                    { new Guid("62c258b2-7f9a-4d81-98d9-aba86458dc40"), "Cabinets" },
                    { new Guid("7a32555e-b5a6-4a91-a7b0-8f17c8b6ddac"), "Studio equipment" },
                    { new Guid("f81305e4-466b-49a7-9ade-65532a174a90"), "Classical guitar" }
                });

            migrationBuilder.InsertData(
                table: "Statuses",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("7f9a973f-1caf-4913-8464-528a6a24f3c8"), "Sended" },
                    { new Guid("bd0d73fa-a0b2-4889-8bee-46f4715b5ac7"), "Canceled" },
                    { new Guid("ef81a669-32a3-447a-97f7-0bce9ba9ec27"), "Pending" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: new Guid("15f6bf84-57ba-4ff6-9672-645cf37edb9c"));

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: new Guid("3a88ec60-503b-459d-a595-28687cc2ff5a"));

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: new Guid("48090a23-0aba-47f0-86bb-8876ae59a2cc"));

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: new Guid("92f0af86-9226-4276-b7a9-8b8a57ee4779"));

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: new Guid("bd019164-80fa-4400-8531-11d0d8ad0504"));

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: new Guid("c6ae762a-d7a2-47ba-a8b6-b9f0489f3898"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("253ceb3e-8ed2-43be-8cf4-5518cd968938"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("28fed80a-2d73-4567-a02c-7c22bb2b262e"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("351a53b7-ee04-43f5-9dd5-d33c8b0d63ea"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("43cd6662-387c-41d9-bf42-f94349945e18"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("62c258b2-7f9a-4d81-98d9-aba86458dc40"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("7a32555e-b5a6-4a91-a7b0-8f17c8b6ddac"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("f81305e4-466b-49a7-9ade-65532a174a90"));

            migrationBuilder.DeleteData(
                table: "Statuses",
                keyColumn: "Id",
                keyValue: new Guid("7f9a973f-1caf-4913-8464-528a6a24f3c8"));

            migrationBuilder.DeleteData(
                table: "Statuses",
                keyColumn: "Id",
                keyValue: new Guid("bd0d73fa-a0b2-4889-8bee-46f4715b5ac7"));

            migrationBuilder.DeleteData(
                table: "Statuses",
                keyColumn: "Id",
                keyValue: new Guid("ef81a669-32a3-447a-97f7-0bce9ba9ec27"));

            migrationBuilder.InsertData(
                table: "Brands",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("64c00f60-709e-439c-b665-2fdba13b7eb3"), "Marshal" },
                    { new Guid("736cf5ed-f97e-41e6-bc38-8612cd9bbf7a"), "Fender" },
                    { new Guid("b14da30a-9de6-4702-92e3-7d891ea8c409"), "Ibanez" },
                    { new Guid("b7d663be-1dcb-4c52-b850-48afab543a40"), "Yamaha" },
                    { new Guid("cc211be6-2fbc-4742-99ae-49a0e68ecf8d"), "Line6" },
                    { new Guid("e398c16f-4fe3-4989-b2a7-39d2119e989c"), "ESP" }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("17ee9bc2-3c92-46b7-98ac-f723f634cd49"), "Proccesors" },
                    { new Guid("6cdcd2c2-7ba9-4a17-a099-9e048e5cfe3c"), "Electric guitar" },
                    { new Guid("6fc24cd9-cf55-4073-8eed-de465199aa10"), "Amplifiers" },
                    { new Guid("754866e3-e3df-4401-9aac-0644e77416aa"), "Studio equipment" },
                    { new Guid("923b05d5-c9e1-4d60-89b1-aa8352cea9ee"), "Accesories" },
                    { new Guid("d290f631-faf9-43b1-8420-8309569dc07f"), "Cabinets" },
                    { new Guid("ec9996d6-c928-4ede-b056-fbc1141c8e1d"), "Classical guitar" }
                });

            migrationBuilder.InsertData(
                table: "Statuses",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("15dfb474-b0a6-4445-b768-f8af3fd5825a"), "Sended" },
                    { new Guid("c90759ff-a237-4eca-a762-2ca05abb65e7"), "Canceled" },
                    { new Guid("c946afe4-2e8f-4c97-8296-89ee107486ac"), "Pending" }
                });
        }
    }
}
