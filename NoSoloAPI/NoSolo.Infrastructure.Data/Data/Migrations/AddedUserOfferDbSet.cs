using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NoSolo.Infrastructure.Data.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedUserOfferDbSet : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Request<OrganizationEntity, UserOfferEntity>_UserOffer_UEntityId",
                table: "RequestEntity<OrganizationEntity, UserOfferEntity>");

            migrationBuilder.DropForeignKey(
                name: "FK_UserOffer_UserProfiles_UserProfileId",
                table: "UserOfferEntity");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserOffer",
                table: "UserOfferEntity");

            migrationBuilder.RenameTable(
                name: "UserOfferEntity",
                newName: "UserOffers");

            migrationBuilder.RenameIndex(
                name: "IX_UserOffer_UserProfileId",
                table: "UserOffers",
                newName: "IX_UserOffers_UserProfileId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserOffers",
                table: "UserOffers",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Request<OrganizationEntity, UserOfferEntity>_UserOffers_UEntityId",
                table: "RequestEntity<OrganizationEntity, UserOfferEntity>",
                column: "UEntityId",
                principalTable: "UserOffers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserOffers_UserProfiles_UserProfileId",
                table: "UserOffers",
                column: "UserProfileId",
                principalTable: "UserProfiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Request<OrganizationEntity, UserOfferEntity>_UserOffers_UEntityId",
                table: "RequestEntity<OrganizationEntity, UserOfferEntity>");

            migrationBuilder.DropForeignKey(
                name: "FK_UserOffers_UserProfiles_UserProfileId",
                table: "UserOffers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserOffers",
                table: "UserOffers");

            migrationBuilder.RenameTable(
                name: "UserOffers",
                newName: "UserOfferEntity");

            migrationBuilder.RenameIndex(
                name: "IX_UserOffers_UserProfileId",
                table: "UserOfferEntity",
                newName: "IX_UserOffer_UserProfileId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserOffer",
                table: "UserOfferEntity",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Request<OrganizationEntity, UserOfferEntity>_UserOffer_UEntityId",
                table: "RequestEntity<OrganizationEntity, UserOfferEntity>",
                column: "UEntityId",
                principalTable: "UserOfferEntity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserOffer_UserProfiles_UserProfileId",
                table: "UserOfferEntity",
                column: "UserProfileId",
                principalTable: "UserProfiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
