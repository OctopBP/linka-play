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
			letterTile._stateText.SetText("Hovered");
			letterTile._selectedView.gameObject.SetActive(true);
			
			hoverTimer = Observable.Timer(settings._timeToSelect)
				.Subscribe(_ => stateMachine.ChangeState(stateMachine.selectedState));
			
			hoverRx = isHoveredRx
				.WhereFalse()
				.Subscribe(_ => stateMachine.ChangeState(stateMachine.defaultState));
		}

		public void OnExit()
		{
			letterTile._selectedView.gameObject.SetActive(false);
			hoverTimer.Dispose();
			hoverRx.Dispose();
		}
	}
}