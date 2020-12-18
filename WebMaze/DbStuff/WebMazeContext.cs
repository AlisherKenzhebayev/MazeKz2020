﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebMaze.DbStuff.Model;
using WebMaze.DbStuff.Model.Medicine;
using WebMaze.DbStuff.Model.Police;

namespace WebMaze.DbStuff
{
    public class WebMazeContext : DbContext
    {
        public DbSet<CitizenUser> CitizenUser { get; set; }

        public DbSet<Adress> Adress { get; set; }

        public DbSet<Policeman> Policemen { get; set; }
        public DbSet<HealthDepartment> HealthDepartment { get; set; }
        public DbSet<RecordForm> RecordForms { get; set; }

        public WebMazeContext(DbContextOptions dbContext) : base(dbContext) { }
    }
}
