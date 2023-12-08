using System;
using Cysharp.Threading.Tasks;
using Extensions;
using UniRx;

namespace Game.Letters
{
	[GenConstructor]
	public partial class HoveredLetterTileState : ILetterGameState
	{
		private readonly LetterStateMachine stateMachine;
		private readonly LetterTileView letterTile;
		private readonly Settings settings;
		private readonly IObservable<bool> isHoveredRx;

		[GenConstructorIgnore] private IDisposable hoverTimer, hoverRx;
		
		public UniTask Enter()
		{
			letterTile.RockRenderer.material.color = letterTile.HoveredColor;
			
			hoverTimer = Observable.Timer(settings.timeToSelect)
				.Subscribe(_ => stateMachine.Enter<SelectedLetterTileState>().Forget());
			
			hoverRx = isHoveredRx
				.WhereFalse()
				.Subscribe(_ => stateMachine.Enter<DefaultLetterTileState>().Forget());
			
			return default;
		}

		public UniTask Exit()
		{
			hoverTimer.Dispose();
			hoverRx.Dispose();
			return default;
		}
	}
}