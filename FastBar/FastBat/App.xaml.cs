using System;
using Xamarin.Forms;

namespace FastBat
{
	public partial class App : Application
	{
		public static FastBatDatabase database;

		public App()
		{
			InitializeComponent();

			//Get token details from sqlite db for validation
			//If DB is null, will redirect to Login Screen (FastBatPage)
			//Other wise validate the token expiry, if token is valid will redirect to events list screen
			var token = App.Database.GetTokenDetails();
			if (token == null)
			{
				MainPage = new FastBatPage();
			}
			else
			{
				//Validate token expiry details
				DateTime issueDate = Convert.ToDateTime(token.issued);
				DateTime currentDate = Convert.ToDateTime(DateTime.UtcNow.ToString("R"));
				double totlaSeconds = (currentDate - issueDate).TotalSeconds;
				if (totlaSeconds > token.expires_in)
				{
					//Delete the existing token
					App.Database.DeleteToken();
					//Delete the events
					App.Database.DeleteEvents();
					//Redirect 
					MainPage = new FastBatPage();
				}
				else
				{
					MainPage = new EventsView(token.access_token);
				}
			}
		}

		public static FastBatDatabase Database
		{
			get
			{
				if (database == null)
				{
					database = new FastBatDatabase();
				}
				return database;
			}
		}

		protected override void OnStart()
		{
			// Handle when your app starts
		}

		protected override void OnSleep()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume()
		{
			// Handle when your app resumes
		}
	}
}

