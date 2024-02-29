using Api_Project_Emp.Data;
using Api_Project_Emp.Migrations;
using Api_Project_Emp.Model;
using Api_Project_Emp.Model.Response;
using Azure.Core.Pipeline;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api_Project_Emp.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly ApplicationDb context;

        public EmployeeController(ApplicationDb context)
        {
            this.context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        // Get All Data

        [HttpGet("GetDATA")]
        public async Task<ActionResult<List<EmployeeResponse>>> GetData()
        {
            var data = await context.employees.ToListAsync();

            
            return Ok(data);
        }


        // only Get FirstName And LastName

        [HttpGet("GetEmployeeNames")]
        public async Task<ActionResult<List<EmployeeResponse>>> GetEmployeeNames()
        {
            var employees = await context.employees.ToListAsync();
            var names = new List<EmployeeResponse>();

            foreach (var employee in employees)
            {
                names.Add(new EmployeeResponse
                {
                    FirstName = employee.FirstName,
                    LastName = employee.LastName

                });
            }

            return Ok(names);
        }

        [HttpGet("GetByNameOnly/{id}")]
        public IActionResult GetByNameOnly(int id)
        {
            var employee = context.employees.FirstOrDefault(x => x.Id == id);

            if (employee == null)
            {
                return NotFound(); 
            }

            var name = employee.FirstName;

            return Ok(name);
        }


        [HttpGet("GetDataById/{id}")]
       public IActionResult GetDataById( int id)
        {
            var data = context.employees.Where(x => x.Id == id).ToList();

            return Ok(data);
        }
        [HttpPost("PostData")]
        public IActionResult PostData(Employee employee) 
        {
            Employee employee2 = new Employee();
           
              
                employee2.FirstName = employee.FirstName;
                employee2.LastName = employee.LastName;
                employee2.Salary = employee.Salary;
                employee2.Phone = employee.Phone;
                context.employees.Add(employee2);
                context.SaveChanges();
                return Ok(employee2);
          }

        // Update data using put
        [HttpPut("PutData")]
        public IActionResult PutData(Employee employee ,int Id)
        {
            var data = context.employees.FirstOrDefault(employee=>employee.Id == Id);
            if (data == null)
            {
                return NotFound();
            }

            data.FirstName = employee.FirstName;
            data.LastName = employee.LastName;
            data.Salary = employee.Salary;
            data.Phone = employee.Phone;    
            context.employees.Update(data);
            context.SaveChanges();
            return Ok();

        }

        [HttpPatch("PatchData/{id}")]
        public IActionResult PatchData(Employee employee ,int id) 
        {
            var data = context.employees.FirstOrDefault(employee => employee.Id == id);
            if (data == null)
            {
                return NotFound();
            }
            if (employee.FirstName != null)
            {
                data.FirstName = employee.FirstName;
            }
            if (employee.LastName != null)
            {
                data.LastName = employee.LastName;
            }
            if (employee.Salary != null)
            {
                data.Salary = employee.Salary;
            }
            if (employee.Phone != null)
            {
                data.Phone = employee.Phone;
            }

            context.SaveChanges();

            return NoContent();



        }
        [HttpDelete("DeleteData/{id}")]
        public IActionResult DeleteData(int id) 
        {
            var demo = context.employees.FirstOrDefault(x => x.Id == id);
            if (demo == null)
            {
                return NotFound();
            }
           
            
                context.employees.Remove(demo);
                context.SaveChanges();
                return NoContent();
            

        }

        // Multiple id Delete
        [HttpDelete("DeleteMultiple")]
        public IActionResult DeleteMultiple(int[] id)
        {
            var data = context.employees.Where(x => id.Contains(x.Id)).ToList();
            if (data == null || data.Count == 0)
            {
                return NotFound();
            }
            context.employees.RemoveRange(data); 
            context.SaveChanges();
            return NoContent();
        }
        [HttpGet("GetSalary")]
        public IActionResult GetSalary(int id) 
        {
            var data = context.employees.FirstOrDefault(x =>x.Id==id);
            if(data == null)
            {
                return NotFound();
            }
            var demo = data.Salary;

            return Ok(demo);

        }

        [HttpGet("GetSalaryByName")]
        public IActionResult GetSalaryByName(string name)
        {
            var data = context.employees.FirstOrDefault( x =>x.FirstName == name);
            if(data == null)
            {
                return BadRequest();
            }
            var names = data.Salary;
            return Ok(names);
        }



    }
}
