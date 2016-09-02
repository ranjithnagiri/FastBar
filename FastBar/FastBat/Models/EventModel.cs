using System;
using SQLite;

namespace FastBat
{
	public class EventModel
	{
		public EventModel()
		{
		}

		[PrimaryKey, AutoIncrement]
		public int Id { get; set; }

		public int EventId { get; set; }
		public string EventKey { get; set; }
		public int BarOperatorUserId { get; set; }
		public string Name { get; set; }
		public string DateTimeStartUtc { get; set; }
		public string DateTimeEndUtc { get; set; }
		public string CloudinaryPublicImageId { get; set; }
	}
}

