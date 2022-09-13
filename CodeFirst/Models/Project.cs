using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CodeFirst.Models
{
    public class Project
    {
        public int Id  { get; set; }
        public string Project_Name { get; set; }
        public string Description { get; set; }
        public string Domain { get; set; }
        public DateTime StartDate  { get; set; }
        public DateTime EndDate  { get; set; }
        // Foreign key   
        [Display(Name = "Department")]
        public virtual int DepartmentId { get; set; }

        [ForeignKey("DepartmentId")]
        public virtual Department Departments { get; set; }
        // Foreign key   
        [Display(Name = "Manager")]
        public virtual int ManagerId { get; set; }

        [ForeignKey("ManagerId")]
        public virtual Manager Managers { get; set; }
        public projectstatus Status { get; set; }

    }
}
