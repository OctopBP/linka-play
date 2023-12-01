using System;
using System.Collections.Generic;
using Infrastructure.Input;
using Extensions;
using Infrastructure;
using UniRx;
using UnityEngine;
using Zenject;
using static LanguageExt.Prelude;

namespace Game.Letters
{
	public class LevelBootstrap : MonoBehaviour
	{
		[SerializeField] private LetterTileView letterTilePrefab;
		[SerializeField] private string lettersToSpawn;
		[SerializeField] private Transform lettersParent;
		[SerializeField] private Bounds placeBounds;
		[SerializeField] private Bounds targetBounds;

		private readonly List<LetterTileView.Model> letters = new();
		private readonly Settings settings = new(timeToSelect: TimeSpan.FromSeconds(1), timeToDrop: TimeSpan.FromSeconds(1));

		private IRnd rnd;
		private IInput input;
		
		[Inject]
		public void Construct(IRnd rnd, IInput input)
		{
			this.rnd = rnd;
			this.input = input;
		}

		private void Start()
		{
			foreach (var letter in lettersToSpawn)
			{
				var newLetter = Instantiate(letterTilePrefab, lettersParent);
				var position = RandomPos();
				var letterModel = new LetterTileView.Model(
					backing: newLetter, letter: LetterValue.a(letter), position, settings, rnd: rnd
				);
				letters.Add(letterModel);
			}
			
			input.cursorPositionRx.Subscribe(mousePosition =>
			{
				var ray = Camera.main.ScreenPointToRay(mousePosition);
				if (!Physics.Raycast(ray, out var hit)) return;
				
				foreach (var letter in letters)
				{
					var letterTransform = letter._backing.transform;
					var isHovered = hit.transform == letterTransform;
					var inTarget = targetBounds.Contains(letterTransform.position.WithZ(targetBounds.center.z));
					letter.Update(
						maybeHitPoint: isHovered ? hit.point : None,
						inTarget: inTarget
					);
				}
			});
		}

		private Vector3 RandomPos()
		{
			var offset = placeBounds.center - placeBounds.size * 0.5f;
			var x = offset.x + placeBounds.size.x * rnd.NextFloat() ;
			var y = offset.y + placeBounds.size.y * rnd.NextFloat();
			return new(x, y, 0);
		}

		private void OnDrawGizmos()
		{
			Gizmos.color = Color.yellow;
			Gizmos.DrawWireCube(center: placeBounds.center, size: placeBounds.size);
			Gizmos.color = Color.green;
			Gizmos.DrawWireCube(center: targetBounds.center, size: targetBounds.size);
			Gizmos.color = Color.white;
		}
	}
}