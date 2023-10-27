using System;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using LanguageExt;
using LanguageExt.SomeHelp;
using Extensions;
using UniRx;
using UnityEngine;

namespace Game.Letters
{
	[GenConstructor]
	public partial class SelectedLetterTileState : IState
	{
		readonly LetterStateMachine stateMachine;
		readonly LetterTileView letterTile;
		readonly Settings settings;
		readonly ReactiveProperty<Option<Vector3>> maybeMousePosition;

		const float LerpSpeed = 0.5f;
		const float ZOffset = -0.5f;

		[GenConstructorIgnore] IDisposable mouseFollow;
		[GenConstructorIgnore] Option<IDisposable> maybeHoldTimer;
		[GenConstructorIgnore] TweenerCore<Vector3, Vector3, VectorOptions> moveTween;
		
		public void OnEnter()
		{
			letterTile._rockRenderer.material.color = letterTile._selectedColor;
			mouseFollow = maybeMousePosition.Collect(position =>
			{
				maybeHoldTimer.IfSome(holdTimer => holdTimer.Dispose());
				maybeHoldTimer = Observable.Timer(settings.timeToSelect)
					.Subscribe(_ => stateMachine.ChangeState(stateMachine.defaultState))
					.ToSome();

				letterTile.transform.localPosition = position.WithZ(ZOffset);
			});
		}

		public void OnExit()
		{
			mouseFollow.Dispose();
			letterTile.transform.DOLocalMoveZ(0, .2f); 
		}
	}
}