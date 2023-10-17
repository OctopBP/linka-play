using System;
using LanguageExt;
using TMPro;
using Tobi.Letters.Infrastructure;
using UnityEngine;

namespace Tobi.Letters
{
	public partial class LetterTileView : MonoBehaviour
	{
		[SerializeField] TMP_Text letterText;
		[SerializeField, PublicAccessor] Transform selectedView;

		public partial class Model
		{
			[PublicAccessor] readonly LetterTileView backing;
			readonly LetterStateMachine stateMachine;

			Option<IDisposable> maybeHoverTimer;
			
			public Model(LetterTileView backing, LetterValue letter, Vector3 position, Settings settings, IInput input)
			{
				this.backing = backing;
				this.stateMachine = new(this, settings, input);
				backing.transform.localPosition = position;
				backing.letterText.SetText(letter.value);
			}

			public void Update() => stateMachine.Update();
		}
	}
}
