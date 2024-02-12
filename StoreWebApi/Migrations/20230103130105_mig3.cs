using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StoreWebApi.Migrations
{
    public partial class mig3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "Reviews",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.InsertData(
                table: "Brands",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("263e34fb-04fd-4e6d-abac-8ccdde1679f5"), "Fender" },
                    { new Guid("37ba777e-8d56-4974-824e-56cc7562f1bc"), "Yamaha" },
                    { new Guid("5b2bc7a3-d0bc-4eda-9067-5df9ef1ba20c"), "Marshal" },
                    { new Guid("b5bc612c-f024-4244-8a4d-af01677481e8"), "Line6" },
                    { new Guid("d1f25d04-7c71-4337-ad55-de307ab602ae"), "Ibanez" },
                    { new Guid("e38eea98-420d-4188-9e7f-d92eb2266696"), "ESP" }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("17b9edee-d5b9-420a-a7cc-1e21c84969e9"), "Studio equipment" },
                    { new Guid("1821dfe5-c838-463c-a548-461959aee7e2"), "Cabinets" },
                    { new Guid("22b0015a-25fa-4af2-ad31-96ae910a4ab7"), "Accesories" },
                    { new Guid("6bda258e-0413-495e-9b90-39a95ed95333"), "Proccesors" },
                    { new Guid("6e99d0db-4c5c-48d7-988c-a48359eb5869"), "Amplifiers" },
                    { new Guid("78dea359-0b8d-491e-8d40-c03a25af508b"), "Classical guitar" },
                    { new Guid("9c4db1a9-65ac-4ad1-b353-5288997db565"), "Electric guitar" }
                });

            migrationBuilder.InsertData(
                table: "Statuses",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("372e7d2d-096c-43ae-8236-b7b8406ba4ae"), "Canceled" },
                    { new Guid("4b7144ea-86f5-4a49-9726-1c4a14289abf"), "Sended" },
                    { new Guid("eac14b9e-9003-49e7-919a-f5babd72beeb"), "Pending" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: new Guid("263e34fb-04fd-4e6d-abac-8ccdde1679f5"));

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: new Guid("37ba777e-8d56-4974-824e-56cc7562f1bc"));

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: new Guid("5b2bc7a3-d0bc-4eda-9067-5df9ef1ba20c"));

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: new Guid("b5bc612c-f024-4244-8a4d-af01677481e8"));

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: new Guid("d1f25d04-7c71-4337-ad55-de307ab602ae"));

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: new Guid("e38eea98-420d-4188-9e7f-d92eb2266696"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("17b9edee-d5b9-420a-a7cc-1e21c84969e9"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("1821dfe5-c838-463c-a548-461959aee7e2"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("22b0015a-25fa-4af2-ad31-96ae910a4ab7"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("6bda258e-0413-495e-9b90-39a95ed95333"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("6e99d0db-4c5c-48d7-988c-a48359eb5869"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("78dea359-0b8d-491e-8d40-c03a25af508b"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("9c4db1a9-65ac-4ad1-b353-5288997db565"));

            migrationBuilder.DeleteData(
                table: "Statuses",
                keyColumn: "Id",
                keyValue: new Guid("372e7d2d-096c-43ae-8236-b7b8406ba4ae"));

            migrationBuilder.DeleteData(
                table: "Statuses",
                keyColumn: "Id",
                keyValue: new Guid("4b7144ea-86f5-4a49-9726-1c4a14289abf"));

            migrationBuilder.DeleteData(
                table: "Statuses",
                keyColumn: "Id",
                keyValue: new Guid("eac14b9e-9003-49e7-919a-f5babd72beeb"));

            migrationBuilder.DropColumn(
                name: "UserName",
                table: "Reviews");

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
    }
}
