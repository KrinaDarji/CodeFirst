using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CodeFirst.Models
{
    public class Employee : IdentityUser
    {
        [Key]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [DataType(DataType.EmailAddress)]
        public string EmailAddress { get; set; }
        [Required]
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }
        [Required]
        [StringLength(100)]
        public string Address { get; set; }
        // Foreign key   
        [Display(Name = "Department")]
        public virtual int DepartmentId { get; set; }

        [ForeignKey("DepartmentId")]
        public virtual Department Departments { get; set; }
        public string Gender { get; set; }
        public int Salary { get; set; }
        public DateTime DOB { get; set; }
        public string subject = "employee";
        public string body = "Welcome User";
    }
}
