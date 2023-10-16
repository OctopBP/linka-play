using System;
using DG.Tweening;
using LanguageExt;
using LanguageExt.SomeHelp;
using TMPro;
using UniRx;
using UnityEngine;

namespace Tobi.Letters
{
	public partial class LetterTileView : MonoBehaviour
	{
		public enum LetterState
		{
			/// <summary> Default state. Unselected and unhovered. </summary>
			Default,
			/// <summary> We are looking at this tile (with eye tracker) or hovering with mouse. </summary>
			Hovered,
			/// <summary> Letter is selected. We can drag and drop it. </summary>
			Selected
		}
		
		[SerializeField] TMP_Text letterText;
		[SerializeField] Transform selectedView;

		public partial class Model
		{
			[PublicAccessor] readonly LetterTileView backing;
			
			readonly LetterValue letter;
			public readonly ReactiveProperty<LetterState> state = new(LetterState.Default);
			readonly Settings settings = new Settings(timeToSelect: TimeSpan.FromSeconds(1));

			Option<IDisposable> maybeHoverTimer;
			
			public Model(LetterTileView backing, LetterValue letter, Vector3 position)
			{
				this.backing = backing;
				this.letter = letter;
				backing.transform.localPosition = position;
				backing.letterText.SetText(letter.value);

				state.Subscribe(newState =>
				{
					// Dispose hover timer on state change
					maybeHoverTimer.IfSome(t => t.Dispose());
					
					switch (newState)
					{
						case LetterState.Default:
							backing.transform.DOLocalMoveZ(0, .2f);
							backing.selectedView.gameObject.SetActive(false);
							break;
						
						case LetterState.Hovered:
							backing.transform.DOLocalMoveZ(0, .2f);
							backing.selectedView.gameObject.SetActive(true);

							maybeHoverTimer = Observable
								.Timer(settings._timeToSelect)
								.Subscribe(_ => SetState(LetterState.Selected))
								.ToSome();
							break;
						
						case LetterState.Selected:
							backing.selectedView.gameObject.SetActive(true);
							backing.transform.DOLocalMoveZ(-0.5f, .2f); 
							break;
					}
				});
			}

			public void SetState(LetterState state)
			{
				this.state.Value = state;
			}
		}
	}
}
