using System.ComponentModel.DataAnnotations;

namespace Projecy_MVC.PL.ViewModels
{
	public class ForgetPasswordViewModel
	{
		[Required(ErrorMessage = "Email Is Rrquired")]
		[EmailAddress(ErrorMessage = "InValid Email")]
		public string Email { get; set; }



	}
}
