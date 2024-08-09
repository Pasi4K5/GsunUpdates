﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Numerous.Database.Context;

#nullable disable

namespace Numerous.Database.Migrations
{
    [DbContext(typeof(NumerousDbContext))]
    [Migration("20240730083947_Init")]
    partial class Init
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseSerialColumns(modelBuilder);

            modelBuilder.Entity("Numerous.Database.Entities.DbAutoPingMapping", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseSerialColumn(b.Property<long>("Id"));

                    b.Property<decimal>("ChannelId")
                        .HasColumnType("numeric(20,0)");

                    b.Property<decimal>("RoleId")
                        .HasColumnType("numeric(20,0)");

                    b.Property<decimal?>("TagId")
                        .HasColumnType("numeric(20,0)");

                    b.HasKey("Id");

                    b.HasIndex("ChannelId", "TagId", "RoleId")
                        .IsUnique();

                    b.ToTable("AutoPingMapping");
                });

            modelBuilder.Entity("Numerous.Database.Entities.DbChannel", b =>
                {
                    b.Property<decimal>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("numeric(20,0)");

                    b.Property<decimal>("GuildId")
                        .HasColumnType("numeric(20,0)");

                    b.HasKey("Id");

                    b.HasIndex("GuildId");

                    b.ToTable("Channel");

                    b.UseTptMappingStrategy();
                });

            modelBuilder.Entity("Numerous.Database.Entities.DbDiscordMessage", b =>
                {
                    b.Property<decimal>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("numeric(20,0)");

                    b.Property<decimal>("AuthorId")
                        .HasColumnType("numeric(20,0)");

                    b.Property<decimal>("ChannelId")
                        .HasColumnType("numeric(20,0)");

                    b.Property<DateTimeOffset?>("DeletedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("IsHidden")
                        .HasColumnType("boolean");

                    b.Property<decimal?>("ReferenceMessageId")
                        .HasColumnType("numeric(20,0)");

                    b.HasKey("Id");

                    b.HasIndex("AuthorId");

                    b.HasIndex("ChannelId");

                    b.HasIndex("ReferenceMessageId");

                    b.ToTable("DiscordMessage");
                });

            modelBuilder.Entity("Numerous.Database.Entities.DbDiscordMessageVersion", b =>
                {
                    b.Property<decimal>("MessageId")
                        .HasColumnType("numeric(20,0)");

                    b.Property<DateTimeOffset>("Timestamp")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("CleanContent")
                        .HasMaxLength(4000)
                        .HasColumnType("character varying(4000)")
                        .HasComment("If NULL, the clean content is the same as the raw content.");

                    b.Property<string>("RawContent")
                        .IsRequired()
                        .HasMaxLength(4000)
                        .HasColumnType("character varying(4000)");

                    b.HasKey("MessageId", "Timestamp");

                    b.ToTable("DiscordMessageVersion");
                });

            modelBuilder.Entity("Numerous.Database.Entities.DbDiscordUser", b =>
                {
                    b.Property<decimal>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("numeric(20,0)");

                    b.Property<string>("TimeZoneId")
                        .HasMaxLength(64)
                        .HasColumnType("character varying(64)");

                    b.HasKey("Id");

                    b.ToTable("DiscordUser");
                });

            modelBuilder.Entity("Numerous.Database.Entities.DbGroupRoleMapping", b =>
                {
                    b.Property<decimal>("GuildId")
                        .HasColumnType("numeric(20,0)");

                    b.Property<decimal>("RoleId")
                        .HasColumnType("numeric(20,0)");

                    b.Property<short>("Group")
                        .HasColumnType("smallint")
                        .HasColumnName("GroupId");

                    b.HasKey("GuildId", "RoleId", "Group");

                    b.ToTable("GroupRoleMapping");
                });

            modelBuilder.Entity("Numerous.Database.Entities.DbGuild", b =>
                {
                    b.Property<decimal>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("numeric(20,0)");

                    b.Property<bool>("TrackMessages")
                        .HasColumnType("boolean");

                    b.Property<decimal?>("UnverifiedRoleId")
                        .HasColumnType("numeric(20,0)");

                    b.HasKey("Id");

                    b.HasIndex("UnverifiedRoleId")
                        .IsUnique();

                    b.ToTable("Guild");
                });

            modelBuilder.Entity("Numerous.Database.Entities.DbJoinMessage", b =>
                {
                    b.Property<decimal>("GuildId")
                        .HasColumnType("numeric(20,0)");

                    b.Property<decimal>("ChannelId")
                        .HasColumnType("numeric(20,0)");

                    b.Property<string>("Description")
                        .HasMaxLength(4096)
                        .HasColumnType("character varying(4096)");

                    b.Property<string>("Title")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.HasKey("GuildId");

                    b.HasIndex("ChannelId");

                    b.ToTable("JoinMessage", t =>
                        {
                            t.HasCheckConstraint("CK_JoinMessage_HasText", "\"Title\" IS NOT NULL OR \"Description\" IS NOT NULL");
                        });
                });

            modelBuilder.Entity("Numerous.Database.Entities.DbOsuUser", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseSerialColumn(b.Property<long>("Id"));

                    b.Property<decimal>("DiscordUserId")
                        .HasColumnType("numeric(20,0)");

                    b.HasKey("Id");

                    b.HasIndex("DiscordUserId")
                        .IsUnique();

                    b.ToTable("OsuUser");
                });

            modelBuilder.Entity("Numerous.Database.Entities.DbReminder", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseSerialColumn(b.Property<long>("Id"));

                    b.Property<decimal>("ChannelId")
                        .HasColumnType("numeric(20,0)");

                    b.Property<string>("Message")
                        .HasMaxLength(6000)
                        .HasColumnType("character varying(6000)");

                    b.Property<DateTimeOffset>("Timestamp")
                        .HasColumnType("timestamp with time zone");

                    b.Property<decimal>("UserId")
                        .HasColumnType("numeric(20,0)");

                    b.HasKey("Id");

                    b.HasIndex("ChannelId");

                    b.HasIndex("UserId");

                    b.ToTable("Reminder");
                });

            modelBuilder.Entity("Numerous.Database.Entities.DbForumChannel", b =>
                {
                    b.HasBaseType("Numerous.Database.Entities.DbChannel");

                    b.ToTable("ForumChannel");
                });

            modelBuilder.Entity("Numerous.Database.Entities.DbMessageChannel", b =>
                {
                    b.HasBaseType("Numerous.Database.Entities.DbChannel");

                    b.Property<bool>("IsReadOnly")
                        .HasColumnType("boolean");

                    b.ToTable("MessageChannel");
                });

            modelBuilder.Entity("Numerous.Database.Entities.DbAutoPingMapping", b =>
                {
                    b.HasOne("Numerous.Database.Entities.DbForumChannel", "Channel")
                        .WithMany("AutoPingMappings")
                        .HasForeignKey("ChannelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Channel");
                });

            modelBuilder.Entity("Numerous.Database.Entities.DbChannel", b =>
                {
                    b.HasOne("Numerous.Database.Entities.DbGuild", "Guild")
                        .WithMany("Channels")
                        .HasForeignKey("GuildId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Guild");
                });

            modelBuilder.Entity("Numerous.Database.Entities.DbDiscordMessage", b =>
                {
                    b.HasOne("Numerous.Database.Entities.DbDiscordUser", "Author")
                        .WithMany("Messages")
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Numerous.Database.Entities.DbMessageChannel", "Channel")
                        .WithMany("Messages")
                        .HasForeignKey("ChannelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Numerous.Database.Entities.DbDiscordMessage", "ReferenceMessage")
                        .WithMany("Replies")
                        .HasForeignKey("ReferenceMessageId");

                    b.Navigation("Author");

                    b.Navigation("Channel");

                    b.Navigation("ReferenceMessage");
                });

            modelBuilder.Entity("Numerous.Database.Entities.DbDiscordMessageVersion", b =>
                {
                    b.HasOne("Numerous.Database.Entities.DbDiscordMessage", "Message")
                        .WithMany("Versions")
                        .HasForeignKey("MessageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Message");
                });

            modelBuilder.Entity("Numerous.Database.Entities.DbGroupRoleMapping", b =>
                {
                    b.HasOne("Numerous.Database.Entities.DbGuild", "Guild")
                        .WithMany("GroupRoleMappings")
                        .HasForeignKey("GuildId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Guild");
                });

            modelBuilder.Entity("Numerous.Database.Entities.DbJoinMessage", b =>
                {
                    b.HasOne("Numerous.Database.Entities.DbMessageChannel", "Channel")
                        .WithMany()
                        .HasForeignKey("ChannelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Numerous.Database.Entities.DbGuild", "Guild")
                        .WithOne("JoinMessage")
                        .HasForeignKey("Numerous.Database.Entities.DbJoinMessage", "GuildId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Channel");

                    b.Navigation("Guild");
                });

            modelBuilder.Entity("Numerous.Database.Entities.DbOsuUser", b =>
                {
                    b.HasOne("Numerous.Database.Entities.DbDiscordUser", "DiscordUser")
                        .WithOne("OsuUser")
                        .HasForeignKey("Numerous.Database.Entities.DbOsuUser", "DiscordUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("DiscordUser");
                });

            modelBuilder.Entity("Numerous.Database.Entities.DbReminder", b =>
                {
                    b.HasOne("Numerous.Database.Entities.DbMessageChannel", "Channel")
                        .WithMany()
                        .HasForeignKey("ChannelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Numerous.Database.Entities.DbDiscordUser", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Channel");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Numerous.Database.Entities.DbForumChannel", b =>
                {
                    b.HasOne("Numerous.Database.Entities.DbChannel", null)
                        .WithOne()
                        .HasForeignKey("Numerous.Database.Entities.DbForumChannel", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Numerous.Database.Entities.DbMessageChannel", b =>
                {
                    b.HasOne("Numerous.Database.Entities.DbChannel", null)
                        .WithOne()
                        .HasForeignKey("Numerous.Database.Entities.DbMessageChannel", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Numerous.Database.Entities.DbDiscordMessage", b =>
                {
                    b.Navigation("Replies");

                    b.Navigation("Versions");
                });

            modelBuilder.Entity("Numerous.Database.Entities.DbDiscordUser", b =>
                {
                    b.Navigation("Messages");

                    b.Navigation("OsuUser")
                        .IsRequired();
                });

            modelBuilder.Entity("Numerous.Database.Entities.DbGuild", b =>
                {
                    b.Navigation("Channels");

                    b.Navigation("GroupRoleMappings");

                    b.Navigation("JoinMessage");
                });

            modelBuilder.Entity("Numerous.Database.Entities.DbForumChannel", b =>
                {
                    b.Navigation("AutoPingMappings");
                });

            modelBuilder.Entity("Numerous.Database.Entities.DbMessageChannel", b =>
                {
                    b.Navigation("Messages");
                });
#pragma warning restore 612, 618
        }
    }
}