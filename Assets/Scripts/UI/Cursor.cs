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
		[SerializeField] private Image bg, selectIndicator;
		[SerializeField] private Image effect;
		[SerializeField] private Image corners;
		[SerializeField] private RectTransform pointer;

		private IInput _input;

		private Option<Sequence> _maybeClickSequence;
		
		[Inject]
		private void Construct(IInput input)
		{
			_input = input;
		}

		private void Start()
		{
			// Disable default cursor
			UnityEngine.Cursor.visible = false;
			
			// Prepare all images
			bg.SetActive();
			selectIndicator.SetInactive();
			effect.SetInactive();

			_input.Enabled.Subscribe(gameObject.SetActive);	
			
			// Subscribe to input
			_input.ClickButtonPressedRx.WhereTrue().Subscribe(_ => OnSelect());
			_input.CursorPositionRx.Subscribe(position => pointer.anchoredPosition = position);

			Option<Tween> maybeScaleTween = None;
			_input.ClickProgressRx.Subscribe(maybeProgress => maybeProgress.Match(
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

			_input.MaybeSelectedBounds.Subscribe(maybeBounds => maybeBounds.Match(
				Some: bounds =>
				{
					corners.SetActive(true);
					corners.rectTransform.sizeDelta = bounds.extents;
					corners.rectTransform.anchoredPosition = bounds.center;
				},
				None: () => corners.SetActive(false))
			);
		}

		private void OnSelect()
		{
			_maybeClickSequence.IfSome(clickSequence => clickSequence.Kill());

			var clickSequence = DOTween.Sequence();
			
			effect.transform.localScale = Vector3.one;
			effect.SetAlpha(1);
			effect.SetActive();
			
			const float effectTime = 0.5f;
			clickSequence.Join(effect.transform.DOScale(1.5f, effectTime));
			clickSequence.Join(effect.DOFade(0, effectTime));

			_maybeClickSequence = clickSequence;
		}
	}
}
