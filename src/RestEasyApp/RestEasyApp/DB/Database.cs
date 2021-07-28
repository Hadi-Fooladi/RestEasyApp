using System;
using System.Collections.Generic;
using System.Linq;
using SQLite;

namespace RestEasyApp.DB
{
	internal class Database : IDisposable
	{
		private readonly SQLiteConnection Con;
		public Database(string path)
		{
			Con = new SQLiteConnection(path);
			Con.CreateTable<Data>();
		}

		public void Dispose() => Con.Dispose();

		public void Add(Data data) => Con.Insert(data);

		public Data GetLatestData => Con.Table<Data>().OrderByDescending(x => x.Date).FirstOrDefault();

		public Data GetLastAlarm => Con.Table<Data>().Where(s => s.Alarm == true).OrderByDescending(x => x.Date).FirstOrDefault();

		public bool DataExists => Con.Table<Data>().Count() != 0;

		public bool AlarmExists => Con.Table<Data>().Where(s => s.Alarm == true).Count() != 0;

		public IEnumerable<Data> AllData => Con.Table<Data>();
	}
}
