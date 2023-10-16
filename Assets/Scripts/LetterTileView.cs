using System;
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
			readonly ReactiveProperty<LetterState> state = new(LetterState.Default);
			
			public Model(LetterTileView backing, LetterValue letter, Vector3 position)
			{
				this.backing = backing;
				this.letter = letter;
				backing.transform.localPosition = position;
				backing.letterText.SetText(letter.value);

				state.Subscribe(newState =>
				{
					switch (newState)
					{
						case LetterState.Default:
							backing.selectedView.gameObject.SetActive(false);
							break;
						case LetterState.Hovered:
							backing.selectedView.gameObject.SetActive(true);
							break;
						case LetterState.Selected:
							backing.selectedView.gameObject.SetActive(true);
							break;
						default:
							throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
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
