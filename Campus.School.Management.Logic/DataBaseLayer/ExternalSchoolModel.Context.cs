﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Campus.School.Management.Logic.DataBaseLayer
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class campuserpEntities : DbContext
    {
        public campuserpEntities()
            : base("name=campuserpEntitiestaha")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<admGrade> admGrades { get; set; }
        public virtual DbSet<AdmGradesSchool> AdmGradesSchools { get; set; }
        public virtual DbSet<admStudyear> admStudyears { get; set; }
        public virtual DbSet<AdmStudent_Class> AdmStudent_Class { get; set; }
        public virtual DbSet<AdmStudent> AdmStudents { get; set; }
    }
}