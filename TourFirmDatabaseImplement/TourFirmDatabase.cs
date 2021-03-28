﻿using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using TourFirmDatabaseImplement.Models;

namespace TourFirmDatabaseImplement
{
    public class TourFirmDatabase : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder.IsConfigured == false)
            {
                optionsBuilder.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=TourFirmDatabase;Integrated Security=True;MultipleActiveResultSets=True;");
            }
            base.OnConfiguring(optionsBuilder);
        }
        public virtual DbSet<Guide> Guides { set; get; }
        public virtual DbSet<Halt> Halts { set; get; }
        public virtual DbSet<Tour> Tours { set; get; }
        public virtual DbSet<Excursion> Excursions { set; get; }
        public virtual DbSet<Operator> Operators { set; get; }
        public virtual DbSet<ExcursionGuide> ExcursionGuides { set; get; }
        public virtual DbSet<TourGuide> TourGuides { set; get; }
    }
}