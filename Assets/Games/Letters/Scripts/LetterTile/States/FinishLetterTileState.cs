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
			letterTile._rockRenderer.material.color = letterTile._finishColor;
			return default;
		}

		public UniTask Exit() => default;
	}
}