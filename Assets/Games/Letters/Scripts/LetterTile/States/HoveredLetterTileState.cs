using System;
using Tobi.Letters.Extensions;
using UniRx;

namespace Tobi.Letters
{
	[GenConstructor]
	public partial class HoveredLetterTileState : IState
	{
		readonly LetterStateMachine stateMachine;
		readonly LetterTileView letterTile;
		readonly Settings settings;
		readonly IObservable<bool> isHoveredRx;

		[GenConstructorIgnore] IDisposable hoverTimer, hoverRx;
		
		public void OnEnter()
		{
			letterTile._rockRenderer.material.color = letterTile._hoveredColor;
			
			hoverTimer = Observable.Timer(settings.timeToSelect)
				.Subscribe(_ => stateMachine.ChangeState(stateMachine.selectedState));
			
			hoverRx = isHoveredRx
				.WhereFalse()
				.Subscribe(_ => stateMachine.ChangeState(stateMachine.defaultState));
		}

		public void OnExit()
		{
			hoverTimer.Dispose();
			hoverRx.Dispose();
		}
	}
}