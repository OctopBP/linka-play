using System;
using Tobi.Letters.Extensions;
using UniRx;

namespace Tobi.Letters
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
			letterTile._stateText.SetText("Default");
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