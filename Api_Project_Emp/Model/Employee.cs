using System.ComponentModel.DataAnnotations;

namespace Api_Project_Emp.Model
{
    public class Employee
    {
        [Key]
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public double Salary { get; set; }

        public int Phone { get; set; }  

    }
}
