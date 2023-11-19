using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NoSolo.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedUserTagDbSet : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contact<UserProfile>_UserProfile_TEntityId",
                table: "Contact<UserProfile>");

            migrationBuilder.DropForeignKey(
                name: "FK_Request<UserProfile, OrganizationOffer>_UserProfile_TEntity~",
                table: "Request<UserProfile, OrganizationOffer>");

            migrationBuilder.DropForeignKey(
                name: "FK_UserOffer_UserProfile_UserProfileId",
                table: "UserOffer");

            migrationBuilder.DropForeignKey(
                name: "FK_UserPhoto_UserProfile_UserProfileId",
                table: "UserPhoto");

            migrationBuilder.DropForeignKey(
                name: "FK_UserProfile_AspNetUsers_UserId",
                table: "UserProfile");

            migrationBuilder.DropForeignKey(
                name: "FK_UserTag_UserProfile_UserProfileId",
                table: "UserTag");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserTag",
                table: "UserTag");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserProfile",
                table: "UserProfile");

            migrationBuilder.RenameTable(
                name: "UserTag",
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
                table: "Contact<UserProfile>",
                column: "TEntityId",
                principalTable: "UserProfiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Request<UserProfile, OrganizationOffer>_UserProfiles_TEntit~",
                table: "Request<UserProfile, OrganizationOffer>",
                column: "TEntityId",
                principalTable: "UserProfiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserOffer_UserProfiles_UserProfileId",
                table: "UserOffer",
                column: "UserProfileId",
                principalTable: "UserProfiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserPhoto_UserProfiles_UserProfileId",
                table: "UserPhoto",
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
                table: "Contact<UserProfile>");

            migrationBuilder.DropForeignKey(
                name: "FK_Request<UserProfile, OrganizationOffer>_UserProfiles_TEntit~",
                table: "Request<UserProfile, OrganizationOffer>");

            migrationBuilder.DropForeignKey(
                name: "FK_UserOffer_UserProfiles_UserProfileId",
                table: "UserOffer");

            migrationBuilder.DropForeignKey(
                name: "FK_UserPhoto_UserProfiles_UserProfileId",
                table: "UserPhoto");

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
                newName: "UserTag");

            migrationBuilder.RenameTable(
                name: "UserProfiles",
                newName: "UserProfile");

            migrationBuilder.RenameIndex(
                name: "IX_UserTags_UserProfileId",
                table: "UserTag",
                newName: "IX_UserTag_UserProfileId");

            migrationBuilder.RenameIndex(
                name: "IX_UserProfiles_UserId",
                table: "UserProfile",
                newName: "IX_UserProfile_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserTag",
                table: "UserTag",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserProfile",
                table: "UserProfile",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Contact<UserProfile>_UserProfile_TEntityId",
                table: "Contact<UserProfile>",
                column: "TEntityId",
                principalTable: "UserProfile",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Request<UserProfile, OrganizationOffer>_UserProfile_TEntity~",
                table: "Request<UserProfile, OrganizationOffer>",
                column: "TEntityId",
                principalTable: "UserProfile",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserOffer_UserProfile_UserProfileId",
                table: "UserOffer",
                column: "UserProfileId",
                principalTable: "UserProfile",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserPhoto_UserProfile_UserProfileId",
                table: "UserPhoto",
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
                table: "UserTag",
                column: "UserProfileId",
                principalTable: "UserProfile",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
