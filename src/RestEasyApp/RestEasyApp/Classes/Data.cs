using EasyTCP;

namespace RestEasyApp
{
	internal class Data
	{
		[EasyTCP(0)]
		public float HR;
		
		[EasyTCP(1)]
		public float RR;

		[EasyTCP(2)]
		public float SPO2;
	}
}
