using System;
using Cysharp.Threading.Tasks;
using Extensions;
using Infrastructure.States;
using UniRx;

namespace Game.Letters
{
	[GenConstructor]
	public partial class DefaultLetterTileState : IState
	{
		private readonly LetterStateMachine stateMachine;
		private readonly LetterTileView letterTile;
		private readonly IObservable<bool> isHoveredRx;

		[GenConstructorIgnore] private IDisposable hover;

		public UniTask Enter()
		{
			letterTile._rockRenderer.material.color = letterTile._defaultColor;
			hover = isHoveredRx
				.WhereTrue()
				.Subscribe(_ => stateMachine.Enter<HoveredLetterTileState>().Forget());
			
			return default;
		}

		public UniTask Exit()
		{
			hover.Dispose();
			
			return default;
		}
	}
}