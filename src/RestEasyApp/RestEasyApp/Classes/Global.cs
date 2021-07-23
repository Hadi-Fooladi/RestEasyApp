using System;

namespace RestEasyApp
{
	internal static class Global
	{
		// Singleton Pattern
		public static Server Server => LazyServer.Value;
		private static readonly Lazy<Server> LazyServer = new Lazy<Server>(CreateServer);

		private static Server CreateServer() => new Server(4689);
	}
}
