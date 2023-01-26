﻿// <auto-generated />
using System;
using MedicalBlog.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace MedicalBlog.Persistence.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20230126205904_init")]
    partial class init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.12")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("MedicalBlog.Domain.Entities.Answer", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<string>("AnswerString")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("CreationDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime(6)")
                        .HasDefaultValue(new DateTime(2023, 1, 26, 22, 59, 4, 555, DateTimeKind.Local).AddTicks(3050));

                    b.Property<string>("QuestionId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("QuestionId");

                    b.HasIndex("UserId");

                    b.ToTable("Answers", (string)null);
                });

            modelBuilder.Entity("MedicalBlog.Domain.Entities.AnswerAgreement", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<string>("AnswerId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("CreationDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime(6)")
                        .HasDefaultValue(new DateTime(2023, 1, 26, 22, 59, 4, 554, DateTimeKind.Local).AddTicks(8105));

                    b.Property<bool>("IsAgreed")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("AnswerId");

                    b.HasIndex("UserId");

                    b.ToTable("AnswerAgreements", (string)null);
                });

            modelBuilder.Entity("MedicalBlog.Domain.Entities.Comment", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<string>("AuthorId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("longtext");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("CreationDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime(6)")
                        .HasDefaultValue(new DateTime(2023, 1, 26, 22, 59, 4, 556, DateTimeKind.Local).AddTicks(4070));

                    b.Property<DateTime?>("ModifiedOn")
                        .IsRequired()
                        .HasColumnType("datetime(6)");

                    b.Property<string>("ParentId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.Property<string>("PostId")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("AuthorId");

                    b.HasIndex("ParentId");

                    b.ToTable("Comments", (string)null);
                });

            modelBuilder.Entity("MedicalBlog.Domain.Entities.CommentAgreement", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<string>("CommentId")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("CreationDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime(6)")
                        .HasDefaultValue(new DateTime(2023, 1, 26, 22, 59, 4, 555, DateTimeKind.Local).AddTicks(7832));

                    b.Property<bool>("IsAgreed")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("CommentAgreements", (string)null);
                });

            modelBuilder.Entity("MedicalBlog.Domain.Entities.Post", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<string>("AuthorId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("longtext");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("CreationDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime(6)")
                        .HasDefaultValue(new DateTime(2023, 1, 26, 22, 59, 4, 557, DateTimeKind.Local).AddTicks(3583));

                    b.Property<DateTime?>("ModifiedOn")
                        .IsRequired()
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Tags")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("AuthorId");

                    b.ToTable("Posts", (string)null);
                });

            modelBuilder.Entity("MedicalBlog.Domain.Entities.PostRating", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("CreationDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime(6)")
                        .HasDefaultValue(new DateTime(2023, 1, 26, 22, 59, 4, 557, DateTimeKind.Local).AddTicks(6037));

                    b.Property<string>("PostId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.Property<int>("Rating")
                        .HasMaxLength(15)
                        .HasColumnType("int");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("PostId");

                    b.HasIndex("UserId");

                    b.ToTable("PostsRating", (string)null);
                });

            modelBuilder.Entity("MedicalBlog.Domain.Entities.PostView", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("CreationDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime(6)")
                        .HasDefaultValue(new DateTime(2023, 1, 26, 22, 59, 4, 558, DateTimeKind.Local).AddTicks(715));

                    b.Property<string>("PostId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("PostId");

                    b.HasIndex("UserId");

                    b.ToTable("PostsView", (string)null);
                });

            modelBuilder.Entity("MedicalBlog.Domain.Entities.Question", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("CreationDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime(6)")
                        .HasDefaultValue(new DateTime(2023, 1, 26, 22, 59, 4, 558, DateTimeKind.Local).AddTicks(4789));

                    b.Property<string>("QuestionString")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Questions", (string)null);
                });

            modelBuilder.Entity("MedicalBlog.Domain.Entities.User", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("CreationDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime(6)")
                        .HasDefaultValue(new DateTime(2023, 1, 26, 22, 59, 4, 558, DateTimeKind.Local).AddTicks(7465));

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("ProfilePictureUrl")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Users", (string)null);
                });

            modelBuilder.Entity("MedicalBlog.Domain.Entities.Answer", b =>
                {
                    b.HasOne("MedicalBlog.Domain.Entities.Question", "Question")
                        .WithMany("Answers")
                        .HasForeignKey("QuestionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MedicalBlog.Domain.Entities.User", "User")
                        .WithMany("Answers")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Question");

                    b.Navigation("User");
                });

            modelBuilder.Entity("MedicalBlog.Domain.Entities.AnswerAgreement", b =>
                {
                    b.HasOne("MedicalBlog.Domain.Entities.Answer", "Answer")
                        .WithMany("AnswerAgreements")
                        .HasForeignKey("AnswerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MedicalBlog.Domain.Entities.User", "User")
                        .WithMany("AnswerAgreements")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Answer");

                    b.Navigation("User");
                });

            modelBuilder.Entity("MedicalBlog.Domain.Entities.Comment", b =>
                {
                    b.HasOne("MedicalBlog.Domain.Entities.User", "Author")
                        .WithMany("Comments")
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MedicalBlog.Domain.Entities.Post", "Post")
                        .WithMany("Comments")
                        .HasForeignKey("Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MedicalBlog.Domain.Entities.Comment", "ParentComment")
                        .WithMany("ChildComments")
                        .HasForeignKey("ParentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Author");

                    b.Navigation("ParentComment");

                    b.Navigation("Post");
                });

            modelBuilder.Entity("MedicalBlog.Domain.Entities.CommentAgreement", b =>
                {
                    b.HasOne("MedicalBlog.Domain.Entities.Comment", "Comment")
                        .WithMany("CommentAgreements")
                        .HasForeignKey("Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MedicalBlog.Domain.Entities.User", "User")
                        .WithMany("CommentAgreements")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Comment");

                    b.Navigation("User");
                });

            modelBuilder.Entity("MedicalBlog.Domain.Entities.Post", b =>
                {
                    b.HasOne("MedicalBlog.Domain.Entities.User", "Author")
                        .WithMany("Posts")
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Author");
                });

            modelBuilder.Entity("MedicalBlog.Domain.Entities.PostRating", b =>
                {
                    b.HasOne("MedicalBlog.Domain.Entities.Post", "Post")
                        .WithMany("PostRatings")
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MedicalBlog.Domain.Entities.User", "User")
                        .WithMany("PostRatings")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Post");

                    b.Navigation("User");
                });

            modelBuilder.Entity("MedicalBlog.Domain.Entities.PostView", b =>
                {
                    b.HasOne("MedicalBlog.Domain.Entities.Post", "Post")
                        .WithMany("PostViews")
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MedicalBlog.Domain.Entities.User", "User")
                        .WithMany("PostViews")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Post");

                    b.Navigation("User");
                });

            modelBuilder.Entity("MedicalBlog.Domain.Entities.Question", b =>
                {
                    b.HasOne("MedicalBlog.Domain.Entities.User", "User")
                        .WithMany("Questions")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("MedicalBlog.Domain.Entities.Answer", b =>
                {
                    b.Navigation("AnswerAgreements");
                });

            modelBuilder.Entity("MedicalBlog.Domain.Entities.Comment", b =>
                {
                    b.Navigation("ChildComments");

                    b.Navigation("CommentAgreements");
                });

            modelBuilder.Entity("MedicalBlog.Domain.Entities.Post", b =>
                {
                    b.Navigation("Comments");

                    b.Navigation("PostRatings");

                    b.Navigation("PostViews");
                });

            modelBuilder.Entity("MedicalBlog.Domain.Entities.Question", b =>
                {
                    b.Navigation("Answers");
                });

            modelBuilder.Entity("MedicalBlog.Domain.Entities.User", b =>
                {
                    b.Navigation("AnswerAgreements");

                    b.Navigation("Answers");

                    b.Navigation("CommentAgreements");

                    b.Navigation("Comments");

                    b.Navigation("PostRatings");

                    b.Navigation("PostViews");

                    b.Navigation("Posts");

                    b.Navigation("Questions");
                });
#pragma warning restore 612, 618
        }
    }
}