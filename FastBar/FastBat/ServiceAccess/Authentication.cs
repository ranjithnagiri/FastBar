using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace FastBat
{
	public class Authentication
	{
		public static async Task<string> GetToken(string email, string password)
		{
			/// <summary>
			/// Get token detailas based on user login 
			/// </summary>
			/// <returns>The fast bat value.</returns>

			string strResult = string.Empty;

			using (var client = new HttpClient())
			{
				client.BaseAddress = new Uri("https://fastbar-test.azurewebsites.net/");
				var content = new FormUrlEncodedContent(new[]
				{
					new KeyValuePair<string, string>("grant_type", "password"),
					new KeyValuePair<string, string>("username", email),
					new KeyValuePair<string, string>("password", password)
				});
				var result = client.PostAsync("token", content).Result;
				strResult = await result.Content.ReadAsStringAsync();
			}
			return strResult;
		}

		public static async Task<bool> CheckForInternetConnection()
		{
			try
			{
				bool internetConnection = false;
				using (var client = new HttpClient())
				{
					client.BaseAddress = new Uri("http://www.google.com");
					client.DefaultRequestHeaders.Accept.Clear();
					//client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", tokenValue);

					var result = client.GetAsync("").Result;
					var strResult = await result.Content.ReadAsStringAsync();
					if (!string.IsNullOrEmpty(strResult))
						internetConnection = true;
					else
						internetConnection = false;
				}
				return internetConnection;
			}
			catch
			{
				return false;
			}
		}
	}
}

