using System;
namespace MasterProject.ViewModels
{
	public class LoginViewModel
	{
		[EmailAddress]
		[Required(ErrorMessage = "Email is required! ")]
		public string Email { get; set; }

		[Required(ErrorMessage = "Password is required! ")]
		[DataType(dataType: DataType.Password)]
		public string Password { get; set; }
	}
}

