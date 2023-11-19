using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NoSolo.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedUserOfferDbSet : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Request<Organization, UserOffer>_UserOffer_UEntityId",
                table: "Request<Organization, UserOffer>");

            migrationBuilder.DropForeignKey(
                name: "FK_UserOffer_UserProfiles_UserProfileId",
                table: "UserOffer");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserOffer",
                table: "UserOffer");

            migrationBuilder.RenameTable(
                name: "UserOffer",
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
                name: "FK_Request<Organization, UserOffer>_UserOffers_UEntityId",
                table: "Request<Organization, UserOffer>",
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
                name: "FK_Request<Organization, UserOffer>_UserOffers_UEntityId",
                table: "Request<Organization, UserOffer>");

            migrationBuilder.DropForeignKey(
                name: "FK_UserOffers_UserProfiles_UserProfileId",
                table: "UserOffers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserOffers",
                table: "UserOffers");

            migrationBuilder.RenameTable(
                name: "UserOffers",
                newName: "UserOffer");

            migrationBuilder.RenameIndex(
                name: "IX_UserOffers_UserProfileId",
                table: "UserOffer",
                newName: "IX_UserOffer_UserProfileId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserOffer",
                table: "UserOffer",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Request<Organization, UserOffer>_UserOffer_UEntityId",
                table: "Request<Organization, UserOffer>",
                column: "UEntityId",
                principalTable: "UserOffer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserOffer_UserProfiles_UserProfileId",
                table: "UserOffer",
                column: "UserProfileId",
                principalTable: "UserProfiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
