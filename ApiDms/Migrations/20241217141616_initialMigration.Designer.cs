﻿// <auto-generated />
using System;
using ApiDms.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ApiDms.Migrations
{
    [DbContext(typeof(DMSDbContext))]
    [Migration("20241217141616_initialMigration")]
    partial class initialMigration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("ApiDms.Models.Collection", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("id"));

                    b.Property<string>("collection_id")
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("collection_id");

                    b.Property<string>("collection_name")
                        .HasColumnType("text")
                        .HasColumnName("collection_name");

                    b.Property<DateTime>("created_at")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("created_at");

                    b.Property<string>("created_by")
                        .HasMaxLength(10)
                        .HasColumnType("character varying(10)")
                        .HasColumnName("created_by");

                    b.Property<string>("description")
                        .HasColumnType("text")
                        .HasColumnName("description");

                    b.Property<DateTime?>("last_updated_at")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("last_updated_at");

                    b.Property<int>("status")
                        .HasColumnType("integer")
                        .HasColumnName("status");

                    b.Property<DateTime?>("updated_at")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("updated_at");

                    b.Property<string>("updated_by")
                        .HasMaxLength(10)
                        .HasColumnType("character varying(10)")
                        .HasColumnName("updated_by");

                    b.HasKey("id");

                    b.ToTable("collections");
                });

            modelBuilder.Entity("ApiDms.Models.CollectionDocumentType", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("id"));

                    b.Property<string>("collection_id")
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("collection_id");

                    b.Property<DateTime>("created_at")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("created_at");

                    b.Property<string>("created_by")
                        .HasMaxLength(10)
                        .HasColumnType("character varying(10)")
                        .HasColumnName("created_by");

                    b.Property<string>("document_type_id")
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("document_type_id");

                    b.Property<DateTime?>("last_updated_at")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("last_updated_at");

                    b.Property<int>("status")
                        .HasColumnType("integer")
                        .HasColumnName("status");

                    b.Property<DateTime?>("updated_at")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("updated_at");

                    b.Property<string>("updated_by")
                        .HasMaxLength(10)
                        .HasColumnType("character varying(10)")
                        .HasColumnName("updated_by");

                    b.HasKey("id");

                    b.ToTable("collection_document_types");
                });

            modelBuilder.Entity("ApiDms.Models.Directory", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("id"));

                    b.Property<string>("collection_id")
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("collection_id");

                    b.Property<DateTime>("created_at")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("created_at");

                    b.Property<string>("created_by")
                        .HasMaxLength(10)
                        .HasColumnType("character varying(10)")
                        .HasColumnName("created_by");

                    b.Property<string>("directory_id")
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("directory_id");

                    b.Property<string>("directory_name")
                        .HasColumnType("text")
                        .HasColumnName("directory_name");

                    b.Property<string>("disk")
                        .HasColumnType("text")
                        .HasColumnName("disk");

                    b.Property<DateTime?>("last_updated_at")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("last_updated_at");

                    b.Property<string>("owner_id")
                        .HasMaxLength(10)
                        .HasColumnType("character varying(10)")
                        .HasColumnName("owner_id");

                    b.Property<int>("parent_id")
                        .HasColumnType("integer")
                        .HasColumnName("parent_id");

                    b.Property<string>("path_name")
                        .HasColumnType("text")
                        .HasColumnName("path_name");

                    b.Property<int>("status")
                        .HasColumnType("integer")
                        .HasColumnName("status");

                    b.Property<DateTime?>("updated_at")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("updated_at");

                    b.Property<string>("updated_by")
                        .HasMaxLength(10)
                        .HasColumnType("character varying(10)")
                        .HasColumnName("updated_by");

                    b.HasKey("id");

                    b.ToTable("directories");
                });

            modelBuilder.Entity("ApiDms.Models.Document", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("id"));

                    b.Property<string>("collection_id")
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("collection_id");

                    b.Property<string>("content_type")
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("content_type");

                    b.Property<DateTime>("created_at")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("created_at");

                    b.Property<string>("created_by")
                        .HasMaxLength(10)
                        .HasColumnType("character varying(10)")
                        .HasColumnName("created_by");

                    b.Property<string>("directory_id")
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("directory_id");

                    b.Property<string>("document_id")
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("document_id");

                    b.Property<string>("document_type_id")
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("document_type_id");

                    b.Property<int>("download")
                        .HasColumnType("integer")
                        .HasColumnName("download");

                    b.Property<DateTime?>("expired_date")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("expired_date");

                    b.Property<string>("extension")
                        .HasMaxLength(10)
                        .HasColumnType("character varying(10)")
                        .HasColumnName("extension");

                    b.Property<string>("file_name")
                        .HasColumnType("text")
                        .HasColumnName("file_name");

                    b.Property<string>("file_path")
                        .HasColumnType("text")
                        .HasColumnName("file_path");

                    b.Property<long>("file_size")
                        .HasColumnType("bigint")
                        .HasColumnName("file_size");

                    b.Property<DateTime?>("last_updated_at")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("last_updated_at");

                    b.Property<string>("owner_id")
                        .HasMaxLength(10)
                        .HasColumnType("character varying(10)")
                        .HasColumnName("owner_id");

                    b.Property<int>("status")
                        .HasColumnType("integer")
                        .HasColumnName("status");

                    b.Property<DateTime?>("updated_at")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("updated_at");

                    b.Property<string>("updated_by")
                        .HasMaxLength(10)
                        .HasColumnType("character varying(10)")
                        .HasColumnName("updated_by");

                    b.Property<int>("version")
                        .HasColumnType("integer")
                        .HasColumnName("version");

                    b.HasKey("id");

                    b.ToTable("documents");
                });

            modelBuilder.Entity("ApiDms.Models.DocumentFavorite", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("id"));

                    b.Property<DateTime>("created_at")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("created_at");

                    b.Property<string>("created_by")
                        .HasMaxLength(10)
                        .HasColumnType("character varying(10)")
                        .HasColumnName("created_by");

                    b.Property<string>("document_id")
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("document_id");

                    b.Property<DateTime?>("last_updated_at")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("last_updated_at");

                    b.Property<string>("owner_id")
                        .HasMaxLength(10)
                        .HasColumnType("character varying(10)")
                        .HasColumnName("owner_id");

                    b.Property<int>("status")
                        .HasColumnType("integer")
                        .HasColumnName("status");

                    b.Property<DateTime?>("updated_at")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("updated_at");

                    b.Property<string>("updated_by")
                        .HasMaxLength(10)
                        .HasColumnType("character varying(10)")
                        .HasColumnName("updated_by");

                    b.HasKey("id");

                    b.ToTable("document_favorites");
                });

            modelBuilder.Entity("ApiDms.Models.DocumentIndex", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("id"));

                    b.Property<DateTime>("created_at")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("created_at");

                    b.Property<string>("created_by")
                        .HasMaxLength(10)
                        .HasColumnType("character varying(10)")
                        .HasColumnName("created_by");

                    b.Property<int>("document_type_id")
                        .HasColumnType("integer")
                        .HasColumnName("document_type_id");

                    b.Property<string>("index_name")
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("index_name");

                    b.Property<string>("index_value")
                        .HasColumnType("text")
                        .HasColumnName("index_value");

                    b.Property<DateTime?>("last_updated_at")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("last_updated_at");

                    b.Property<string>("rules")
                        .HasColumnType("text")
                        .HasColumnName("rules");

                    b.Property<int>("status")
                        .HasColumnType("integer")
                        .HasColumnName("status");

                    b.Property<DateTime?>("updated_at")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("updated_at");

                    b.Property<string>("updated_by")
                        .HasMaxLength(10)
                        .HasColumnType("character varying(10)")
                        .HasColumnName("updated_by");

                    b.HasKey("id");

                    b.ToTable("document_indices");
                });

            modelBuilder.Entity("ApiDms.Models.DocumentType", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("id"));

                    b.Property<DateTime>("created_at")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("created_at");

                    b.Property<string>("created_by")
                        .HasMaxLength(10)
                        .HasColumnType("character varying(10)")
                        .HasColumnName("created_by");

                    b.Property<string>("description")
                        .HasColumnType("text")
                        .HasColumnName("description");

                    b.Property<string>("document_type_id")
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("document_type_id");

                    b.Property<string>("document_type_name")
                        .HasColumnType("text")
                        .HasColumnName("document_type_name");

                    b.Property<DateTime?>("last_updated_at")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("last_updated_at");

                    b.Property<int>("status")
                        .HasColumnType("integer")
                        .HasColumnName("status");

                    b.Property<DateTime?>("updated_at")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("updated_at");

                    b.Property<string>("updated_by")
                        .HasMaxLength(10)
                        .HasColumnType("character varying(10)")
                        .HasColumnName("updated_by");

                    b.HasKey("id");

                    b.ToTable("document_types");
                });
#pragma warning restore 612, 618
        }
    }
}
