using System;
namespace MasterProject.ViewModels
{
	public class RegisterViewModel
	{
		[EmailAddress(ErrorMessage = "enter a valid email address")]
		[Required(ErrorMessage = "email is required")]
		public string Email { get; set; }

		[DataType(dataType:DataType.Password)]
		[Required(ErrorMessage = "enter a valid password")]
		public string Password { get; set; }

		[DataType(dataType: DataType.Password)]
		[Compare("Password",ErrorMessage = "Password don't match")]
		[Required(ErrorMessage = "you must repeat your password")]
		public string RPassword { get; set; }
	}
}

