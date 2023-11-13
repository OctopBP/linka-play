using DG.Tweening;
using Extensions;
using Infrastructure.Input;
using LanguageExt;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using static LanguageExt.Prelude;

namespace Core
{
	public class Cursor : MonoBehaviour
	{
		[SerializeField] Image bg, selectIndicator;
		[SerializeField] Image effect;

		[MonoReadonly] RectTransform rect;
		IInput input;

		Option<Sequence> maybeClickSequence;
		
		[Inject]
		void Construct(IInput input)
		{
			this.input = input;
		}
		
		void Start()
		{
			// Disable default cursor
			UnityEngine.Cursor.visible = false;
			
			// Prepare all images
			bg.SetActive();
			selectIndicator.SetInactive();
			effect.SetInactive();

			// Init 
			rect = GetComponent<RectTransform>();
			
			// Subscribe to input
			input.clickButtonPressedRx.WhereTrue().Subscribe(_ => OnSelect());
			input.cursorPositionRx.Subscribe(position => rect.anchoredPosition = position);

			Option<Tween> maybeScaleTween = None;
			input.clickProgressRx.Subscribe(maybeProgress => maybeProgress.Match(
				Some: progress =>
				{
					maybeScaleTween.IfSome(scaleTween => scaleTween.Kill());
					selectIndicator.SetActive();
					selectIndicator.SetAlpha(0.5f);
					selectIndicator.transform.localScale = Vector3.one * progress;
				},
				None: () =>
				{
					maybeScaleTween.IfSome(scaleTween => scaleTween.Kill());
					maybeScaleTween = selectIndicator
						.DOFade(0, 0.1f)
						.OnComplete(selectIndicator.SetInactive);
				}
			));
		}

		void OnSelect()
		{
			maybeClickSequence.IfSome(clickSequence => clickSequence.Kill());

			var clickSequence = DOTween.Sequence();
			
			effect.transform.localScale = Vector3.one;
			effect.SetAlpha(1);
			effect.SetActive();
			
			const float effectTime = 0.5f;
			clickSequence.Join(effect.transform.DOScale(1.5f, effectTime));
			clickSequence.Join(effect.DOFade(0, effectTime));

			maybeClickSequence = clickSequence;
		}
	}
}
