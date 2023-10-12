using System;

namespace Tobi.Letters
{
	[GenConstructor]
	public partial class RandomAdapter: IRnd
	{
		readonly Random random;

		public static RandomAdapter a(int seed) => new RandomAdapter(new Random(seed));
		
		public float NextFloat() => (float) NextDouble();
		public double NextDouble() => random.NextDouble();
	}
}