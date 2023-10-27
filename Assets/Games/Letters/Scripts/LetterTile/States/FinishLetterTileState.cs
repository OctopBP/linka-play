namespace Tobi.Letters
{
	[GenConstructor]
	public partial class FinishLetterTileState : IState
	{
		readonly LetterTileView letterTile;

		public void OnEnter()
		{
			letterTile._rockRenderer.material.color = letterTile._finishColor;
		}

		public void OnExit() { }
	}
}