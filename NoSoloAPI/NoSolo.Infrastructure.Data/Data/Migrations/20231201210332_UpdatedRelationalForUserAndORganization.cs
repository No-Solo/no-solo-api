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
                name: "FK_Contact<OrganizationEntity>_Organizations_TEntityId1",
                table: "ContactEntity<OrganizationEntity>");

            migrationBuilder.DropForeignKey(
                name: "FK_Contact<UserEntity>_AspNetUsers_TEntityId1",
                table: "ContactEntity<UserEntity>");

            migrationBuilder.DropForeignKey(
                name: "FK_OrganizationOffer_Organizations_OrganizationId1",
                table: "OrganizationOfferEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_OrganizationPhoto_Organizations_OrganizationId1",
                table: "OrganizationPhotoEntity");

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
                table: "OrganizationPhotoEntity");

            migrationBuilder.DropIndex(
                name: "IX_OrganizationOffer_OrganizationId1",
                table: "OrganizationOfferEntity");

            migrationBuilder.DropIndex(
                name: "IX_Contact<UserEntity>_TEntityId1",
                table: "ContactEntity<UserEntity>");

            migrationBuilder.DropIndex(
                name: "IX_Contact<OrganizationEntity>_TEntityId1",
                table: "ContactEntity<OrganizationEntity>");

            migrationBuilder.DropColumn(
                name: "OrganizationId1",
                table: "OrganizationPhotoEntity");

            migrationBuilder.DropColumn(
                name: "OrganizationId1",
                table: "OrganizationOfferEntity");

            migrationBuilder.DropColumn(
                name: "TEntityId1",
                table: "ContactEntity<UserEntity>");

            migrationBuilder.DropColumn(
                name: "TEntityId1",
                table: "ContactEntity<OrganizationEntity>");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "OrganizationId1",
                table: "OrganizationPhotoEntity",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "OrganizationId1",
                table: "OrganizationOfferEntity",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "TEntityId1",
                table: "ContactEntity<UserEntity>",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "TEntityId1",
                table: "ContactEntity<OrganizationEntity>",
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
                table: "OrganizationPhotoEntity",
                column: "OrganizationId1");

            migrationBuilder.CreateIndex(
                name: "IX_OrganizationOffer_OrganizationId1",
                table: "OrganizationOfferEntity",
                column: "OrganizationId1");

            migrationBuilder.CreateIndex(
                name: "IX_Contact<UserEntity>_TEntityId1",
                table: "ContactEntity<UserEntity>",
                column: "TEntityId1");

            migrationBuilder.CreateIndex(
                name: "IX_Contact<OrganizationEntity>_TEntityId1",
                table: "ContactEntity<OrganizationEntity>",
                column: "TEntityId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Contact<OrganizationEntity>_Organizations_TEntityId1",
                table: "ContactEntity<OrganizationEntity>",
                column: "TEntityId1",
                principalTable: "Organizations",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Contact<UserEntity>_AspNetUsers_TEntityId1",
                table: "ContactEntity<UserEntity>",
                column: "TEntityId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OrganizationOffer_Organizations_OrganizationId1",
                table: "OrganizationOfferEntity",
                column: "OrganizationId1",
                principalTable: "Organizations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrganizationPhoto_Organizations_OrganizationId1",
                table: "OrganizationPhotoEntity",
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
