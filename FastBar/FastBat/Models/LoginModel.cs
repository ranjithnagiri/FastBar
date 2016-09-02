using System;
using System.Threading;
using System.Threading.Tasks;

namespace FastBat
{
	public class LoginModel: ViewModelBase
	{
		public string Email { get; set; }
		public string Password { get; set; }
		public string ValidationErrors { get; private set; }

		public LoginModel()
		{
			Email = "demo@getfastbar.com";//string.Empty;
			Password = "password";//string.Empty;
		}

		public bool CanLogin()
		{
			ValidationErrors = "";
			if (string.IsNullOrEmpty(Email))
			{
				ValidationErrors = "Please enter a email.";
				bool validStatus = EmailValidation.ValidateEmail(Email);
				if(!validStatus)
					ValidationErrors = "Please enter valid email.";
			}
			if (string.IsNullOrEmpty(Password))
			{
				ValidationErrors += "Please enter a password.";
			}
			return (ValidationErrors == "");
		}
	}
}