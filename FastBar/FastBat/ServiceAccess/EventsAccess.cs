using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace FastBat
{
	public class EventsAccess
	{
		/// <summary>
		/// Get list of events based on user
		/// </summary>
		/// <returns>The events.</returns>
		/// <param name="tokenValue">Token value.</param>

		public static async Task<string> GetEvents(string tokenValue)
		{
			string strResult = string.Empty;
			try
			{
				using (var client = new HttpClient())
				{
					client.BaseAddress = new Uri("https://fastbar-test.azurewebsites.net/");
					client.DefaultRequestHeaders.Accept.Clear();
					client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", tokenValue);

					var result = client.GetAsync("api/Events?userTypeFilter=Operating").Result;
					strResult = await result.Content.ReadAsStringAsync();
				}
				return strResult;
			}
			catch { return string.Empty; }
		}
	}
}

