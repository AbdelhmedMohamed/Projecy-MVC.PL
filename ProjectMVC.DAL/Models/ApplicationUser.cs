﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectMVC.DAL.Models
{
	public class ApplicationUser : IdentityUser
	{
		public bool IsAgree { get; set; }

		public string FName { get; set; }
		public string LEmail { get; set; }

	}
}
