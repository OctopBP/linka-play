using System;
using DG.Tweening;
using LanguageExt;
using Tobi.Letters.Extensions;
using UniRx;
using UnityEngine;

namespace Tobi.Letters
{
	[GenConstructor]
	public partial class SelectedLetterTileState : IState
	{
		readonly LetterStateMachine stateMachine;
		readonly LetterTileView letterTile;
		readonly ReactiveProperty<Option<Vector3>> maybeMousePosition;

		const float LerpSpeed = 100;
		const float ZOffset = -0.5f;

		[GenConstructorIgnore] IDisposable mouseFollow;
		
		public void OnEnter()
		{
			letterTile._stateText.SetText("Selected");
			mouseFollow = maybeMousePosition.Subscribe(maybePosition =>
			{
				maybePosition.Match(
					Some: position => letterTile.transform.localPosition = Vector3.Lerp(
						a: letterTile.transform.localPosition,
						b: position.WithZ(ZOffset),
						t: Time.deltaTime * LerpSpeed
					),
					None: () => stateMachine.ChangeState(stateMachine.defaultState)
				);
			});
		}

		public void OnExit()
		{
			mouseFollow.Dispose();
			letterTile.transform.DOLocalMoveZ(0, .2f); 
		}
	}
}