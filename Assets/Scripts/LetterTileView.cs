using TMPro;
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

		[PublicAccessor] LetterValue letter;
		[PublicAccessor] LetterState state = LetterState.Default;
		
		public void SetLetter(LetterValue letter)
		{
			this.letter = letter;
			letterText.SetText(letter.value);
		}

		public void PlaceToRandomPos(IRnd rnd, Bounds bounds)
		{
			var offset = bounds.center - bounds.size * 0.5f;
			var x = offset.x + bounds.size.x * rnd.NextFloat() ;
			var y = offset.y + bounds.size.y * rnd.NextFloat();
			transform.localPosition = new Vector3(x, y, 0);
		}
	}
}
