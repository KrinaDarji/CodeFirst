using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CodeFirst.Models
{
    public class Department
    {
        [Key]
        public int DepartmentId { get; set; }
        public string Department_Name { get; set; }
    }
}
