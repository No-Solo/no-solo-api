using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NoSolo.Infrastructure.Data.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedRelationalForUserAndORganization : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contact<Organization>_Organizations_TEntityId1",
                table: "Contact<Organization>");

            migrationBuilder.DropForeignKey(
                name: "FK_Contact<User>_AspNetUsers_TEntityId1",
                table: "Contact<User>");

            migrationBuilder.DropForeignKey(
                name: "FK_OrganizationOffer_Organizations_OrganizationId1",
                table: "OrganizationOffer");

            migrationBuilder.DropForeignKey(
                name: "FK_OrganizationPhoto_Organizations_OrganizationId1",
                table: "OrganizationPhoto");

            migrationBuilder.DropForeignKey(
                name: "FK_UserOffers_AspNetUsers_UserGuid",
                table: "UserOffers");

            migrationBuilder.DropForeignKey(
                name: "FK_UserTags_AspNetUsers_UserGuid",
                table: "UserTags");

            migrationBuilder.DropIndex(
                name: "IX_UserTags_UserGuid",
                table: "UserTags");

            migrationBuilder.DropIndex(
                name: "IX_UserOffers_UserGuid",
                table: "UserOffers");

            migrationBuilder.DropIndex(
                name: "IX_OrganizationPhoto_OrganizationId1",
                table: "OrganizationPhoto");

            migrationBuilder.DropIndex(
                name: "IX_OrganizationOffer_OrganizationId1",
                table: "OrganizationOffer");

            migrationBuilder.DropIndex(
                name: "IX_Contact<User>_TEntityId1",
                table: "Contact<User>");

            migrationBuilder.DropIndex(
                name: "IX_Contact<Organization>_TEntityId1",
                table: "Contact<Organization>");

            migrationBuilder.DropColumn(
                name: "OrganizationId1",
                table: "OrganizationPhoto");

            migrationBuilder.DropColumn(
                name: "OrganizationId1",
                table: "OrganizationOffer");

            migrationBuilder.DropColumn(
                name: "TEntityId1",
                table: "Contact<User>");

            migrationBuilder.DropColumn(
                name: "TEntityId1",
                table: "Contact<Organization>");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "OrganizationId1",
                table: "OrganizationPhoto",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "OrganizationId1",
                table: "OrganizationOffer",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "TEntityId1",
                table: "Contact<User>",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "TEntityId1",
                table: "Contact<Organization>",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserTags_UserGuid",
                table: "UserTags",
                column: "UserGuid");

            migrationBuilder.CreateIndex(
                name: "IX_UserOffers_UserGuid",
                table: "UserOffers",
                column: "UserGuid");

            migrationBuilder.CreateIndex(
                name: "IX_OrganizationPhoto_OrganizationId1",
                table: "OrganizationPhoto",
                column: "OrganizationId1");

            migrationBuilder.CreateIndex(
                name: "IX_OrganizationOffer_OrganizationId1",
                table: "OrganizationOffer",
                column: "OrganizationId1");

            migrationBuilder.CreateIndex(
                name: "IX_Contact<User>_TEntityId1",
                table: "Contact<User>",
                column: "TEntityId1");

            migrationBuilder.CreateIndex(
                name: "IX_Contact<Organization>_TEntityId1",
                table: "Contact<Organization>",
                column: "TEntityId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Contact<Organization>_Organizations_TEntityId1",
                table: "Contact<Organization>",
                column: "TEntityId1",
                principalTable: "Organizations",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Contact<User>_AspNetUsers_TEntityId1",
                table: "Contact<User>",
                column: "TEntityId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OrganizationOffer_Organizations_OrganizationId1",
                table: "OrganizationOffer",
                column: "OrganizationId1",
                principalTable: "Organizations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrganizationPhoto_Organizations_OrganizationId1",
                table: "OrganizationPhoto",
                column: "OrganizationId1",
                principalTable: "Organizations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserOffers_AspNetUsers_UserGuid",
                table: "UserOffers",
                column: "UserGuid",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserTags_AspNetUsers_UserGuid",
                table: "UserTags",
                column: "UserGuid",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
