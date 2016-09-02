using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace FastBat
{
	public partial class FastBatPage : ContentPage
	{
		LoginModel viewModel;
		public FastBatPage()
		{
			InitializeComponent();
			//Binding Login Model
			viewModel = new LoginModel();
			BindingContext = viewModel;
		}

		/// <summary>
		/// Validate the user login 
		/// </summary>
		/// <param name="sender">Sender.</param>
		/// <param name="e">E.</param>
		async void OnLoginClicked(object sender, EventArgs e)
		{
			acInLoading.IsRunning = true;
			btnLogin.IsEnabled = false;
			await LoginProcess();
			acInLoading.IsRunning = false;
			btnLogin.IsEnabled = true;
		}

		/// <summary>
		/// Logins the process.
		/// </summary>
		/// <returns>The process.</returns>
		async Task LoginProcess()
		{
			await Task.Delay(1000);
			if (viewModel.CanLogin())
			{
				try
				{
					//Checking internet connection
					bool internetConnection = await Authentication.CheckForInternetConnection();
					//If internet is working properly, will verify the login details using api
					if (internetConnection)
					{
						//Get the login result details based on user details
						var result = await Authentication.GetToken(viewModel.Email, viewModel.Password);
						string strResult = result.Replace(".issued", "issued");
						result = strResult.Replace(".expires", "expires");
						var tokenResult = (TokenModel)JsonConvert.DeserializeObject(result, typeof(TokenModel));
						if (tokenResult.error == null && tokenResult.error_description == null)
						{
							//Save token details in sqlite database
							App.Database.SaveTokenDetails(tokenResult);

							//Redirect to Event Page
							var homePage = new EventsView(tokenResult.access_token);
							acInLoading.IsRunning = false;
							await this.Navigation.PushModalAsync(homePage);
						}
						else
						{
							//Displaying message for login failure
							acInLoading.IsRunning = false;
							await DisplayAlert(string.Empty, tokenResult.error_description, "OK");
						}
					}
					else {
						await DisplayAlert(string.Empty, "Please check your internet connection", "OK");
					}

				}
				catch (Exception ex)
				{
					await DisplayAlert(string.Empty, "Something went wrong! Please try again", "OK");
				}

			}
			else {
				await DisplayAlert(string.Empty, viewModel.ValidationErrors, "Cancel");
			}
		}
	}
}

