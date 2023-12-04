using Cysharp.Threading.Tasks;

namespace Game.Letters
{
	[GenConstructor]
	public partial class FinishLetterTileState : ILetterGameState
	{
		private readonly LetterTileView letterTile;

		public UniTask Enter()
		{
			letterTile.RockRenderer.material.color = letterTile.FinishColor;
			return default;
		}

		public UniTask Exit() => default;
	}
}