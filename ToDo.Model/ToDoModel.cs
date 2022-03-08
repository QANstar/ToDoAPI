using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDo.Model
{
    public class ToDoModel
    {
        public int ID { get; set; }
        [Required]
        [StringLength(20)]
        public string Name { get; set; }
        [StringLength(150)]
        public string Describe { get; set; }
        [Required]
        public int? Type { get; set; }
        [StringLength(20)]
        public string StartDate { get; set; }
        [StringLength(20)]
        public string EndDate { get; set; }
        [Required]
        public int? Priority { get; set; }
        [Required]
        public int? State { get; set; }
    }
}
