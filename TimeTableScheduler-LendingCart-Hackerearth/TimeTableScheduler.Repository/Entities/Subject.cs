﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace TimeTableScheduler.Repository.Entities
{
    public partial class Subject
    {
        public Subject()
        {
            Teacher = new HashSet<Teacher>();
        }

        public int Id { get; set; }
        public int ClassId { get; set; }
        public string Name { get; set; }

        public virtual Class Class { get; set; }
        public virtual ICollection<Teacher> Teacher { get; set; }
    }
}