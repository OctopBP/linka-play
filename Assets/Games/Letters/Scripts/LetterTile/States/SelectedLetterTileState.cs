using System;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using LanguageExt;
using LanguageExt.SomeHelp;
using Extensions;
using Infrastructure.States;
using UniRx;
using UnityEngine;

namespace Game.Letters
{
	[GenConstructor]
	public partial class SelectedLetterTileState : IState
	{
		private readonly LetterStateMachine stateMachine;
		private readonly LetterTileView letterTile;
		private readonly Settings settings;
		private readonly ReactiveProperty<Option<Vector3>> maybeMousePosition;

		private const float ZOffset = -0.5f;

		[GenConstructorIgnore] private IDisposable mouseFollow;
		[GenConstructorIgnore] private Option<IDisposable> maybeHoldTimer;
		[GenConstructorIgnore] private TweenerCore<Vector3, Vector3, VectorOptions> moveTween;
		
		public UniTask Enter()
		{
			letterTile.RockRenderer.material.color = letterTile.SelectedColor;
			mouseFollow = maybeMousePosition.Collect(position =>
			{
				maybeHoldTimer.IfSome(holdTimer => holdTimer.Dispose());
				maybeHoldTimer = Observable.Timer(settings.timeToSelect)
					.Subscribe(_ => stateMachine.Enter<DefaultLetterTileState>().Forget())
					.ToSome();

				letterTile.transform.localPosition = position.WithZ(ZOffset);
			});

			return default;
		}

		public UniTask Exit()
		{
			mouseFollow.Dispose();
			letterTile.transform.DOLocalMoveZ(0, .2f);
			
			return default;
		}
	}
}