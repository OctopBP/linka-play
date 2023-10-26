namespace Tobi.Letters
{
	public interface IState
	{
		void OnEnter();
		void OnExit();
	}
}