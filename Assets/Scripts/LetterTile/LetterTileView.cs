using LanguageExt;
using TMPro;
using Tobi.Letters.Infrastructure;
using UnityEngine;

namespace Tobi.Letters
{
	public partial class LetterTileView : MonoBehaviour
	{
		[SerializeField] TMP_Text letterText;
		[SerializeField, PublicAccessor] TMP_Text stateText;
		[SerializeField, PublicAccessor] Transform selectedView;

		public partial class Model
		{
			[PublicAccessor] readonly LetterTileView backing;
			readonly LetterStateMachine stateMachine;
			
			public Model(LetterTileView backing, LetterValue letter, Vector3 position, Settings settings, IInput input)
			{
				this.backing = backing;
				stateMachine = new(this, settings);
				backing.transform.localPosition = position;
				backing.letterText.SetText(letter.value);
			}

			public void Update(Option<Vector3> maybeHitPoint)
			{
				stateMachine.Update(maybeHitPoint: maybeHitPoint);
			}
		}
	}
}
