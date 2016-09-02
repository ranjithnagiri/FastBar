using System;
using SQLite;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

namespace FastBat
{
	public class FastBatDatabase
	{
		static object locker = new object();

		SQLiteConnection database;


		public FastBatDatabase()
		{
			database = DependencyService.Get<ISQLite>().GetConnection();
			// create the tables
			database.CreateTable<EventModel>();
			database.CreateTable<TokenModel>();
		}

		#region Get Section

		//Get event details
		public IEnumerable<EventModel> GetEvents()
		{
			lock (locker)
			{
				var result = (from i in database.Table<EventModel>() select i).ToList();
				if (result != null && result.Count > 0)
					return result;
				else
					return null;
			}
		}

		//Get token details
		public TokenModel GetTokenDetails()
		{
			lock (locker)
			{
				var result = (from i in database.Table<TokenModel>() select i).ToList();
				if (result != null && result.Count > 0)
					return result[0];
				else
					return null;
			}
		}

		#endregion


		#region Save Details

		//Save event details
		public int SaveEvent(EventModel item)
		{
			lock (locker)
			{
				if (item.Id != 0)
				{
					database.Update(item);
					return item.EventId;
				}
				else {
					return database.Insert(item);
				}
			}
		}

		//Save token details
		public int SaveTokenDetails(TokenModel item)
		{
			lock (locker)
			{
				if (item.Id != 0)
				{
					database.Update(item);
					return item.Id;
				}
				else {
					return database.Insert(item);
				}
			}
		}

		#endregion

		#region Delete section
		//Delete all events
		public void DeleteEvents()
		{
			lock (locker)
			{
				var result = (from i in database.Table<EventModel>() select i).ToList();
				if (result != null && result.Count > 0)
				{
					foreach(var item in result)
						database.Delete<EventModel>(item.Id);
				}
			}
		}

		//Delete token details
		public void DeleteToken()
		{
			lock (locker)
			{
				var result = (from i in database.Table<TokenModel>() select i).ToList();
				if (result != null && result.Count > 0)
				{
					foreach(var item in result)
						database.Delete<TokenModel>(item.Id);
				}
			}
		}

		#endregion

	}
}