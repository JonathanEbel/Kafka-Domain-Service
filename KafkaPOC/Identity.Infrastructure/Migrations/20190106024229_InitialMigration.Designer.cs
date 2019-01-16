﻿// <auto-generated />
using System;
using Identity.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Identity.Infrastructure.Migrations
{
    [DbContext(typeof(IdentityContext))]
    [Migration("20190106024229_InitialMigration")]
    partial class InitialMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "2.2.0-rtm-35687")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("Identity.Models.ApplicationUser", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("Active");

                    b.Property<DateTime>("DateCreated");

                    b.Property<int?>("FailedLoginAttempts");

                    b.Property<DateTime>("LastLogin");

                    b.Property<DateTime>("LockedOutUntil");

                    b.Property<string>("Password");

                    b.Property<string>("Salt");

                    b.Property<string>("UserName");

                    b.HasKey("Id");

                    b.HasIndex("Id")
                        .IsUnique();

                    b.HasIndex("UserName")
                        .IsUnique();

                    b.ToTable("ApplicationUsers");
                });

            modelBuilder.Entity("Identity.Models.ApplicationUserClaim", b =>
                {
                    b.Property<Guid>("id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid?>("ApplicationUserId");

                    b.Property<string>("claimKey");

                    b.Property<string>("claimValue");

                    b.HasKey("id");

                    b.HasIndex("ApplicationUserId");

                    b.ToTable("ApplicationUserClaims");
                });

            modelBuilder.Entity("Identity.Models.ApplicationUserClaim", b =>
                {
                    b.HasOne("Identity.Models.ApplicationUser")
                        .WithMany("Claims")
                        .HasForeignKey("ApplicationUserId");
                });
#pragma warning restore 612, 618
        }
    }
}