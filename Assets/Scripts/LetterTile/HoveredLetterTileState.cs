using System;
using UniRx;

namespace Tobi.Letters
{
	public partial class HoveredLetterTileState : IState
	{
		readonly LetterTileView letterTile;
		readonly Settings settings;
		
		[PublicAccessor] IObservable<Unit> hoverTimer;

		public HoveredLetterTileState(LetterTileView letterTile, Settings settings) {
			this.letterTile = letterTile;
			this.settings = settings;
		}
		
		public void OnEnter()
		{
			letterTile._selectedView.gameObject.SetActive(true);
			hoverTimer = Observable.Timer(settings._timeToSelect).AsUnitObservable();
		}

		public void Update() { }

		public void OnExit()
		{
			letterTile._selectedView.gameObject.SetActive(false);
		}
	}
}