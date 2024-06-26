﻿// <auto-generated />
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NoSolo.Infrastructure.Data.DbContext;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace NoSolo.Infrastructure.Data.Data.Migrations
{
    [DbContext(typeof(DataBaseContext))]
    [Migration("20231125203942_UpdateUserEntity")]
    partial class UpdateUserEntity
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("text");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("text");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("text");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("text");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("text");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uuid");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("Value")
                        .HasColumnType("text");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("NoSolo.Core.Entities.Auth.RefreshToken", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("ExpiryDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("TokenHash")
                        .HasColumnType("text");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("RefreshTokens");
                });

            modelBuilder.Entity("NoSolo.Core.Entities.Base.ContactEntity<NoSolo.Core.Entities.OrganizationEntity.OrganizationEntity>", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("TEntityId")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("TEntityId1")
                        .HasColumnType("uuid");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("TEntityId");

                    b.HasIndex("TEntityId1");

                    b.ToTable("ContactEntity<OrganizationEntity>");
                });

            modelBuilder.Entity("NoSolo.Core.Entities.Base.ContactEntity<NoSolo.Core.Entities.UserEntity.UserEntity>", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("TEntityId")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("TEntityId1")
                        .HasColumnType("uuid");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("TEntityId");

                    b.HasIndex("TEntityId1");

                    b.ToTable("ContactEntity<UserEntity>");
                });

            modelBuilder.Entity("NoSolo.Core.Entities.Base.RequestEntity<NoSolo.Core.Entities.OrganizationEntity.OrganizationEntity, NoSolo.Core.Entities.UserEntity.UserOfferEntity>", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.Property<Guid>("TEntityId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("UEntityId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("TEntityId");

                    b.HasIndex("UEntityId");

                    b.ToTable("RequestEntity<OrganizationEntity, UserOfferEntity>");
                });

            modelBuilder.Entity("NoSolo.Core.Entities.Base.RequestEntity<NoSolo.Core.Entities.UserEntity.UserEntity, NoSolo.Core.Entities.OrganizationEntity.OrganizationOfferEntity>", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.Property<Guid>("TEntityId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("UEntityId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("TEntityId");

                    b.HasIndex("UEntityId");

                    b.ToTable("RequestEntity<UserEntity, OrganizationOfferEntity>");
                });

            modelBuilder.Entity("NoSolo.Core.Entities.OrganizationEntity.MemberEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("OrganizationId")
                        .HasColumnType("uuid");

                    b.Property<int>("Role")
                        .HasColumnType("integer");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("OrganizationId");

                    b.HasIndex("UserId");

                    b.ToTable("MemberEntity");
                });

            modelBuilder.Entity("NoSolo.Core.Entities.OrganizationEntity.OrganizationEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Address")
                        .HasColumnType("text");

                    b.Property<DateTime>("Created")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<DateTime>("LastUpdated")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<int?>("NumberOfEmployees")
                        .HasColumnType("integer");

                    b.Property<string>("PhotoUrl")
                        .HasColumnType("text");

                    b.Property<string>("WebSiteUrl")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Organizations");
                });

            modelBuilder.Entity("NoSolo.Core.Entities.OrganizationEntity.OrganizationOfferEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("Created")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("character varying(500)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<Guid>("OrganizationId")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("OrganizationId1")
                        .HasColumnType("uuid");

                    b.Property<List<string>>("Tags")
                        .HasColumnType("text[]");

                    b.HasKey("Id");

                    b.HasIndex("OrganizationId");

                    b.HasIndex("OrganizationId1");

                    b.ToTable("OrganizationOfferEntity");
                });

            modelBuilder.Entity("NoSolo.Core.Entities.OrganizationEntity.OrganizationPhotoEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<bool>("IsMain")
                        .HasColumnType("boolean");

                    b.Property<Guid>("OrganizationId")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("OrganizationId1")
                        .HasColumnType("uuid");

                    b.Property<string>("PublicId")
                        .HasColumnType("text");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("OrganizationId");

                    b.HasIndex("OrganizationId1");

                    b.ToTable("OrganizationPhotoEntity");
                });

            modelBuilder.Entity("NoSolo.Core.Entities.UserEntity.UserEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("About")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("character varying(250)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("integer");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("text");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("character varying(250)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("boolean");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<int>("Gender")
                        .HasColumnType("integer");

                    b.Property<string>("LastName")
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<int>("Locale")
                        .HasColumnType("integer");

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("boolean");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("MiddleName")
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("text");

                    b.Property<string>("PhoneNumber")
                        .HasMaxLength(10)
                        .HasColumnType("character varying(10)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("boolean");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("text");

                    b.Property<int>("Sponsorship")
                        .HasColumnType("integer");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("boolean");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("NoSolo.Core.Entities.UserEntity.UserOfferEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("Created")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Preferences")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("character varying(150)");

                    b.Property<Guid>("UserGuid")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("UserGuid");

                    b.HasIndex("UserId");

                    b.ToTable("UserOffers");
                });

            modelBuilder.Entity("NoSolo.Core.Entities.UserEntity.UserPhotoEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("PublicId")
                        .HasColumnType("text");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("UserGuid")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("UserGuid")
                        .IsUnique();

                    b.ToTable("UserPhotoEntity");
                });

            modelBuilder.Entity("NoSolo.Core.Entities.UserEntity.UserRoleEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("NoSolo.Core.Entities.UserEntity.UserTagEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<bool>("Active")
                        .HasColumnType("boolean");

                    b.Property<string>("Tag")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)");

                    b.Property<Guid>("UserGuid")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("UserGuid");

                    b.HasIndex("UserId");

                    b.ToTable("UserTags");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.HasOne("NoSolo.Core.Entities.UserEntity.UserRoleEntity", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
                {
                    b.HasOne("NoSolo.Core.Entities.UserEntity.UserEntity", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.HasOne("NoSolo.Core.Entities.UserEntity.UserEntity", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<System.Guid>", b =>
                {
                    b.HasOne("NoSolo.Core.Entities.UserEntity.UserRoleEntity", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("NoSolo.Core.Entities.UserEntity.UserEntity", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
                {
                    b.HasOne("NoSolo.Core.Entities.UserEntity.UserEntity", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("NoSolo.Core.Entities.Auth.RefreshToken", b =>
                {
                    b.HasOne("NoSolo.Core.Entities.UserEntity.UserEntity", "UserEntity")
                        .WithMany("RefreshTokens")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("UserEntity");
                });

            modelBuilder.Entity("NoSolo.Core.Entities.Base.ContactEntity<NoSolo.Core.Entities.OrganizationEntity.OrganizationEntity>", b =>
                {
                    b.HasOne("NoSolo.Core.Entities.OrganizationEntity.OrganizationEntity", null)
                        .WithMany("Contacts")
                        .HasForeignKey("TEntityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("NoSolo.Core.Entities.OrganizationEntity.OrganizationEntity", "TEntity")
                        .WithMany()
                        .HasForeignKey("TEntityId1");

                    b.Navigation("TEntity");
                });

            modelBuilder.Entity("NoSolo.Core.Entities.Base.ContactEntity<NoSolo.Core.Entities.UserEntity.UserEntity>", b =>
                {
                    b.HasOne("NoSolo.Core.Entities.UserEntity.UserEntity", null)
                        .WithMany("Contacts")
                        .HasForeignKey("TEntityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("NoSolo.Core.Entities.UserEntity.UserEntity", "TEntity")
                        .WithMany()
                        .HasForeignKey("TEntityId1");

                    b.Navigation("TEntity");
                });

            modelBuilder.Entity("NoSolo.Core.Entities.Base.RequestEntity<NoSolo.Core.Entities.OrganizationEntity.OrganizationEntity, NoSolo.Core.Entities.UserEntity.UserOfferEntity>", b =>
                {
                    b.HasOne("NoSolo.Core.Entities.OrganizationEntity.OrganizationEntity", "TEntity")
                        .WithMany("RequestsFromOrganizationToUserOffer")
                        .HasForeignKey("TEntityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("NoSolo.Core.Entities.UserEntity.UserOfferEntity", "UEntity")
                        .WithMany()
                        .HasForeignKey("UEntityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("TEntity");

                    b.Navigation("UEntity");
                });

            modelBuilder.Entity("NoSolo.Core.Entities.Base.RequestEntity<NoSolo.Core.Entities.UserEntity.UserEntity, NoSolo.Core.Entities.OrganizationEntity.OrganizationOfferEntity>", b =>
                {
                    b.HasOne("NoSolo.Core.Entities.UserEntity.UserEntity", "TEntity")
                        .WithMany("RequestsFromUserProfileToOgranizationOffer")
                        .HasForeignKey("TEntityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("NoSolo.Core.Entities.OrganizationEntity.OrganizationOfferEntity", "UEntity")
                        .WithMany()
                        .HasForeignKey("UEntityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("TEntity");

                    b.Navigation("UEntity");
                });

            modelBuilder.Entity("NoSolo.Core.Entities.OrganizationEntity.MemberEntity", b =>
                {
                    b.HasOne("NoSolo.Core.Entities.OrganizationEntity.OrganizationEntity", "OrganizationEntity")
                        .WithMany("OrganizationUsers")
                        .HasForeignKey("OrganizationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("NoSolo.Core.Entities.UserEntity.UserEntity", "UserEntity")
                        .WithMany("OrganizationUsers")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("OrganizationEntity");

                    b.Navigation("UserEntity");
                });

            modelBuilder.Entity("NoSolo.Core.Entities.OrganizationEntity.OrganizationOfferEntity", b =>
                {
                    b.HasOne("NoSolo.Core.Entities.OrganizationEntity.OrganizationEntity", null)
                        .WithMany("Offers")
                        .HasForeignKey("OrganizationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("NoSolo.Core.Entities.OrganizationEntity.OrganizationEntity", "OrganizationEntity")
                        .WithMany()
                        .HasForeignKey("OrganizationId1");

                    b.Navigation("OrganizationEntity");
                });

            modelBuilder.Entity("NoSolo.Core.Entities.OrganizationEntity.OrganizationPhotoEntity", b =>
                {
                    b.HasOne("NoSolo.Core.Entities.OrganizationEntity.OrganizationEntity", null)
                        .WithMany("Photos")
                        .HasForeignKey("OrganizationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("NoSolo.Core.Entities.OrganizationEntity.OrganizationEntity", "OrganizationEntity")
                        .WithMany()
                        .HasForeignKey("OrganizationId1");

                    b.Navigation("OrganizationEntity");
                });

            modelBuilder.Entity("NoSolo.Core.Entities.UserEntity.UserOfferEntity", b =>
                {
                    b.HasOne("NoSolo.Core.Entities.UserEntity.UserEntity", null)
                        .WithMany("Offers")
                        .HasForeignKey("UserGuid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("NoSolo.Core.Entities.UserEntity.UserEntity", "UserEntity")
                        .WithMany()
                        .HasForeignKey("UserId");

                    b.Navigation("UserEntity");
                });

            modelBuilder.Entity("NoSolo.Core.Entities.UserEntity.UserPhotoEntity", b =>
                {
                    b.HasOne("NoSolo.Core.Entities.UserEntity.UserEntity", "UserEntity")
                        .WithOne("PhotoEntity")
                        .HasForeignKey("NoSolo.Core.Entities.UserEntity.UserPhotoEntity", "UserGuid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("UserEntity");
                });

            modelBuilder.Entity("NoSolo.Core.Entities.UserEntity.UserTagEntity", b =>
                {
                    b.HasOne("NoSolo.Core.Entities.UserEntity.UserEntity", null)
                        .WithMany("Tags")
                        .HasForeignKey("UserGuid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("NoSolo.Core.Entities.UserEntity.UserEntity", "UserEntity")
                        .WithMany()
                        .HasForeignKey("UserId");

                    b.Navigation("UserEntity");
                });

            modelBuilder.Entity("NoSolo.Core.Entities.OrganizationEntity.OrganizationEntity", b =>
                {
                    b.Navigation("Contacts");

                    b.Navigation("Offers");

                    b.Navigation("OrganizationUsers");

                    b.Navigation("Photos");

                    b.Navigation("RequestsFromOrganizationToUserOffer");
                });

            modelBuilder.Entity("NoSolo.Core.Entities.UserEntity.UserEntity", b =>
                {
                    b.Navigation("Contacts");

                    b.Navigation("Offers");

                    b.Navigation("OrganizationUsers");

                    b.Navigation("PhotoEntity");

                    b.Navigation("RefreshTokens");

                    b.Navigation("RequestsFromUserProfileToOgranizationOffer");

                    b.Navigation("Tags");
                });
#pragma warning restore 612, 618
        }
    }
}
