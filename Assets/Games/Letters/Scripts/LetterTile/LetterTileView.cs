using Cysharp.Threading.Tasks;
using TMPro;
using Extensions;
using Infrastructure;
using LanguageExt;
using UniRx;
using UnityEngine;

namespace Game.Letters
{
	public partial class LetterTileView : MonoBehaviour
	{
		[SerializeField] private TMP_Text letterText;
		[SerializeField, PublicAccessor] private Color defaultColor, hoveredColor, selectedColor, finishColor;
		[SerializeField, PublicAccessor] private MeshRenderer rockRenderer;

		public partial class Model
		{
			[PublicAccessor] private readonly LetterTileView backing;
			private readonly ReactiveProperty<bool> isHoveredRx = new();
			private readonly ReactiveProperty<Option<Vector3>> maybeHitPointRx = new();
			
			public Model(LetterTileView backing, LetterValue letter, Vector3 position, Settings settings, IRnd rnd)
			{
				this.backing = backing;

				SetupStateMachine(backing, settings);

				backing.transform.localPosition = position;
				backing.letterText.SetText(letter.value);

				backing._rockRenderer.transform.localEulerAngles =
					backing._rockRenderer.transform.localEulerAngles.WithZ(rnd.NextFloat() * 360f);
			}

			private void SetupStateMachine(LetterTileView backing, Settings settings)
			{
				var stateMachine = new LetterStateMachine();
				stateMachine.RegisterState(new DefaultLetterTileState(stateMachine, backing, isHoveredRx));
				stateMachine.RegisterState(new HoveredLetterTileState(stateMachine, backing, settings, isHoveredRx));
				stateMachine.RegisterState(new DefaultLetterTileState(stateMachine, backing, isHoveredRx));
				stateMachine.RegisterState(new SelectedLetterTileState(stateMachine, backing, settings, maybeHitPointRx));
				stateMachine.RegisterState(new FinishLetterTileState(backing));
				stateMachine.Enter<DefaultLetterTileState>().Forget();
			}

			public void Update(Option<Vector3> maybeHitPoint, bool inTarget)
			{
				isHoveredRx.Value = inTarget;
				maybeHitPointRx.Value = maybeHitPoint;
			}
		}
	}
}
