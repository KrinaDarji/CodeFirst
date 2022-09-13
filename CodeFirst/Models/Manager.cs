using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CodeFirst.Models
{
    public class Manager
    {
        [Key]
        public int ManagerId { get; set; }
        // Foreign key   
        [Display(Name = "employees")]
        
        //public  int Id { get; set; }
        public virtual int Id { get; set; }

        [ForeignKey("Id")]
        public  virtual Employee Employees { get; set; }
        // Foreign key   
        [Display(Name = "Department")]
        public virtual int DepartmentId { get; set; }

        [ForeignKey("DepartmentId")]
        public virtual Department Departments { get; set; }
        public int Rank { get; set; }
    }
}
