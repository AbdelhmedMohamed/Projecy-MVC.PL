﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectMVC.DAL.Models
{
    //Model
    public class Department :ModelBase
    {


        [Required(ErrorMessage ="Code is required! ")]
        public string Code { get; set; }

        [Required(ErrorMessage = "Name is required! ")]
        public string Name { get; set; }

        [Display(Name ="Date Of Creation")]
        public DateTime DataOfCreation { get; set; }

        //navigation property [many]

        public ICollection<Employee> employees { get; set; } = new HashSet<Employee>();



    }
}
