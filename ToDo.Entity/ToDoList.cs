// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ToDo.Entity
{
    public partial class ToDoList
    {
        [Key]
        public int ID { get; set; }
        [Required]
        [StringLength(20)]
        public string UserName { get; set; }
        [Required]
        [StringLength(20)]
        public string Name { get; set; }
        [StringLength(150)]
        public string Describe { get; set; }
        public int? Type { get; set; }
        [StringLength(20)]
        public string StartDate { get; set; }
        [StringLength(20)]
        public string EndDate { get; set; }
        public int? Priority { get; set; }
        public int? State { get; set; }
    }
}