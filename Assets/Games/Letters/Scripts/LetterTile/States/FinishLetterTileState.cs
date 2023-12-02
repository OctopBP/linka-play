using Cysharp.Threading.Tasks;
using Infrastructure.States;

namespace Game.Letters
{
	[GenConstructor]
	public partial class FinishLetterTileState : IState
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