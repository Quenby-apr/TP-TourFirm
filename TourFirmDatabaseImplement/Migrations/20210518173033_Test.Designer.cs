// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TourFirmDatabaseImplement;

namespace TourFirmDatabaseImplement.Migrations
{
    [DbContext(typeof(TourFirmDatabase))]
    [Migration("20210518173033_Test")]
    partial class Test
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("TourFirmDatabaseImplement.Models.Excursion", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Duration")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PlaceID")
                        .HasColumnType("int");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("TouristID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("PlaceID");

                    b.HasIndex("TouristID");

                    b.ToTable("Excursions");
                });

            modelBuilder.Entity("TourFirmDatabaseImplement.Models.Guide", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AdditionalLanguage")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MainLanguage")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("OperatorID")
                        .HasColumnType("int");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("WorkPlace")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.HasIndex("OperatorID");

                    b.ToTable("Guides");
                });

            modelBuilder.Entity("TourFirmDatabaseImplement.Models.GuideExcursion", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ExcursionID")
                        .HasColumnType("int");

                    b.Property<int>("GuideID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("ExcursionID");

                    b.HasIndex("GuideID");

                    b.ToTable("GuideExcursions");
                });

            modelBuilder.Entity("TourFirmDatabaseImplement.Models.Halt", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("OperatorID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("OperatorID");

                    b.ToTable("Halts");
                });

            modelBuilder.Entity("TourFirmDatabaseImplement.Models.Operator", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Mail")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("Operators");
                });

            modelBuilder.Entity("TourFirmDatabaseImplement.Models.Place", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TouristID")
                        .HasColumnType("int");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.HasIndex("TouristID");

                    b.ToTable("Places");
                });

            modelBuilder.Entity("TourFirmDatabaseImplement.Models.Tour", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("HaltID")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("OperatorID")
                        .HasColumnType("int");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("ID");

                    b.HasIndex("HaltID");

                    b.HasIndex("OperatorID");

                    b.ToTable("Tours");
                });

            modelBuilder.Entity("TourFirmDatabaseImplement.Models.TourGuide", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("GuideID")
                        .HasColumnType("int");

                    b.Property<int>("TourID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("GuideID");

                    b.HasIndex("TourID");

                    b.ToTable("TourGuides");
                });

            modelBuilder.Entity("TourFirmDatabaseImplement.Models.Tourist", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Mail")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("Tourists");
                });

            modelBuilder.Entity("TourFirmDatabaseImplement.Models.Travel", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DateEnd")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateStart")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TouristID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("TouristID");

                    b.ToTable("Travels");
                });

            modelBuilder.Entity("TourFirmDatabaseImplement.Models.TravelExcursion", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ExcursionID")
                        .HasColumnType("int");

                    b.Property<int>("TravelID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("ExcursionID");

                    b.HasIndex("TravelID");

                    b.ToTable("TravelExcursions");
                });

            modelBuilder.Entity("TourFirmDatabaseImplement.Models.TravelTour", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("TourID")
                        .HasColumnType("int");

                    b.Property<int>("TravelID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("TourID");

                    b.HasIndex("TravelID");

                    b.ToTable("TravelTours");
                });

            modelBuilder.Entity("TourFirmDatabaseImplement.Models.Excursion", b =>
                {
                    b.HasOne("TourFirmDatabaseImplement.Models.Place", "Place")
                        .WithMany("Excursions")
                        .HasForeignKey("PlaceID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TourFirmDatabaseImplement.Models.Tourist", "Tourist")
                        .WithMany("Excursions")
                        .HasForeignKey("TouristID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TourFirmDatabaseImplement.Models.Guide", b =>
                {
                    b.HasOne("TourFirmDatabaseImplement.Models.Operator", "Operator")
                        .WithMany("Guides")
                        .HasForeignKey("OperatorID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TourFirmDatabaseImplement.Models.GuideExcursion", b =>
                {
                    b.HasOne("TourFirmDatabaseImplement.Models.Excursion", "Excursion")
                        .WithMany("GuideExcursions")
                        .HasForeignKey("ExcursionID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TourFirmDatabaseImplement.Models.Guide", "Guide")
                        .WithMany("GuideExcursions")
                        .HasForeignKey("GuideID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TourFirmDatabaseImplement.Models.Halt", b =>
                {
                    b.HasOne("TourFirmDatabaseImplement.Models.Operator", "Operator")
                        .WithMany("Halts")
                        .HasForeignKey("OperatorID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TourFirmDatabaseImplement.Models.Place", b =>
                {
                    b.HasOne("TourFirmDatabaseImplement.Models.Tourist", "Tourist")
                        .WithMany("Places")
                        .HasForeignKey("TouristID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TourFirmDatabaseImplement.Models.Tour", b =>
                {
                    b.HasOne("TourFirmDatabaseImplement.Models.Halt", "Halt")
                        .WithMany("Tours")
                        .HasForeignKey("HaltID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TourFirmDatabaseImplement.Models.Operator", "Operator")
                        .WithMany("Tours")
                        .HasForeignKey("OperatorID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TourFirmDatabaseImplement.Models.TourGuide", b =>
                {
                    b.HasOne("TourFirmDatabaseImplement.Models.Guide", "Guide")
                        .WithMany("TourGuides")
                        .HasForeignKey("GuideID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TourFirmDatabaseImplement.Models.Tour", "Tour")
                        .WithMany("TourGuides")
                        .HasForeignKey("TourID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TourFirmDatabaseImplement.Models.Travel", b =>
                {
                    b.HasOne("TourFirmDatabaseImplement.Models.Tourist", "Tourist")
                        .WithMany("Travels")
                        .HasForeignKey("TouristID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TourFirmDatabaseImplement.Models.TravelExcursion", b =>
                {
                    b.HasOne("TourFirmDatabaseImplement.Models.Excursion", "Excursion")
                        .WithMany("TravelExcursions")
                        .HasForeignKey("ExcursionID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TourFirmDatabaseImplement.Models.Travel", "Travel")
                        .WithMany("TravelExcursions")
                        .HasForeignKey("TravelID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TourFirmDatabaseImplement.Models.TravelTour", b =>
                {
                    b.HasOne("TourFirmDatabaseImplement.Models.Tour", "Tour")
                        .WithMany("TravelTours")
                        .HasForeignKey("TourID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TourFirmDatabaseImplement.Models.Travel", "Travel")
                        .WithMany("TravelTours")
                        .HasForeignKey("TravelID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
