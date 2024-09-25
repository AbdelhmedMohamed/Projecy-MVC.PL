using System.ComponentModel.DataAnnotations;

namespace Projecy_MVC.PL.ViewModels
{
	public class SignUpViewModel
	{
		[Required(ErrorMessage = "FName Is Rrquired")]
		public string FName { get; set; }

		[Required(ErrorMessage = "LName Is Rrquired")]
		public string LEmail { get; set; }

		[Required(ErrorMessage ="Email Is Rrquired")]
		[EmailAddress(ErrorMessage ="InValid Email")]
        public string Email { get; set; }

		[Required(ErrorMessage = "Password Is Rrquired")]
		[DataType(DataType.Password)]
		public string	Password { get; set; }

		[Required(ErrorMessage = "Confirm Password Is Rrquired")]
		[Compare(nameof(Password),ErrorMessage = "ConfirmPassword Doesnt match password")]
		[DataType(DataType.Password)]
		public string ConfirmPassword { get; set; }

		[Required(ErrorMessage = "Rrquired To Agree")]

		public bool IsAgree { get; set; }

	}
}
