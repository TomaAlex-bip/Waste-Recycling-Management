// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WasteRecyclingManagementApi.Data;

#nullable disable

namespace WasteRecyclingManagementApi.Data.Migrations
{
    [DbContext(typeof(RecyclingDbContext))]
    [Migration("20220609114721_addedcontainers")]
    partial class addedcontainers
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("WasteRecyclingManagementApi.Core.Entities.Container", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("MeasureUnit")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Occupied")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("RecyclingPointId")
                        .HasColumnType("int");

                    b.Property<decimal>("TotalCapacity")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("RecyclingPointId");

                    b.ToTable("Containers");
                });

            modelBuilder.Entity("WasteRecyclingManagementApi.Core.Entities.Operation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("ContainerId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<decimal>("WasteAmount")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("ContainerId");

                    b.HasIndex("UserId");

                    b.ToTable("Operation");
                });

            modelBuilder.Entity("WasteRecyclingManagementApi.Core.Entities.RecyclingPoint", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<double>("Latitude")
                        .HasColumnType("float");

                    b.Property<double>("Longitude")
                        .HasColumnType("float");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("RecyclingPoints");
                });

            modelBuilder.Entity("WasteRecyclingManagementApi.Core.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Role")
                        .HasColumnType("int");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("WasteRecyclingManagementApi.Core.Entities.Container", b =>
                {
                    b.HasOne("WasteRecyclingManagementApi.Core.Entities.RecyclingPoint", "RecyclingPoint")
                        .WithMany("Containers")
                        .HasForeignKey("RecyclingPointId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("RecyclingPoint");
                });

            modelBuilder.Entity("WasteRecyclingManagementApi.Core.Entities.Operation", b =>
                {
                    b.HasOne("WasteRecyclingManagementApi.Core.Entities.Container", "Container")
                        .WithMany("Operations")
                        .HasForeignKey("ContainerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WasteRecyclingManagementApi.Core.Entities.User", "User")
                        .WithMany("Operations")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Container");

                    b.Navigation("User");
                });

            modelBuilder.Entity("WasteRecyclingManagementApi.Core.Entities.Container", b =>
                {
                    b.Navigation("Operations");
                });

            modelBuilder.Entity("WasteRecyclingManagementApi.Core.Entities.RecyclingPoint", b =>
                {
                    b.Navigation("Containers");
                });

            modelBuilder.Entity("WasteRecyclingManagementApi.Core.Entities.User", b =>
                {
                    b.Navigation("Operations");
                });
#pragma warning restore 612, 618
        }
    }
}
