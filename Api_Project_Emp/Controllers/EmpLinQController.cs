using Api_Project_Emp.Data;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Api_Project_Emp.Migrations;
using Api_Project_Emp.Model;
using Api_Project_Emp.Model.Response;
using Microsoft.EntityFrameworkCore;
using System.Transactions;
namespace Api_Project_Emp.Controllers
{
    public class EmpLinQController : Controller
    {
        private readonly ApplicationDb context;

        public EmpLinQController(ApplicationDb context)
        {
            this.context = context;
        }
        //[HttpGet("GetDataEmp")]
        //public IActionResult GetDataEmp()
        //{
        //    var data = context.employees
        //                      .ToList();
        //    return Ok(data);
        //}
        //[HttpGet("GetDataByName")]
        //public IActionResult GetDataByName(int id)
        //{
        //    var data = context.employees
        //                      .Where(x => x.Id == id)
        //                      .Select(x => x.FirstName).ToList();
        //    return Ok(data);
        //}

        [HttpGet("GetDatEmpDetails")]
        public IActionResult GetDatEmpDetails()
        {
            var data = context.employeeDetails
                              .ToList();
            return Ok(data);
        }

        // use of where clouse
        [HttpGet("UseWhere")]
        public IActionResult UseWhere()
        { 
            var data =(from emp in context.employeeDetails
                      where emp.age> 20
                      select emp).ToList();
            return Ok(data); 
        }
        [HttpGet("GetDataEmp2")]

        public IActionResult GetDataEmp2()
        {
            // Assuming employeeDetails is a DbSet in YourDbContext
            var data =  (from emp in context.employeeDetails orderby
                        emp.Name
                        select emp).ToList();
            return Ok(data);
                
        }
        //use of Descending oder
        [HttpGet("Descending")]
        public IActionResult Descending()
        {
            var data = (from emp in context.employeeDetails
                orderby emp.Name descending
                select emp).ToList();
            return Ok(data);

                }
        [HttpGet("ThenByUse")]
        public IActionResult ThenByUse()
        {
            var data = (from emp in context.employeeDetails
                        orderby emp.Name, emp.age // Sort first by Name, then by age
                        select emp).ToList();

            return Ok(data);
        }
        [HttpGet("Inner")]
        public IActionResult Inner()
        {
            var result = (from emp in context.employeeDetails
                          join dept in context.departments on emp.DepartmentId equals dept.DepartmentId
                          select new
                          {
                              emp.Name,
                              dept.DepartmentName
                          }).ToList(); // Materialize the query result into a list

            return Ok(result);
        }
        [HttpGet("First")]
        public IActionResult First()
        {
            var data = (from emp in context.employeeDetails
                        join dept in context.departments on emp.DepartmentId equals dept.DepartmentId
                        select new
                        {
                            emp.Name,
                            emp.age,
                            emp.Salary,
                            dept.DepartmentName
                        }).ToList();
            return Ok(data);
        }
        [HttpGet("Left")]
        public IActionResult Left()
        {
            var data = (from emp in context.employeeDetails
                        join dept in context.departments on emp.DepartmentId equals dept.DepartmentId into empDept
                        from dept in empDept.DefaultIfEmpty()
                        select new
                        {
                            emp.Name,
                            emp.age,
                            emp.Salary,
                            DepartmentName = dept != null ? dept.DepartmentName : "No Department"
                        }).ToList();

            return Ok(data);
        }
        [HttpGet("Right")]
        public IActionResult Right()
        {
            var data = (from dept in context.departments
                        join emp in context.employeeDetails on dept.DepartmentId equals emp.DepartmentId into deptEmp
                        from emp in deptEmp.DefaultIfEmpty()
                        select new
                        {
                            Name = emp != null ? emp.Name : "No Employee",
                            Age = emp != null ? emp.age : 0, // Assuming age is an int, replace with appropriate default value
                            Salary = emp != null ? emp.Salary : 0.0, // Assuming Salary is a double, replace with appropriate default value
                            dept.DepartmentName
                        }).ToList();

            return Ok(data);
        }
        //  Use of select 
        [HttpGet("UseSelect")]
        public IActionResult UseSelect()
        {
            var data = (from emp in context.employeeDetails
                        select new
                        {
                            emp.Name,
                            emp.Salary
                        }).ToList();

            return Ok(data);
        }
        // use of Contain 
        [HttpGet("UseContains")]
        public IActionResult UseContains()
        {
            var data = context.employeeDetails
                            .Where(emp => emp.Name.Contains("e"))
                            .ToList();

            return Ok(data);
        }





    }
}
      
    

