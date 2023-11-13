using LanguageExt;
using UniRx;
using UnityEngine;

namespace Infrastructure.Input
{
	public interface IInput
	{
		/// <summary> Cursor screen position. </summary>
		ReactiveProperty<Vector2> cursorPositionRx { get; }

		/// <summary> Is click button pressed. </summary>
		ReactiveProperty<bool> clickButtonPressedRx { get; }

		/// <summary>
		/// When we look at one point, we start focusing. And after some time we initiate a click.
		/// Here we store the focus progress.
		/// <br/>
		/// It is always <see cref="Option{A}.None"/> for <see cref="KeyboardInput"/>
		/// </summary>
		ReactiveProperty<Option<float>> clickProgressRx { get; }
	}
}