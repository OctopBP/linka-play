namespace Tobi.Letters.Infrastructure
{
	public interface IInputProvider
	{
		IInput GetInput();
		void Update();
	}
}