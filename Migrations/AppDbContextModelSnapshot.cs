﻿// <auto-generated />
using System;
using MadhurAPI.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace MadhurAPI.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0-rc.1.24451.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("MadhurAPI.Models.BannerMaster", b =>
                {
                    b.Property<long>("AutoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("AutoId"));

                    b.Property<string>("Banner")
                        .IsRequired()
                        .HasColumnType("varchar(200)");

                    b.HasKey("AutoId");

                    b.ToTable("BannerMaster");

                    b.HasData(
                        new
                        {
                            AutoId = 1L,
                            Banner = "slider1.png"
                        },
                        new
                        {
                            AutoId = 2L,
                            Banner = "slider2.png"
                        });
                });

            modelBuilder.Entity("MadhurAPI.Models.DTO.AllMemberDTO", b =>
                {
                    b.Property<string>("City")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MemberId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MemberName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MobileNo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RefId")
                        .HasColumnType("nvarchar(max)");

                    b.ToTable("AllMember");
                });

            modelBuilder.Entity("MadhurAPI.Models.DTO.AllSelfMemberDTO", b =>
                {
                    b.Property<string>("City")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MemberId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MemberName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MobileNo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("mTotal")
                        .HasColumnType("int");

                    b.Property<int?>("pTotal")
                        .HasColumnType("int");

                    b.ToTable("AllSelfMember");
                });

            modelBuilder.Entity("MadhurAPI.Models.DTO.LevelCount", b =>
                {
                    b.Property<int>("level")
                        .HasColumnType("int");

                    b.Property<int>("member")
                        .HasColumnType("int");

                    b.Property<int>("purchase")
                        .HasColumnType("int");

                    b.Property<int>("total")
                        .HasColumnType("int");

                    b.ToTable("LevelCount");
                });

            modelBuilder.Entity("MadhurAPI.Models.DTO.LevelReportDTO", b =>
                {
                    b.Property<int>("level")
                        .HasColumnType("int");

                    b.Property<int>("totalCount")
                        .HasColumnType("int");

                    b.ToTable("LevelReport");
                });

            modelBuilder.Entity("MadhurAPI.Models.DTO.LevelWiseMemberDTO", b =>
                {
                    b.Property<string>("City")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MemberId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MemberName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MobileNo")
                        .HasColumnType("nvarchar(max)");

                    b.ToTable("LevelWiseMember");
                });

            modelBuilder.Entity("MadhurAPI.Models.DTO.RewardMasterDTO", b =>
                {
                    b.Property<int>("AutoId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("MemberId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Remark")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("level")
                        .HasColumnType("nvarchar(max)");

                    b.ToTable("RewardMasterDTO");
                });

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

            modelBuilder.Entity("MadhurAPI.Models.KycDocument", b =>
                {
                    b.Property<long>("AutoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("AutoId"));

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime");

                    b.Property<string>("MemberId")
                        .IsRequired()
                        .HasColumnType("varchar(30)");

                    b.Property<string>("file")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<string>("type")
                        .IsRequired()
                        .HasColumnType("varchar(30)");

                    b.HasKey("AutoId");

                    b.ToTable("KycDocument");
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

                    b.Property<string>("IsSubscribe")
                        .HasColumnType("char(1)");

                    b.Property<string>("MemberId")
                        .HasColumnType("varchar(20)")
                        .HasColumnOrder(3);

                    b.Property<string>("MemberName")
                        .HasColumnType("varchar(50)");

                    b.Property<string>("MemberType")
                        .HasColumnType("varchar(20)");

                    b.Property<string>("MobileNo")
                        .HasColumnType("varchar(10)");

                    b.Property<string>("Nominee")
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("PinCode")
                        .HasColumnType("varchar(6)");

                    b.Property<string>("RefId")
                        .HasColumnType("varchar(20)")
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

            modelBuilder.Entity("MadhurAPI.Models.Plan", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("file")
                        .IsRequired()
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("type")
                        .IsRequired()
                        .HasColumnType("varchar(20)");

                    b.HasKey("Id");

                    b.ToTable("Plan");
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

            modelBuilder.Entity("MadhurAPI.Models.RewardDistributor", b =>
                {
                    b.Property<long>("AutoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("AutoId"));

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime");

                    b.Property<string>("Level")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MemberId")
                        .IsRequired()
                        .HasColumnType("varchar(30)");

                    b.Property<string>("StoreId")
                        .IsRequired()
                        .HasColumnType("varchar(30)");

                    b.HasKey("AutoId");

                    b.ToTable("RewardDistributor");
                });

            modelBuilder.Entity("MadhurAPI.Models.RewardMaster", b =>
                {
                    b.Property<long>("AutoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("AutoId"));

                    b.Property<DateTime?>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("IsApproved")
                        .IsRequired()
                        .HasColumnType("nvarchar(1)");

                    b.Property<string>("MemberId")
                        .HasColumnType("varchar(20)");

                    b.Property<string>("Remark")
                        .HasColumnType("nvarchar(500)");

                    b.Property<string>("file_path")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("level")
                        .HasColumnType("varchar(5)");

                    b.HasKey("AutoId");

                    b.ToTable("RewardMaster");
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

            modelBuilder.Entity("MadhurAPI.Models.StoreMaster", b =>
                {
                    b.Property<long>("AutoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("AutoId"));

                    b.Property<string>("Address")
                        .HasColumnType("varchar(200)");

                    b.Property<string>("City")
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Mobile")
                        .HasColumnType("varchar(10)");

                    b.Property<string>("PinCode")
                        .HasColumnType("varchar(6)");

                    b.Property<string>("State")
                        .HasColumnType("varchar(50)");

                    b.Property<string>("StoreId")
                        .IsRequired()
                        .HasColumnType("varchar(20)");

                    b.Property<string>("StoreName")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<string>("type")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("AutoId");

                    b.ToTable("StoreMaster");
                });

            modelBuilder.Entity("MadhurAPI.Models.TermsCondition", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("content")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("TermsCondition");
                });
#pragma warning restore 612, 618
        }
    }
}
