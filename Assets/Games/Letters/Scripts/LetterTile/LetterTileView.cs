using LanguageExt;
using TMPro;
using Extensions;
using Infrastructure;
using UnityEngine;

namespace Game.Letters
{
	public partial class LetterTileView : MonoBehaviour
	{
		[SerializeField] TMP_Text letterText;
		[SerializeField, PublicAccessor] Color defaultColor, hoveredColor, selectedColor, finishColor;
		[SerializeField, PublicAccessor] MeshRenderer rockRenderer;

		public partial class Model
		{
			[PublicAccessor] readonly LetterTileView backing;
			readonly LetterStateMachine stateMachine;
			
			public Model(LetterTileView backing, LetterValue letter, Vector3 position, Settings settings, IRnd rnd)
			{
				this.backing = backing;
				stateMachine = new(this, settings);
				backing.transform.localPosition = position;
				backing.letterText.SetText(letter.value);

				backing._rockRenderer.transform.localEulerAngles =
					backing._rockRenderer.transform.localEulerAngles.WithZ(rnd.NextFloat() * 360f);
			}

			public void Update(Option<Vector3> maybeHitPoint, bool inTarget)
			{
				stateMachine.Update(maybeHitPoint: maybeHitPoint, inTarget: inTarget);
			}
		}
	}
}
