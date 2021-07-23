using System.IO;
using Xamarin.Essentials;

namespace RestEasyApp
{
	using DB;

	internal static class Global
	{
		public static Database Database { get; private set; }

		public static void Init()
		{
			var root = FileSystem.AppDataDirectory;
			var path = Path.Combine(root, "DB.db3");

			Database = new Database(path);
		}
	}
}
