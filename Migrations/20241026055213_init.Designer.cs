﻿// <auto-generated />
using System;
using MadhurAPI.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace MadhurAPI.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20241026055213_init")]
    partial class init
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0-rc.1.24451.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("MadhurAPI.Models.DistrictMaster", b =>
                {
                    b.Property<int>("recid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("recid"));

                    b.Property<int>("dist_code")
                        .HasColumnType("int");

                    b.Property<string>("distt_name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("state_code")
                        .HasColumnType("int");

                    b.HasKey("recid");

                    b.ToTable("DistrictMaster");
                });

            modelBuilder.Entity("MadhurAPI.Models.Member", b =>
                {
                    b.Property<long>("AutoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnOrder(0);

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("AutoId"));

                    b.Property<string>("AadharNo")
                        .HasColumnType("varchar(14)");

                    b.Property<string>("Address")
                        .HasColumnType("varchar(400)");

                    b.Property<string>("City")
                        .HasColumnType("varchar(50)");

                    b.Property<DateTime?>("CreationDate")
                        .HasColumnType("datetime");

                    b.Property<string>("IsActive")
                        .HasColumnType("char(1)");

                    b.Property<string>("MemberId")
                        .HasColumnType("varchar(10)")
                        .HasColumnOrder(3);

                    b.Property<string>("MemberName")
                        .HasColumnType("varchar(50)");

                    b.Property<string>("MobileNo")
                        .HasColumnType("varchar(10)");

                    b.Property<string>("Nominee")
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("PinCode")
                        .HasColumnType("varchar(6)");

                    b.Property<string>("RefId")
                        .HasColumnType("varchar(10)")
                        .HasColumnOrder(2);

                    b.Property<string>("RegPin")
                        .HasColumnType("varchar(10)")
                        .HasColumnOrder(1);

                    b.Property<string>("RelationWithNominee")
                        .HasColumnType("varchar(20)");

                    b.Property<string>("State")
                        .HasColumnType("varchar(50)");

                    b.Property<DateTime?>("dob")
                        .HasColumnType("date");

                    b.HasKey("AutoId");

                    b.ToTable("Members");
                });

            modelBuilder.Entity("MadhurAPI.Models.RegKey", b =>
                {
                    b.Property<long>("AuotId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("AuotId"));

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("IsCopy")
                        .IsRequired()
                        .HasColumnType("nvarchar(1)");

                    b.Property<string>("Key")
                        .IsRequired()
                        .HasColumnType("nvarchar(10)");

                    b.HasKey("AuotId");

                    b.ToTable("RegKeys");
                });

            modelBuilder.Entity("MadhurAPI.Models.StateMaster", b =>
                {
                    b.Property<int>("RecId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RecId"));

                    b.Property<int>("country_id")
                        .HasColumnType("int");

                    b.Property<string>("state_code")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("state_name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("RecId");

                    b.ToTable("StateMaster");
                });
#pragma warning restore 612, 618
        }
    }
}
