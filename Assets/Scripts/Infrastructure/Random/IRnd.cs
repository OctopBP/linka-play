namespace Infrastructure
{
	public interface IRnd
	{
		float NextFloat();
		double NextDouble();
		bool NextBool();
	}
}