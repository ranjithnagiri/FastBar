using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace FastBat
{
	public partial class EventsView : ContentPage
	{
		public ObservableCollection<EventModel> events { get; set; }
		string strToken = string.Empty;

		public EventsView(string token)
		{
			InitializeComponent();
			strToken = token;
			//start activity indicator 
			acInLoading.IsRunning = true;
		}

		//display list view while appearing the view
		protected override async void OnAppearing()
		{
			base.OnAppearing();
			await GetEvents();
			lstEvents.ItemsSource = events;
			//End activity indicator 
			acInLoading.IsRunning = false;
			//Hide activity indicator 
			acInLoading.IsVisible = false;
		}

		//Get event details based on token
		async Task GetEvents()
		{
			await Task.Delay(1000);
			events = new ObservableCollection<EventModel>();
			//Get events list from sqlite database
			var list = App.Database.GetEvents();
			if (list == null )
			{
				//checking internet connection
				bool internetConnection = await Authentication.CheckForInternetConnection();
				if (internetConnection)
				{
					//Get the ecent list based on token
					var result = EventsAccess.GetEvents(strToken).Result;
					var eventList = (IList<EventModel>)JsonConvert.DeserializeObject(result, typeof(IList<EventModel>));

					if (eventList != null)
					{
						foreach (var item in eventList)
						{
							//Save the item in database
							App.Database.SaveEvent(item);
							events.Add(new EventModel() { EventKey = item.EventKey, Name = item.Name, CloudinaryPublicImageId = "icon.png" });
						}
					}
				}
				else
				{
					await DisplayAlert(string.Empty, "Please check your internet connection", "OK");
				}
			}
			else
			{ 
				foreach (var item in list)
				{
					App.Database.SaveEvent(item);
					events.Add(new EventModel() { EventKey = item.EventKey, Name = item.Name, CloudinaryPublicImageId = "icon.png" });
				}
			}
		}

	}
}
