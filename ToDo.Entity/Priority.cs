﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ToDo.Entity
{
    public partial class Priority
    {
        [Key]
        public int ID { get; set; }
        [Column("Priority")]
        [StringLength(10)]
        public string priority { get; set; }
    }
}