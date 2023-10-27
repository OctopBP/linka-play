using System;
using Extensions;
using UniRx;

namespace Game.Letters
{
	[GenConstructor]
	public partial class DefaultLetterTileState : IState
	{
		readonly LetterStateMachine stateMachine;
		readonly LetterTileView letterTile;
		readonly IObservable<bool> isHoveredRx;

		[GenConstructorIgnore] IDisposable hover;

		public void OnEnter()
		{
			letterTile._rockRenderer.material.color = letterTile._defaultColor;
			hover = isHoveredRx
				.WhereTrue()
				.Subscribe(_ => stateMachine.ChangeState(stateMachine.hoveredState));
		}

		public void OnExit()
		{
			hover.Dispose();
		}
	}
}