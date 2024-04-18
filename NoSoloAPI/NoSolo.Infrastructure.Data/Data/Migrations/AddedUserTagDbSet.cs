using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NoSolo.Infrastructure.Data.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedUserTagDbSet : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contact<UserProfile>_UserProfile_TEntityId",
                table: "ContactEntity<UserProfile>");

            migrationBuilder.DropForeignKey(
                name: "FK_Request<UserProfile, OrganizationOfferEntity>_UserProfile_TEntity~",
                table: "RequestEntity<UserProfile, OrganizationOfferEntity>");

            migrationBuilder.DropForeignKey(
                name: "FK_UserOffer_UserProfile_UserProfileId",
                table: "UserOfferEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_UserPhoto_UserProfile_UserProfileId",
                table: "UserPhotoEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_UserProfile_AspNetUsers_UserId",
                table: "UserProfile");

            migrationBuilder.DropForeignKey(
                name: "FK_UserTag_UserProfile_UserProfileId",
                table: "UserTagEntity");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserTag",
                table: "UserTagEntity");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserProfile",
                table: "UserProfile");

            migrationBuilder.RenameTable(
                name: "UserTagEntity",
                newName: "UserTags");

            migrationBuilder.RenameTable(
                name: "UserProfile",
                newName: "UserProfiles");

            migrationBuilder.RenameIndex(
                name: "IX_UserTag_UserProfileId",
                table: "UserTags",
                newName: "IX_UserTags_UserProfileId");

            migrationBuilder.RenameIndex(
                name: "IX_UserProfile_UserId",
                table: "UserProfiles",
                newName: "IX_UserProfiles_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserTags",
                table: "UserTags",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserProfiles",
                table: "UserProfiles",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Contact<UserProfile>_UserProfiles_TEntityId",
                table: "ContactEntity<UserProfile>",
                column: "TEntityId",
                principalTable: "UserProfiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Request<UserProfile, OrganizationOfferEntity>_UserProfiles_TEntit~",
                table: "RequestEntity<UserProfile, OrganizationOfferEntity>",
                column: "TEntityId",
                principalTable: "UserProfiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserOffer_UserProfiles_UserProfileId",
                table: "UserOfferEntity",
                column: "UserProfileId",
                principalTable: "UserProfiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserPhoto_UserProfiles_UserProfileId",
                table: "UserPhotoEntity",
                column: "UserProfileId",
                principalTable: "UserProfiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserProfiles_AspNetUsers_UserId",
                table: "UserProfiles",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserTags_UserProfiles_UserProfileId",
                table: "UserTags",
                column: "UserProfileId",
                principalTable: "UserProfiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contact<UserProfile>_UserProfiles_TEntityId",
                table: "ContactEntity<UserProfile>");

            migrationBuilder.DropForeignKey(
                name: "FK_Request<UserProfile, OrganizationOfferEntity>_UserProfiles_TEntit~",
                table: "RequestEntity<UserProfile, OrganizationOfferEntity>");

            migrationBuilder.DropForeignKey(
                name: "FK_UserOffer_UserProfiles_UserProfileId",
                table: "UserOfferEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_UserPhoto_UserProfiles_UserProfileId",
                table: "UserPhotoEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_UserProfiles_AspNetUsers_UserId",
                table: "UserProfiles");

            migrationBuilder.DropForeignKey(
                name: "FK_UserTags_UserProfiles_UserProfileId",
                table: "UserTags");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserTags",
                table: "UserTags");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserProfiles",
                table: "UserProfiles");

            migrationBuilder.RenameTable(
                name: "UserTags",
                newName: "UserTagEntity");

            migrationBuilder.RenameTable(
                name: "UserProfiles",
                newName: "UserProfile");

            migrationBuilder.RenameIndex(
                name: "IX_UserTags_UserProfileId",
                table: "UserTagEntity",
                newName: "IX_UserTag_UserProfileId");

            migrationBuilder.RenameIndex(
                name: "IX_UserProfiles_UserId",
                table: "UserProfile",
                newName: "IX_UserProfile_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserTag",
                table: "UserTagEntity",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserProfile",
                table: "UserProfile",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Contact<UserProfile>_UserProfile_TEntityId",
                table: "ContactEntity<UserProfile>",
                column: "TEntityId",
                principalTable: "UserProfile",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Request<UserProfile, OrganizationOfferEntity>_UserProfile_TEntity~",
                table: "RequestEntity<UserProfile, OrganizationOfferEntity>",
                column: "TEntityId",
                principalTable: "UserProfile",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserOffer_UserProfile_UserProfileId",
                table: "UserOfferEntity",
                column: "UserProfileId",
                principalTable: "UserProfile",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserPhoto_UserProfile_UserProfileId",
                table: "UserPhotoEntity",
                column: "UserProfileId",
                principalTable: "UserProfile",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserProfile_AspNetUsers_UserId",
                table: "UserProfile",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserTag_UserProfile_UserProfileId",
                table: "UserTagEntity",
                column: "UserProfileId",
                principalTable: "UserProfile",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
