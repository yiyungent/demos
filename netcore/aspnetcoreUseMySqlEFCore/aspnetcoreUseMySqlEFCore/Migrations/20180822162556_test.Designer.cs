﻿// <auto-generated />
using aspnetcoreUseMySqlEFCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.Data.EntityFrameworkCore.Storage.Internal;
using System;

namespace aspnetcoreUseMySqlEFCore.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20180822162556_test")]
    partial class test
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.3-rtm-10026");

            modelBuilder.Entity("aspnetcoreUseMySqlEFCore.Data", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Annotation");

                    b.HasKey("Id");

                    b.ToTable("DataSet");
                });
#pragma warning restore 612, 618
        }
    }
}
