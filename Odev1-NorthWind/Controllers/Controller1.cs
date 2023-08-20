using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Odev1_NorthWind.Models;
using System.Linq;

namespace Odev1_NorthWind.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Controller1 : ControllerBase
    {

        public IActionResult GetEmployees()
        {

            NorthWindContext context = new NorthWindContext();
            #region Method
            List<ListModel1> employees = context.Employees.Select(e => new ListModel1()
            {
                FirstName = e.FirstName,
                LastName = e.LastName,
                Title = e.Title,
                HireDate = e.HireDate
            }
            ).ToList();

            #endregion
            return Ok(employees);
        }

        [HttpGet("{id}")] 
        public IActionResult GetEmployee(int id) 
        {
            NorthWindContext context = new NorthWindContext();
            var query = from e in context.Employees where e.EmployeeId == id select e;
            Employee employee = query.SingleOrDefault();
            if(employee==null)
            {
                return NotFound("Verilen ID'de çalışan bulunamadı");
            }
            return Ok(employee);
        }

        [HttpPost]
        public IActionResult AddEmployee(EmployeeCreateModel employeeModel)
        {
            Employee employee = new Employee();
            employee.FirstName = employeeModel.FirstName;
            employee.LastName = employeeModel.LastName;
            employee.Title = employeeModel.Title;
           // employee.HireDate = employeeModel.HireDate;
            employee.ReportsTo = employeeModel.ReportsTo;

            NorthWindContext context = new NorthWindContext();
            context.Employees.Add(employee);
            context.SaveChanges();
            return CreatedAtAction(nameof(GetEmployee), new { id = employee.EmployeeId }, employee);
        }

        [HttpDelete("{id}")]
        
        public IActionResult DeleteEmployee(int id) 
        {
            NorthWindContext context = new NorthWindContext();
            Employee employee = context.Employees.SingleOrDefault(e => e.EmployeeId == id);

            if(employee==null) 
            {
                return NotFound();
            }
            context.Employees.Remove(employee);
            context.SaveChanges();
            return NoContent();
        }

        [HttpPatch("{id}")]
        public IActionResult UpdateEmployee(EmployeeCreateModel employeeModel, int id)
        {
            NorthWindContext context = new NorthWindContext();
            Employee employee = context.Employees.Find(id);
            employee.FirstName = employeeModel.FirstName;
            employee.LastName = employeeModel.LastName;
            employee.Title = employeeModel.Title;
            context.Employees.Update(employee);
            context.SaveChanges();
            return NoContent();
        }
    }
}
