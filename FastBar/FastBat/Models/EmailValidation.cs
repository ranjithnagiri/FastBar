﻿using System;
using System.Text.RegularExpressions;

namespace FastBat
{
	public class EmailValidation
	{
		static Regex ValidEmailRegex = CreateValidEmailRegex();

		/// <summary>
		/// Taken from http://haacked.com/archive/2007/08/21/i-knew-how-to-validate-an-email-address-until-i.aspx
		/// </summary>
		/// <returns></returns>
		private static Regex CreateValidEmailRegex()
		{
			string validEmailPattern = @"^(?!\.)(""([^""\r\\]|\\[""\r\\])*""|"
				+ @"([-a-z0-9!#$%&'*+/=?^_`{|}~]|(?<!\.)\.)*)(?<!\.)"
				+ @"@[a-z0-9][\w\.-]*[a-z0-9]\.[a-z][a-z\.]*[a-z]$";

			return new Regex(validEmailPattern, RegexOptions.IgnoreCase);
		}


		public static bool ValidateEmail(string email)
		{
			if (email != null)
			{
				bool isValid = ValidEmailRegex.IsMatch(email);
				return isValid;
			}
			else
				return false;
		}
	}
}
