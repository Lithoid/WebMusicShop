using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StoreWebApi.Migrations
{
    public partial class mig10 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.InsertData(
                table: "Brands",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("1f05112e-0e94-4445-bbea-cd25f8e3bf25"), "Ibanez" },
                    { new Guid("6f32bcf0-f370-4588-81f5-c4bf8a4adacf"), "Yamaha" },
                    { new Guid("ce07fd34-a263-40ed-8110-74db16a8576f"), "Marshal" },
                    { new Guid("cf7ff1e7-d1c7-4555-95d5-e6e6582d3b83"), "Fender" },
                    { new Guid("fad7f2bf-da9b-4e43-9f36-2c44b401262a"), "ESP" },
                    { new Guid("fe54a0c0-f208-4350-a1b6-b44da458a176"), "Line6" }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("03658b8a-ec48-4dc2-96df-05bb8ed0f77e"), "Electric guitar" },
                    { new Guid("401f09ab-c8ec-424d-926f-13bd56ce90a7"), "Amplifiers" },
                    { new Guid("57dad653-79a6-4e4f-aba5-e69a9a05301e"), "Classical guitar" },
                    { new Guid("5bedc214-57ee-48c3-8288-4f8f21a407e6"), "Cabinets" },
                    { new Guid("643b7609-8c8b-4c23-9a36-c9d6c919ba04"), "Accesories" },
                    { new Guid("7f44950d-eec2-4d59-b044-e18426a55367"), "Proccesors" },
                    { new Guid("c541509c-5d83-45fa-80dc-21e87cfc3164"), "Studio equipment" }
                });

            migrationBuilder.InsertData(
                table: "Statuses",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("10f2e367-6708-4f29-9e5b-13011f17df95"), "Sended" },
                    { new Guid("7e3c6884-237e-4c96-9aab-96975126e7d5"), "Pending" },
                    { new Guid("ccf686d4-588c-4c2a-9e70-fc8b9908b19e"), "Canceled" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: new Guid("1f05112e-0e94-4445-bbea-cd25f8e3bf25"));

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: new Guid("6f32bcf0-f370-4588-81f5-c4bf8a4adacf"));

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: new Guid("ce07fd34-a263-40ed-8110-74db16a8576f"));

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: new Guid("cf7ff1e7-d1c7-4555-95d5-e6e6582d3b83"));

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: new Guid("fad7f2bf-da9b-4e43-9f36-2c44b401262a"));

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: new Guid("fe54a0c0-f208-4350-a1b6-b44da458a176"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("03658b8a-ec48-4dc2-96df-05bb8ed0f77e"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("401f09ab-c8ec-424d-926f-13bd56ce90a7"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("57dad653-79a6-4e4f-aba5-e69a9a05301e"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("5bedc214-57ee-48c3-8288-4f8f21a407e6"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("643b7609-8c8b-4c23-9a36-c9d6c919ba04"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("7f44950d-eec2-4d59-b044-e18426a55367"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("c541509c-5d83-45fa-80dc-21e87cfc3164"));

            migrationBuilder.DeleteData(
                table: "Statuses",
                keyColumn: "Id",
                keyValue: new Guid("10f2e367-6708-4f29-9e5b-13011f17df95"));

            migrationBuilder.DeleteData(
                table: "Statuses",
                keyColumn: "Id",
                keyValue: new Guid("7e3c6884-237e-4c96-9aab-96975126e7d5"));

            migrationBuilder.DeleteData(
                table: "Statuses",
                keyColumn: "Id",
                keyValue: new Guid("ccf686d4-588c-4c2a-9e70-fc8b9908b19e"));

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
    }
}
