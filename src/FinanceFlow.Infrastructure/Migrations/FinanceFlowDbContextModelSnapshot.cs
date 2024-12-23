﻿// <auto-generated />
using System;
using FinanceFlow.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace FinanceFlow.Infrastructure.Migrations
{
    [DbContext(typeof(FinanceFlowDbContext))]
    partial class FinanceFlowDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            MySqlModelBuilderExtensions.AutoIncrementColumns(modelBuilder);

            modelBuilder.Entity("FinanceFlow.Domain.Entities.Account", b =>
                {
                    b.Property<long>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<long>("ID"));

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(65,30)");

                    b.Property<DateTime>("Create_at")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Description")
                        .HasColumnType("longtext");

                    b.Property<DateTime>("End_Date")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("Installment")
                        .HasColumnType("int");

                    b.Property<bool>("Recurrence")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTime>("Start_Date")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("TypeAccount")
                        .HasColumnType("int");

                    b.Property<DateTime>("Update_at")
                        .HasColumnType("datetime(6)");

                    b.Property<long>("UserID")
                        .HasColumnType("bigint");

                    b.HasKey("ID");

                    b.HasIndex("UserID");

                    b.ToTable("Accounts");
                });

            modelBuilder.Entity("FinanceFlow.Domain.Entities.Recurrence", b =>
                {
                    b.Property<long>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<long>("ID"));

                    b.Property<long>("AccountID")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("Create_at")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("End_Date")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("Start_Date")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("Update_at")
                        .HasColumnType("datetime(6)");

                    b.HasKey("ID");

                    b.HasIndex("AccountID");

                    b.ToTable("Recurrences");
                });

            modelBuilder.Entity("FinanceFlow.Domain.Entities.Tag", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<long>("Id"));

                    b.Property<long>("AccountId")
                        .HasColumnType("bigint");

                    b.Property<int>("Value")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AccountId");

                    b.ToTable("Tags", (string)null);
                });

            modelBuilder.Entity("FinanceFlow.Domain.Entities.Transaction", b =>
                {
                    b.Property<long>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<long>("ID"));

                    b.Property<long>("AccountID")
                        .HasColumnType("bigint");

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(65,30)");

                    b.Property<DateTime>("Create_at")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("TypeAccount")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("AccountID");

                    b.ToTable("Transactions");
                });

            modelBuilder.Entity("FinanceFlow.Domain.Entities.User", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("CodePassword")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<Guid>("UserIdentifier")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("FinanceFlow.Domain.Entities.Account", b =>
                {
                    b.HasOne("FinanceFlow.Domain.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("FinanceFlow.Domain.Entities.Recurrence", b =>
                {
                    b.HasOne("FinanceFlow.Domain.Entities.Account", "Account")
                        .WithMany()
                        .HasForeignKey("AccountID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Account");
                });

            modelBuilder.Entity("FinanceFlow.Domain.Entities.Tag", b =>
                {
                    b.HasOne("FinanceFlow.Domain.Entities.Account", "Account")
                        .WithMany("Tags")
                        .HasForeignKey("AccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Account");
                });

            modelBuilder.Entity("FinanceFlow.Domain.Entities.Transaction", b =>
                {
                    b.HasOne("FinanceFlow.Domain.Entities.Account", "Account")
                        .WithMany()
                        .HasForeignKey("AccountID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Account");
                });

            modelBuilder.Entity("FinanceFlow.Domain.Entities.Account", b =>
                {
                    b.Navigation("Tags");
                });
#pragma warning restore 612, 618
        }
    }
}
