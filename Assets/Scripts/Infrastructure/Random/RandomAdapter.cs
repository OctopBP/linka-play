using System;

namespace Infrastructure
{
	[GenConstructor]
	public partial class RandomAdapter: IRnd
	{
		readonly Random _random;

		public static RandomAdapter a(int seed) => new RandomAdapter(new Random(seed));
		
		public float NextFloat() => (float) NextDouble();
		public double NextDouble() => _random.NextDouble();
	}
}