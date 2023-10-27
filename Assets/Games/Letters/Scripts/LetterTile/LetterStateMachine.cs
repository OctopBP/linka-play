using System;
using System.Linq;
using LanguageExt;
using LanguageExt.SomeHelp;
using UniRx;
using UnityEngine;

namespace Game.Letters
{
	public class LetterStateMachine
	{
		Option<IState> maybeCurrentState;
		
		public readonly DefaultLetterTileState defaultState;
		public readonly HoveredLetterTileState hoveredState;
		public readonly SelectedLetterTileState selectedState;
		public readonly FinishLetterTileState finishState;

		readonly ReactiveProperty<Option<Vector3>> maybeHitPointRx = new();
		readonly ReactiveProperty<bool> inTargetRx = new();

		IObservable<bool> IsHoveredRx => maybeHitPointRx.Select(maybeHitPoint => maybeHitPoint.IsSome);
		
		public LetterStateMachine(LetterTileView.Model model, Settings settings)
		{
			hoveredState = new(this, model._backing, settings, IsHoveredRx);
			defaultState = new(this, model._backing, IsHoveredRx);
			selectedState = new(this, model._backing, settings, maybeHitPointRx);
			finishState = new(model._backing);

			ChangeState(defaultState);
		}

		public void Update(Option<Vector3> maybeHitPoint, bool inTarget)
		{
			if (inTarget)
			{
				ChangeState(finishState);
			}
			else
			{
				maybeHitPointRx.Value = maybeHitPoint;
			}
		}

		public void ChangeState(IState newState)
		{
			if (maybeCurrentState.Any(currentState => currentState == newState)) return;

			maybeCurrentState.IfSome(currentState => currentState.OnExit());
			maybeCurrentState = newState.ToSome();
			newState.OnEnter();
		}
	}
}