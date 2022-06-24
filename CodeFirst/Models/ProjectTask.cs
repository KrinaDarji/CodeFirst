using System;
using System.ComponentModel.DataAnnotations;

namespace CodeFirst.Models
{
    public class ProjectTask
    {
        public int Id { get; set; }
        public string TaskName { get; set; }
        [StringLength(100)]
        public string Description { get; set; }
        public string TaskAssignTo { get; set; }
        public string TaskCreatedBy { get; set; }
        public int Workinghours { get; set; }
        public DateTime LastDate { get; set; }

    }
}
