using System;
using SQLite;

namespace FastBat
{
	public interface ISQLite
	{
		SQLiteConnection GetConnection();
	}
}

