﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace STG.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class Entities : DbContext
    {
        public Entities()
            : base("name=Entities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<C__MigrationHistory> C__MigrationHistory { get; set; }
        public virtual DbSet<AspNetRoles> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUserClaims> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogins> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUsers> AspNetUsers { get; set; }
        public virtual DbSet<Teachers> Teachers { get; set; }
        public virtual DbSet<Groups> Groups { get; set; }
        public virtual DbSet<Rooms> Rooms { get; set; }
        public virtual DbSet<Schools> Schools { get; set; }
        public virtual DbSet<RoomTypes> RoomTypes { get; set; }
        public virtual DbSet<SubjectTypes> SubjectTypes { get; set; }
        public virtual DbSet<Subjects> Subjects { get; set; }
        public virtual DbSet<SubGroupTypes> SubGroupTypes { get; set; }
        public virtual DbSet<Lessons> Lessons { get; set; }
        public virtual DbSet<Timetables> Timetables { get; set; }
        public virtual DbSet<STGConfig> STGConfig { get; set; }
    }
}
