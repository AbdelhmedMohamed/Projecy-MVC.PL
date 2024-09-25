using System.ComponentModel.DataAnnotations;

namespace Projecy_MVC.PL.ViewModels
{
	public class RestPasswordViewModel
	{
			[Required(ErrorMessage = "Password Is Rrquired")]
		[DataType(DataType.Password)]
		public string	Password { get; set; }

		[Required(ErrorMessage = "Confirm Password Is Rrquired")]
		[Compare(nameof(Password), ErrorMessage = "ConfirmPassword Doesnt match password")]
		[DataType(DataType.Password)]
		public string ConfirmPassword { get; set; }
	}
}
