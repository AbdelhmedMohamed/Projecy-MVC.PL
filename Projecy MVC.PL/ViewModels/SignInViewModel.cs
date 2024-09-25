using System.ComponentModel.DataAnnotations;

namespace Projecy_MVC.PL.ViewModels
{
	public class SignInViewModel
	{
		[Required(ErrorMessage = "Email Is Rrquired")]
		[EmailAddress(ErrorMessage = "InValid Email")]
		public string Email { get; set; }


		[Required(ErrorMessage = "Password Is Rrquired")]
		[DataType(DataType.Password)]
		public string Password { get; set; }

		public bool RememberMe { get; set; }	

	}
}
