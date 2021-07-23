using System;
using System.Collections.Generic;
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

		public IEnumerable<Data> AllData => Con.Table<Data>();
	}
}
