using System;
using Cysharp.Threading.Tasks;
using Extensions;
using Infrastructure.States;
using UniRx;

namespace Game.Letters
{
	[GenConstructor]
	public partial class HoveredLetterTileState : IState
	{
		private readonly LetterStateMachine stateMachine;
		private readonly LetterTileView letterTile;
		private readonly Settings settings;
		private readonly IObservable<bool> isHoveredRx;

		[GenConstructorIgnore] private IDisposable hoverTimer, hoverRx;
		
		public UniTask Enter()
		{
			letterTile._rockRenderer.material.color = letterTile._hoveredColor;
			
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