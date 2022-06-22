using CodeFirst.Models;
using CodeFirst.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
namespace CodeFirst.Controllers
{
    public class EmpController : Controller
    {
        private readonly IEmployeeRepository employeeRepository;
        private readonly ApplicationDbContext db;

        public EmpController(IEmployeeRepository employeeRepository, ApplicationDbContext applicationDbContext)
        {
            this.employeeRepository = employeeRepository;
            db = applicationDbContext;
        }
        // GET: EmpController
        public ActionResult Index()
        {
            var employee = employeeRepository.GetEmployees();
            return View(employee);

        }

        // GET: EmpController/Details/5
        public ActionResult Details(int id)
        {
            var employee = employeeRepository.GetEmployeeById(id);
            return View(employee);
        }

        // GET: EmpController/Create
        public ActionResult Create()
        {
            ViewData["DepartmentId"] = new SelectList(db.Departments, "DepartmentId", "Department_Name");
            return View();
        }

        // POST: EmpController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Employee employee)
        {
            if (ModelState.IsValid)
            {
                string subject = employee.subject;
                string body = employee.body;
                MailMessage mm = new MailMessage();
                mm.To.Add(employee.EmailAddress);
                mm.Subject = subject;
                mm.Body = body;
                mm.From = new MailAddress("henriette.lebsack6@ethereal.email");
                mm.IsBodyHtml = false;
                SmtpClient smtp = new SmtpClient("smtp.ethereal.email");
                smtp.Port = 587;
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new System.Net.NetworkCredential("henriette.lebsack6@ethereal.email", "2D2CEG2HpQ517cN6ty");
                smtp.Send(mm);
                employeeRepository.NewEmployee(employee);
                return RedirectToAction("Index");
            }
            ViewData["DepartmentId"] = new SelectList(db.Departments, "DepartmentId", "Department_Name", employee.DepartmentId);
            return View(employee);
        }

        // GET: EmpController/Edit/5
        public ActionResult Edit(int id)
        {
            var employee = employeeRepository.GetEmployeeById(id);
            ViewData["DepartmentId"] = new SelectList(db.Departments, "DepartmentId", "Department_Name", employee.DepartmentId);
            return View(employee);
        }

        // POST: EmpController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Employee employee)
        {
            if (ModelState.IsValid)
            {
                employeeRepository.UpdateEmployee(employee);
                db.SaveChanges();
            }
            return RedirectToAction("Index", "emp");
        }

        // GET: EmpController/Delete/5
        public ActionResult Delete(int id)
        {
            var employee = employeeRepository.GetEmployeeById(id);
            return View(employee);
        }

        // POST: EmpController/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult ConfirmDelete(int id)
        {
            employeeRepository.DeleteEmployee(id);
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        public ActionResult ExportDataToFile()
        {
            try
            {
                //  var dictioneryexportType = Request.Form.ToDictionary(x => x.Key, x => x.Value.ToString());
                var products = GetEmployeeDetail();
                ExportToCsv(products);
                return RedirectToAction(nameof(Index));
            }
            catch(Exception)
            {
                throw;
            }

        }
        private DataTable GetEmployeeDetail()
        {
            var employees = employeeRepository.GetEmployees();

            DataTable dtEmployee = new DataTable("EmployeeDetails");
            dtEmployee.Columns.AddRange(new DataColumn[10] { new DataColumn("ID"),
                                            new DataColumn("FirstName"),
                                            new DataColumn("LastName"),
                                            new DataColumn("EmailAddress"),
                                            new DataColumn("PhoneNumber"),
                                            new DataColumn("Address"),
                                            new DataColumn("DepartmentId"),
                                            new DataColumn("Gender"),
                                            new DataColumn("salary"),
                                            new DataColumn("DOB"),
                                                    });
            foreach (var emp in employees)
            {
                dtEmployee.Rows.Add(emp.Id, emp.FirstName, emp.LastName, emp.EmailAddress, emp.Phone, emp.Address, emp.DepartmentId, emp.Gender, emp.Salary, emp.DOB);
            }

            return dtEmployee;
        }
        private void ExportToCsv(DataTable employee)
        {
            try
            {
                StringBuilder sb = new StringBuilder();

                IEnumerable<string> columnNames = employee.Columns.Cast<DataColumn>().
                                                  Select(column => column.ColumnName);
                sb.AppendLine(string.Join(",", columnNames));

                foreach (DataRow row in employee.Rows)
                {
                    IEnumerable<string> fields = row.ItemArray.Select(field =>
                      string.Concat("\"", field.ToString().Replace("\"", "\"\""), "\""));
                    sb.AppendLine(string.Join(",", fields));
                }
                byte[] byteArray = ASCIIEncoding.ASCII.GetBytes(sb.ToString());
                Response.Clear();
                Response.Headers.Add("content-disposition", "attachment;filename=Employeedetails.csv");
                Response.ContentType = "application/text";
                Response.Body.WriteAsync(byteArray);
                Response.Body.Flush();
            }
            catch (Exception)
            {

                throw;
            }
          
        }
    }
}

