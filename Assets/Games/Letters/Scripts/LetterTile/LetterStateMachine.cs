using System;
using LanguageExt;
using LanguageExt.SomeHelp;
using UniRx;
using UnityEngine;

namespace Tobi.Letters
{
	public class LetterStateMachine
	{
		Option<IState> maybeCurrentState;
		
		public readonly DefaultLetterTileState defaultState;
		public readonly HoveredLetterTileState hoveredState;
		public readonly SelectedLetterTileState selectedState;

		readonly ReactiveProperty<Option<Vector3>> maybeHitPointRx = new();

		IObservable<bool> IsHoveredRx => maybeHitPointRx.Select(maybeHitPoint => maybeHitPoint.IsSome);
		
		public LetterStateMachine(LetterTileView.Model model, Settings settings)
		{
			hoveredState = new(this, model._backing, settings, IsHoveredRx);
			defaultState = new(this, model._backing, IsHoveredRx);
			selectedState = new(this, model._backing, maybeHitPointRx);

			ChangeState(defaultState);
		}

		public void Update(Option<Vector3> maybeHitPoint)
		{
			maybeHitPointRx.Value = maybeHitPoint;
		}

		public void ChangeState(IState newState)
		{
			maybeCurrentState.IfSome(currentState => currentState.OnExit());
			maybeCurrentState = newState.ToSome();
			newState.OnEnter();
		}
	}
}