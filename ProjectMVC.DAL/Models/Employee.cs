﻿using System;
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

    public class Employee :ModelBase
    {
        

        [Required(ErrorMessage = "Name Is Required")]
        [MaxLength(50,ErrorMessage = "Max Length For Namr Is 50")]
        [MinLength(4,ErrorMessage = "Max Length For Namr Is 4")]

        public string Name { get; set; }

        [Range(21,60)]

        public int? Age {  get; set; }
        [RegularExpression(@"^[0-9]{1,3}-[a-zA-Z]{5,10}-[a-zA-Z]{4,10}-[a-zA-Z]{5,10}$"
         , ErrorMessage ="Address Must Be Like 123-street-city-country" )]

        public string Address { get; set; }

        [DataType(DataType.Currency)]
        public decimal Salary {  get; set; }

        public bool IsActive { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [Phone]
        [Display(Name ="phone Number")]

        public string PhoneNumber { get; set; }

        [Display(Name ="Hire Date")]

        public DateTime HireDate { get; set; }

        public bool IsDeleted {  get; set; }    

        public Gender Gender { get; set; }


    }
}
