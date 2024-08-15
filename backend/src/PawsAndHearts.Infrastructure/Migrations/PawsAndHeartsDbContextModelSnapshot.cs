﻿// <auto-generated />
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using PawsAndHearts.Infrastructure;

#nullable disable

namespace PawsAndHearts.Infrastructure.Migrations
{
    [DbContext(typeof(PawsAndHeartsDbContext))]
    partial class PawsAndHeartsDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("PawsAndHearts.Domain.Models.Pet", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("Breed")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("breed");

                    b.Property<string>("Color")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("color");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("character varying(500)")
                        .HasColumnName("description");

                    b.Property<string>("HealthInfo")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("character varying(250)")
                        .HasColumnName("health_info");

                    b.Property<double>("Height")
                        .HasColumnType("double precision")
                        .HasColumnName("height");

                    b.Property<string>("HelpStatus")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("help_status");

                    b.Property<bool>("IsNeutered")
                        .HasColumnType("boolean")
                        .HasColumnName("is_neutered");

                    b.Property<bool>("IsVaccinated")
                        .HasColumnType("boolean")
                        .HasColumnName("is_vaccinated");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("name");

                    b.Property<string>("Species")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("species");

                    b.Property<double>("Weight")
                        .HasColumnType("double precision")
                        .HasColumnName("weight");

                    b.Property<Guid?>("volunteer_id")
                        .HasColumnType("uuid")
                        .HasColumnName("volunteer_id");

                    b.ComplexProperty<Dictionary<string, object>>("Address", "PawsAndHearts.Domain.Models.Pet.Address#Address", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("City")
                                .IsRequired()
                                .HasMaxLength(200)
                                .HasColumnType("character varying(200)")
                                .HasColumnName("address_city");

                            b1.Property<string>("Flat")
                                .HasMaxLength(200)
                                .HasColumnType("character varying(200)")
                                .HasColumnName("address_flat");

                            b1.Property<string>("House")
                                .IsRequired()
                                .HasMaxLength(200)
                                .HasColumnType("character varying(200)")
                                .HasColumnName("address_house");

                            b1.Property<string>("Street")
                                .IsRequired()
                                .HasMaxLength(200)
                                .HasColumnType("character varying(200)")
                                .HasColumnName("address_street");
                        });

                    b.ComplexProperty<Dictionary<string, object>>("BirthDate", "PawsAndHearts.Domain.Models.Pet.BirthDate#BirthDate", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<DateOnly>("Value")
                                .HasColumnType("date")
                                .HasColumnName("birth_date_value");
                        });

                    b.ComplexProperty<Dictionary<string, object>>("CreationDate", "PawsAndHearts.Domain.Models.Pet.CreationDate#CreationDate", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<DateOnly>("Value")
                                .HasColumnType("date")
                                .HasColumnName("creation_date_value");
                        });

                    b.ComplexProperty<Dictionary<string, object>>("PhoneNumber", "PawsAndHearts.Domain.Models.Pet.PhoneNumber#PhoneNumber", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasMaxLength(14)
                                .HasColumnType("character varying(14)")
                                .HasColumnName("phone_number_value");
                        });

                    b.HasKey("Id")
                        .HasName("pk_pets");

                    b.HasIndex("volunteer_id")
                        .HasDatabaseName("ix_pets_volunteer_id");

                    b.ToTable("pets", (string)null);
                });

            modelBuilder.Entity("PawsAndHearts.Domain.Models.Volunteer", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<int>("Experience")
                        .HasMaxLength(90)
                        .HasColumnType("integer")
                        .HasColumnName("experience");

                    b.Property<int>("PetsBeingTreated")
                        .HasColumnType("integer")
                        .HasColumnName("pets_being_treated");

                    b.Property<int>("PetsFoundHome")
                        .HasColumnType("integer")
                        .HasColumnName("pets_found_home");

                    b.Property<int>("PetsLookingForHome")
                        .HasColumnType("integer")
                        .HasColumnName("pets_looking_for_home");

                    b.ComplexProperty<Dictionary<string, object>>("FullName", "PawsAndHearts.Domain.Models.Volunteer.FullName#FullName", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("Name")
                                .IsRequired()
                                .HasMaxLength(50)
                                .HasColumnType("character varying(50)")
                                .HasColumnName("full_name_name");

                            b1.Property<string>("Patronymic")
                                .HasMaxLength(50)
                                .HasColumnType("character varying(50)")
                                .HasColumnName("full_name_patronymic");

                            b1.Property<string>("Surname")
                                .IsRequired()
                                .HasMaxLength(50)
                                .HasColumnType("character varying(50)")
                                .HasColumnName("full_name_surname");
                        });

                    b.ComplexProperty<Dictionary<string, object>>("PhoneNumber", "PawsAndHearts.Domain.Models.Volunteer.PhoneNumber#PhoneNumber", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasMaxLength(14)
                                .HasColumnType("character varying(14)")
                                .HasColumnName("phone_number_value");
                        });

                    b.HasKey("Id")
                        .HasName("pk_volunteers");

                    b.ToTable("volunteers", (string)null);
                });

            modelBuilder.Entity("PawsAndHearts.Domain.Models.Pet", b =>
                {
                    b.HasOne("PawsAndHearts.Domain.Models.Volunteer", null)
                        .WithMany("Pets")
                        .HasForeignKey("volunteer_id")
                        .HasConstraintName("fk_pets_volunteers_volunteer_id");

                    b.OwnsOne("PawsAndHearts.Domain.ValueObjects.PetDetails", "PetDetails", b1 =>
                        {
                            b1.Property<Guid>("PetId")
                                .HasColumnType("uuid")
                                .HasColumnName("id");

                            b1.HasKey("PetId");

                            b1.ToTable("pets");

                            b1.ToJson("PetDetails");

                            b1.WithOwner()
                                .HasForeignKey("PetId")
                                .HasConstraintName("fk_pets_pets_id");

                            b1.OwnsMany("PawsAndHearts.Domain.ValueObjects.Requisite", "Requisites", b2 =>
                                {
                                    b2.Property<Guid>("PetDetailsPetId")
                                        .HasColumnType("uuid");

                                    b2.Property<int>("Id")
                                        .ValueGeneratedOnAdd()
                                        .HasColumnType("integer");

                                    b2.Property<string>("Description")
                                        .IsRequired()
                                        .HasMaxLength(100)
                                        .HasColumnType("character varying(100)");

                                    b2.Property<string>("Name")
                                        .IsRequired()
                                        .HasMaxLength(50)
                                        .HasColumnType("character varying(50)");

                                    b2.HasKey("PetDetailsPetId", "Id")
                                        .HasName("pk_pets");

                                    b2.ToTable("pets");

                                    b2.WithOwner()
                                        .HasForeignKey("PetDetailsPetId")
                                        .HasConstraintName("fk_pets_pets_pet_details_pet_id");
                                });

                            b1.OwnsMany("PawsAndHearts.Domain.ValueObjects.PetPhoto", "Photos", b2 =>
                                {
                                    b2.Property<Guid>("PetDetailsPetId")
                                        .HasColumnType("uuid");

                                    b2.Property<int>("Id")
                                        .ValueGeneratedOnAdd()
                                        .HasColumnType("integer");

                                    b2.Property<bool>("IsMain")
                                        .HasColumnType("boolean");

                                    b2.Property<string>("Path")
                                        .IsRequired()
                                        .HasMaxLength(100)
                                        .HasColumnType("character varying(100)");

                                    b2.HasKey("PetDetailsPetId", "Id")
                                        .HasName("pk_pets");

                                    b2.ToTable("pets");

                                    b2.WithOwner()
                                        .HasForeignKey("PetDetailsPetId")
                                        .HasConstraintName("fk_pets_pets_pet_details_pet_id");
                                });

                            b1.Navigation("Photos");

                            b1.Navigation("Requisites");
                        });

                    b.Navigation("PetDetails")
                        .IsRequired();
                });

            modelBuilder.Entity("PawsAndHearts.Domain.Models.Volunteer", b =>
                {
                    b.OwnsOne("PawsAndHearts.Domain.ValueObjects.VolunteerDetails", "VolunteerDetails", b1 =>
                        {
                            b1.Property<Guid>("VolunteerId")
                                .HasColumnType("uuid")
                                .HasColumnName("id");

                            b1.HasKey("VolunteerId");

                            b1.ToTable("volunteers");

                            b1.ToJson("VolunteerDetails");

                            b1.WithOwner()
                                .HasForeignKey("VolunteerId")
                                .HasConstraintName("fk_volunteers_volunteers_id");

                            b1.OwnsMany("PawsAndHearts.Domain.ValueObjects.SocialNetwork", "SocialNetworks", b2 =>
                                {
                                    b2.Property<Guid>("VolunteerDetailsVolunteerId")
                                        .HasColumnType("uuid");

                                    b2.Property<int>("Id")
                                        .ValueGeneratedOnAdd()
                                        .HasColumnType("integer");

                                    b2.Property<string>("Link")
                                        .IsRequired()
                                        .HasMaxLength(100)
                                        .HasColumnType("character varying(100)");

                                    b2.Property<string>("Name")
                                        .IsRequired()
                                        .HasMaxLength(50)
                                        .HasColumnType("character varying(50)");

                                    b2.HasKey("VolunteerDetailsVolunteerId", "Id")
                                        .HasName("pk_volunteers");

                                    b2.ToTable("volunteers");

                                    b2.WithOwner()
                                        .HasForeignKey("VolunteerDetailsVolunteerId")
                                        .HasConstraintName("fk_volunteers_volunteers_volunteer_details_volunteer_id");
                                });

                            b1.OwnsMany("PawsAndHearts.Domain.ValueObjects.Requisite", "Requisites", b2 =>
                                {
                                    b2.Property<Guid>("VolunteerDetailsVolunteerId")
                                        .HasColumnType("uuid");

                                    b2.Property<int>("Id")
                                        .ValueGeneratedOnAdd()
                                        .HasColumnType("integer");

                                    b2.Property<string>("Description")
                                        .IsRequired()
                                        .HasMaxLength(100)
                                        .HasColumnType("character varying(100)");

                                    b2.Property<string>("Name")
                                        .IsRequired()
                                        .HasMaxLength(50)
                                        .HasColumnType("character varying(50)");

                                    b2.HasKey("VolunteerDetailsVolunteerId", "Id")
                                        .HasName("pk_volunteers");

                                    b2.ToTable("volunteers");

                                    b2.WithOwner()
                                        .HasForeignKey("VolunteerDetailsVolunteerId")
                                        .HasConstraintName("fk_volunteers_volunteers_volunteer_details_volunteer_id");
                                });

                            b1.Navigation("Requisites");

                            b1.Navigation("SocialNetworks");
                        });

                    b.Navigation("VolunteerDetails")
                        .IsRequired();
                });

            modelBuilder.Entity("PawsAndHearts.Domain.Models.Volunteer", b =>
                {
                    b.Navigation("Pets");
                });
#pragma warning restore 612, 618
        }
    }
}
