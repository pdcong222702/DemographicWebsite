﻿// <auto-generated />
using System;
using Demographic_Website.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Demographic_Website.Migrations
{
    [DbContext(typeof(DemoGraphicContext))]
    [Migration("20240505062355_V9")]
    partial class V9
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Demographic_Website.ModelViews.BookLet", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("PopulationId")
                        .HasColumnType("int");

                    b.Property<int>("ResidenceBookletId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PopulationId");

                    b.HasIndex("ResidenceBookletId");

                    b.ToTable("BookLet");
                });

            modelBuilder.Entity("Demographic_Website.Models.Population", b =>
                {
                    b.Property<int>("PopulationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PopulationId"));

                    b.Property<string>("BirthPlace")
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<string>("CitizenIdentificationCard")
                        .HasMaxLength(12)
                        .HasColumnType("nvarchar(12)");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DateOfBirth")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DateOfIssue")
                        .HasColumnType("datetime2");

                    b.Property<string>("Ethnicity")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<bool?>("Gender")
                        .HasColumnType("bit");

                    b.Property<string>("Image")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool?>("IsChoose")
                        .HasColumnType("bit");

                    b.Property<string>("Occupation")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("PlaceOfIssue")
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<string>("PopulationName")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("PopulationNickName")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Relationship")
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Religion")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int?>("ResidenceBookletId")
                        .HasColumnType("int");

                    b.Property<string>("WorkPlace")
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<bool?>("alive")
                        .HasColumnType("bit");

                    b.HasKey("PopulationId")
                        .HasName("PK__Populati__3A2E15E20414EC74");

                    b.HasIndex("ResidenceBookletId");

                    b.ToTable("Populations");
                });

            modelBuilder.Entity("Demographic_Website.Models.ResidenceBooklet", b =>
                {
                    b.Property<int>("ResidenceBookletId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ResidenceBookletId"));

                    b.Property<string>("Address")
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<string>("BookletArea")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTime?>("CreateDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("MoveDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("MoveReason")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ResidenceBookletCode")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("ResidenceBookletId")
                        .HasName("PK__Residenc__AE46D5A2295AD5E1");

                    b.ToTable("ResidenceBooklet", (string)null);
                });

            modelBuilder.Entity("Demographic_Website.Models.TemporarilyAbsent", b =>
                {
                    b.Property<int>("TemporarilyAbsentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TemporarilyAbsentId"));

                    b.Property<string>("BirthPlace")
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<string>("CitizenIdentificationCard")
                        .HasMaxLength(12)
                        .HasColumnType("nvarchar(12)");

                    b.Property<DateTime?>("DateOfBirth")
                        .HasColumnType("datetime2");

                    b.Property<string>("DocumentCodeAbsent")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateOnly?>("FromDate")
                        .HasColumnType("date");

                    b.Property<bool?>("Gender")
                        .HasColumnType("bit");

                    b.Property<bool?>("IsNewAbsent")
                        .HasMaxLength(100)
                        .HasColumnType("bit");

                    b.Property<string>("Nationality")
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<string>("Phone")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("PopulationId")
                        .HasColumnType("int");

                    b.Property<string>("PopulationName")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Reason")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TemporaryAddress")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("TemporarilyAbsentId")
                        .HasName("PK__Temporar__FB890209EFD0C9ED");

                    b.HasIndex("PopulationId");

                    b.ToTable("TemporarilyAbsent", (string)null);
                });

            modelBuilder.Entity("Demographic_Website.Models.TemporarilyStaying", b =>
                {
                    b.Property<int>("TemporarilyStayingId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TemporarilyStayingId"));

                    b.Property<string>("BirthPlace")
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<string>("CitizenIdentificationCard")
                        .HasMaxLength(12)
                        .HasColumnType("nvarchar(12)");

                    b.Property<DateTime?>("DateOfBirth")
                        .HasColumnType("datetime2");

                    b.Property<string>("DocumentCodeStaying")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateOnly?>("FromDate")
                        .HasColumnType("date");

                    b.Property<bool?>("Gender")
                        .HasColumnType("bit");

                    b.Property<bool?>("IsNewStay")
                        .HasMaxLength(100)
                        .HasColumnType("bit");

                    b.Property<string>("Nationality")
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<string>("Phone")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("PopulationId")
                        .HasColumnType("int");

                    b.Property<string>("PopulationName")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Reason")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TemporaryAddress")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("TemporarilyStayingId")
                        .HasName("PK__Temporar__FB89020962FB23B0");

                    b.HasIndex("PopulationId");

                    b.ToTable("TemporarilyStaying", (string)null);
                });

            modelBuilder.Entity("Demographic_Website.ModelViews.BookLet", b =>
                {
                    b.HasOne("Demographic_Website.Models.Population", "Population")
                        .WithMany()
                        .HasForeignKey("PopulationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Demographic_Website.Models.ResidenceBooklet", "ResidenceBooklet")
                        .WithMany()
                        .HasForeignKey("ResidenceBookletId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Population");

                    b.Navigation("ResidenceBooklet");
                });

            modelBuilder.Entity("Demographic_Website.Models.Population", b =>
                {
                    b.HasOne("Demographic_Website.Models.ResidenceBooklet", "ResidenceBooklet")
                        .WithMany("Populations")
                        .HasForeignKey("ResidenceBookletId")
                        .HasConstraintName("FK_ResidenceBooklet");

                    b.Navigation("ResidenceBooklet");
                });

            modelBuilder.Entity("Demographic_Website.Models.TemporarilyAbsent", b =>
                {
                    b.HasOne("Demographic_Website.Models.Population", "Population")
                        .WithMany("TemporarilyAbsents")
                        .HasForeignKey("PopulationId")
                        .HasConstraintName("FK_PopulationAbsent");

                    b.Navigation("Population");
                });

            modelBuilder.Entity("Demographic_Website.Models.TemporarilyStaying", b =>
                {
                    b.HasOne("Demographic_Website.Models.Population", "Population")
                        .WithMany("TemporarilyStayings")
                        .HasForeignKey("PopulationId")
                        .HasConstraintName("FK_PopulationStaying");

                    b.Navigation("Population");
                });

            modelBuilder.Entity("Demographic_Website.Models.Population", b =>
                {
                    b.Navigation("TemporarilyAbsents");

                    b.Navigation("TemporarilyStayings");
                });

            modelBuilder.Entity("Demographic_Website.Models.ResidenceBooklet", b =>
                {
                    b.Navigation("Populations");
                });
#pragma warning restore 612, 618
        }
    }
}
