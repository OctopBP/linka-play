namespace Infrastructure.Input
{
	public interface IInputProvider
	{
		IInput GetInput();
		void Update();
	}
}