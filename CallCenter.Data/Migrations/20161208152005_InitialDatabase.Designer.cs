using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using CallCenter.Data;

namespace CallCenter.Data.Migrations
{
    [DbContext(typeof(CallCenterContext))]
    [Migration("20161208152005_InitialDatabase")]
    partial class InitialDatabase
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.0-rtm-21431")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("CallCenter.Model.Entities.Call", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CallStatus")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(0);

                    b.Property<int>("CampaignId");

                    b.Property<int>("CustomerId");

                    b.Property<string>("Note");

                    b.Property<DateTime>("Time");

                    b.Property<string>("UserName");

                    b.HasKey("Id");

                    b.HasIndex("CampaignId");

                    b.HasIndex("CustomerId");

                    b.ToTable("Calls");
                });

            modelBuilder.Entity("CallCenter.Model.Entities.Campaign", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreationDate");

                    b.Property<string>("Description");

                    b.Property<DateTime>("ExpirationDate");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Campaigns");
                });

            modelBuilder.Entity("CallCenter.Model.Entities.Customer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedDate");

                    b.Property<string>("Email");

                    b.Property<string>("Name");

                    b.Property<string>("Note");

                    b.Property<string>("Phone");

                    b.Property<string>("Surname");

                    b.HasKey("Id");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("CallCenter.Model.Entities.Call", b =>
                {
                    b.HasOne("CallCenter.Model.Entities.Campaign", "Campaign")
                        .WithMany("Calls")
                        .HasForeignKey("CampaignId");

                    b.HasOne("CallCenter.Model.Entities.Customer", "Customer")
                        .WithMany("Calls")
                        .HasForeignKey("CustomerId");
                });
        }
    }
}
