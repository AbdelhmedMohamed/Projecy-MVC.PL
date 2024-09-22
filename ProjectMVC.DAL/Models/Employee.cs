using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ProjectMVC.DAL.Models
{
   public enum Gender
    {
        [EnumMember(Value ="Male")]
        Male=1 ,
        [EnumMember(Value = "Female")]

        Female = 2
    }

    enum EmployeeType
    {
        FullTime =1 ,   
        PartTime=2  
    }
    //poco class
    public class Employee :ModelBase
    {
        
        public string Name { get; set; }

        public int? Age {  get; set; }      
        public string Address { get; set; }

        public decimal Salary {  get; set; }

        public bool IsActive { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public DateTime HireDate { get; set; }

        public bool IsDeleted {  get; set; }    

        public Gender Gender { get; set; }

        //navigation property [one]
        public Department Department {  get; set; }     

        public int? DepartmentId { get; set; } //Fk colomn
    }
}
