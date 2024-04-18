using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NoSolo.Infrastructure.Data.Data.Migrations
{
    /// <inheritdoc />
    public partial class BigUpdateMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserOffers_UserProfiles_UserProfileId",
                table: "UserOffers");

            migrationBuilder.DropForeignKey(
                name: "FK_UserPhoto_UserProfiles_UserProfileId",
                table: "UserPhotoEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_UserTags_UserProfiles_UserProfileId",
                table: "UserTags");

            migrationBuilder.DropTable(
                name: "ContactEntity<UserProfile>");

            migrationBuilder.DropTable(
                name: "Projects");

            migrationBuilder.DropTable(
                name: "RequestEntity<UserProfile, OrganizationOfferEntity>");

            migrationBuilder.DropTable(
                name: "UserProfiles");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "UserTags");

            migrationBuilder.RenameColumn(
                name: "UserProfileId",
                table: "UserTags",
                newName: "UserGuid");

            migrationBuilder.RenameIndex(
                name: "IX_UserTags_UserProfileId",
                table: "UserTags",
                newName: "IX_UserTags_UserGuid");

            migrationBuilder.RenameColumn(
                name: "UserProfileId",
                table: "UserPhotoEntity",
                newName: "UserGuid");

            migrationBuilder.RenameIndex(
                name: "IX_UserPhoto_UserProfileId",
                table: "UserPhotoEntity",
                newName: "IX_UserPhoto_UserGuid");

            migrationBuilder.RenameColumn(
                name: "UserProfileId",
                table: "UserOffers",
                newName: "UserGuid");

            migrationBuilder.RenameIndex(
                name: "IX_UserOffers_UserProfileId",
                table: "UserOffers",
                newName: "IX_UserOffers_UserGuid");

            migrationBuilder.AlterColumn<string>(
                name: "Tag",
                table: "UserTags",
                type: "character varying(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "UserTags",
                type: "uuid",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Url",
                table: "UserPhotoEntity",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Preferences",
                table: "UserOffers",
                type: "character varying(150)",
                maxLength: 150,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "UserOffers",
                type: "uuid",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Organizations",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Organizations",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Organizations",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastUpdated",
                table: "Organizations",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "NumberOfEmployees",
                table: "Organizations",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WebSiteUrl",
                table: "Organizations",
                type: "text",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Url",
                table: "OrganizationPhotoEntity",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "OrganizationId1",
                table: "OrganizationPhotoEntity",
                type: "uuid",
                nullable: true);

            migrationBuilder.AlterColumn<List<string>>(
                name: "Tags",
                table: "OrganizationOfferEntity",
                type: "text[]",
                nullable: true,
                oldClrType: typeof(int[]),
                oldType: "integer[]",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "OrganizationOfferEntity",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "OrganizationOfferEntity",
                type: "character varying(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "OrganizationId1",
                table: "OrganizationOfferEntity",
                type: "uuid",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Url",
                table: "ContactEntity<OrganizationEntity>",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Type",
                table: "ContactEntity<OrganizationEntity>",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Text",
                table: "ContactEntity<OrganizationEntity>",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "TEntityId1",
                table: "ContactEntity<OrganizationEntity>",
                type: "uuid",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UserName",
                table: "AspNetUsers",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "character varying(256)",
                oldMaxLength: 256,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PhoneNumber",
                table: "AspNetUsers",
                type: "character varying(10)",
                maxLength: 10,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "About",
                table: "AspNetUsers",
                type: "character varying(250)",
                maxLength: 250,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "AspNetUsers",
                type: "character varying(250)",
                maxLength: 250,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "AspNetUsers",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Gender",
                table: "AspNetUsers",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "AspNetUsers",
                type: "character varying(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Locale",
                table: "AspNetUsers",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Location",
                table: "AspNetUsers",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "MiddleName",
                table: "AspNetUsers",
                type: "character varying(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Sponsorship",
                table: "AspNetUsers",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "ContactEntity<UserEntity>",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TEntityId1 = table.Column<Guid>(type: "uuid", nullable: true),
                    TEntityId = table.Column<Guid>(type: "uuid", nullable: false),
                    Type = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Url = table.Column<string>(type: "text", nullable: false),
                    Text = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contact<UserEntity>", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Contact<UserEntity>_AspNetUsers_TEntityId",
                        column: x => x.TEntityId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Contact<UserEntity>_AspNetUsers_TEntityId1",
                        column: x => x.TEntityId1,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "RequestEntity<UserEntity, OrganizationOfferEntity>",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TEntityId = table.Column<Guid>(type: "uuid", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    UEntityId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Request<UserEntity, OrganizationOfferEntity>", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Request<UserEntity, OrganizationOfferEntity>_AspNetUsers_TEntityId",
                        column: x => x.TEntityId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Request<UserEntity, OrganizationOfferEntity>_OrganizationOffer_UEntityId",
                        column: x => x.UEntityId,
                        principalTable: "OrganizationOfferEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserTags_UserId",
                table: "UserTags",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserOffers_UserId",
                table: "UserOffers",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_OrganizationPhoto_OrganizationId1",
                table: "OrganizationPhotoEntity",
                column: "OrganizationId1");

            migrationBuilder.CreateIndex(
                name: "IX_OrganizationOffer_OrganizationId1",
                table: "OrganizationOfferEntity",
                column: "OrganizationId1");

            migrationBuilder.CreateIndex(
                name: "IX_Contact<OrganizationEntity>_TEntityId1",
                table: "ContactEntity<OrganizationEntity>",
                column: "TEntityId1");

            migrationBuilder.CreateIndex(
                name: "IX_Contact<UserEntity>_TEntityId",
                table: "ContactEntity<UserEntity>",
                column: "TEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_Contact<UserEntity>_TEntityId1",
                table: "ContactEntity<UserEntity>",
                column: "TEntityId1");

            migrationBuilder.CreateIndex(
                name: "IX_Request<UserEntity, OrganizationOfferEntity>_TEntityId",
                table: "RequestEntity<UserEntity, OrganizationOfferEntity>",
                column: "TEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_Request<UserEntity, OrganizationOfferEntity>_UEntityId",
                table: "RequestEntity<UserEntity, OrganizationOfferEntity>",
                column: "UEntityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Contact<OrganizationEntity>_Organizations_TEntityId1",
                table: "ContactEntity<OrganizationEntity>",
                column: "TEntityId1",
                principalTable: "Organizations",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OrganizationOffer_Organizations_OrganizationId1",
                table: "OrganizationOfferEntity",
                column: "OrganizationId1",
                principalTable: "Organizations",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OrganizationPhoto_Organizations_OrganizationId1",
                table: "OrganizationPhotoEntity",
                column: "OrganizationId1",
                principalTable: "Organizations",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserOffers_AspNetUsers_UserGuid",
                table: "UserOffers",
                column: "UserGuid",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserOffers_AspNetUsers_UserId",
                table: "UserOffers",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserPhoto_AspNetUsers_UserGuid",
                table: "UserPhotoEntity",
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

            migrationBuilder.AddForeignKey(
                name: "FK_UserTags_AspNetUsers_UserId",
                table: "UserTags",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contact<OrganizationEntity>_Organizations_TEntityId1",
                table: "ContactEntity<OrganizationEntity>");

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
                name: "FK_UserOffers_AspNetUsers_UserId",
                table: "UserOffers");

            migrationBuilder.DropForeignKey(
                name: "FK_UserPhoto_AspNetUsers_UserGuid",
                table: "UserPhotoEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_UserTags_AspNetUsers_UserGuid",
                table: "UserTags");

            migrationBuilder.DropForeignKey(
                name: "FK_UserTags_AspNetUsers_UserId",
                table: "UserTags");

            migrationBuilder.DropTable(
                name: "ContactEntity<UserEntity>");

            migrationBuilder.DropTable(
                name: "RequestEntity<UserEntity, OrganizationOfferEntity>");

            migrationBuilder.DropIndex(
                name: "IX_UserTags_UserId",
                table: "UserTags");

            migrationBuilder.DropIndex(
                name: "IX_UserOffers_UserId",
                table: "UserOffers");

            migrationBuilder.DropIndex(
                name: "IX_OrganizationPhoto_OrganizationId1",
                table: "OrganizationPhotoEntity");

            migrationBuilder.DropIndex(
                name: "IX_OrganizationOffer_OrganizationId1",
                table: "OrganizationOfferEntity");

            migrationBuilder.DropIndex(
                name: "IX_Contact<OrganizationEntity>_TEntityId1",
                table: "ContactEntity<OrganizationEntity>");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "UserTags");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "UserOffers");

            migrationBuilder.DropColumn(
                name: "Address",
                table: "Organizations");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Organizations");

            migrationBuilder.DropColumn(
                name: "LastUpdated",
                table: "Organizations");

            migrationBuilder.DropColumn(
                name: "NumberOfEmployees",
                table: "Organizations");

            migrationBuilder.DropColumn(
                name: "WebSiteUrl",
                table: "Organizations");

            migrationBuilder.DropColumn(
                name: "OrganizationId1",
                table: "OrganizationPhotoEntity");

            migrationBuilder.DropColumn(
                name: "OrganizationId1",
                table: "OrganizationOfferEntity");

            migrationBuilder.DropColumn(
                name: "TEntityId1",
                table: "ContactEntity<OrganizationEntity>");

            migrationBuilder.DropColumn(
                name: "About",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Gender",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Locale",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Location",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "MiddleName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Sponsorship",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "UserGuid",
                table: "UserTags",
                newName: "UserProfileId");

            migrationBuilder.RenameIndex(
                name: "IX_UserTags_UserGuid",
                table: "UserTags",
                newName: "IX_UserTags_UserProfileId");

            migrationBuilder.RenameColumn(
                name: "UserGuid",
                table: "UserPhotoEntity",
                newName: "UserProfileId");

            migrationBuilder.RenameIndex(
                name: "IX_UserPhoto_UserGuid",
                table: "UserPhotoEntity",
                newName: "IX_UserPhoto_UserProfileId");

            migrationBuilder.RenameColumn(
                name: "UserGuid",
                table: "UserOffers",
                newName: "UserProfileId");

            migrationBuilder.RenameIndex(
                name: "IX_UserOffers_UserGuid",
                table: "UserOffers",
                newName: "IX_UserOffers_UserProfileId");

            migrationBuilder.AlterColumn<int>(
                name: "Tag",
                table: "UserTags",
                type: "integer",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(20)",
                oldMaxLength: 20);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "UserTags",
                type: "text",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Url",
                table: "UserPhotoEntity",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Preferences",
                table: "UserOffers",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(150)",
                oldMaxLength: 150);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Organizations",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Url",
                table: "OrganizationPhotoEntity",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<int[]>(
                name: "Tags",
                table: "OrganizationOfferEntity",
                type: "integer[]",
                nullable: true,
                oldClrType: typeof(List<string>),
                oldType: "text[]",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "OrganizationOfferEntity",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "OrganizationOfferEntity",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(500)",
                oldMaxLength: 500);

            migrationBuilder.AlterColumn<string>(
                name: "Url",
                table: "ContactEntity<OrganizationEntity>",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Type",
                table: "ContactEntity<OrganizationEntity>",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "Text",
                table: "ContactEntity<OrganizationEntity>",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "UserName",
                table: "AspNetUsers",
                type: "character varying(256)",
                maxLength: 256,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "PhoneNumber",
                table: "AspNetUsers",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(10)",
                oldMaxLength: 10,
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "Projects",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    OrganizationId = table.Column<Guid>(type: "uuid", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projects", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Projects_Organizations_OrganizationId",
                        column: x => x.OrganizationId,
                        principalTable: "Organizations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserProfiles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    About = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    FirstName = table.Column<string>(type: "text", nullable: true),
                    Gender = table.Column<int>(type: "integer", nullable: false),
                    LastName = table.Column<string>(type: "text", nullable: true),
                    Locale = table.Column<int>(type: "integer", nullable: false),
                    Location = table.Column<string>(type: "text", nullable: true),
                    MiddleName = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserProfiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserProfiles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ContactEntity<UserProfile>",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TEntityId = table.Column<Guid>(type: "uuid", nullable: false),
                    Text = table.Column<string>(type: "text", nullable: true),
                    Type = table.Column<string>(type: "text", nullable: true),
                    Url = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contact<UserProfile>", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Contact<UserProfile>_UserProfiles_TEntityId",
                        column: x => x.TEntityId,
                        principalTable: "UserProfiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RequestEntity<UserProfile, OrganizationOfferEntity>",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TEntityId = table.Column<Guid>(type: "uuid", nullable: false),
                    UEntityId = table.Column<Guid>(type: "uuid", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Request<UserProfile, OrganizationOfferEntity>", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Request<UserProfile, OrganizationOfferEntity>_OrganizationOffer_U~",
                        column: x => x.UEntityId,
                        principalTable: "OrganizationOfferEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Request<UserProfile, OrganizationOfferEntity>_UserProfiles_TEntit~",
                        column: x => x.TEntityId,
                        principalTable: "UserProfiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Contact<UserProfile>_TEntityId",
                table: "ContactEntity<UserProfile>",
                column: "TEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_OrganizationId",
                table: "Projects",
                column: "OrganizationId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Request<UserProfile, OrganizationOfferEntity>_TEntityId",
                table: "RequestEntity<UserProfile, OrganizationOfferEntity>",
                column: "TEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_Request<UserProfile, OrganizationOfferEntity>_UEntityId",
                table: "RequestEntity<UserProfile, OrganizationOfferEntity>",
                column: "UEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_UserProfiles_UserId",
                table: "UserProfiles",
                column: "UserId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_UserOffers_UserProfiles_UserProfileId",
                table: "UserOffers",
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
                name: "FK_UserTags_UserProfiles_UserProfileId",
                table: "UserTags",
                column: "UserProfileId",
                principalTable: "UserProfiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
