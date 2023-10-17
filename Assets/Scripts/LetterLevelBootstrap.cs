using System;
using System.Collections.Generic;
using DG.Tweening;
using Infrastructure.Input;
using LanguageExt;
using Tobi.Letters.Infrastructure;
using UniRx;
using UnityEngine;

namespace Tobi.Letters
{
	public class LetterLevelBootstrap : MonoBehaviour
	{
		[SerializeField] LetterTileView letterTilePrefab;
		[SerializeField] string lettersToSpawn;
		[SerializeField] Transform lettersParent;
		[SerializeField] Bounds placeBounds;

		readonly List<LetterTileView.Model> letters = new();
		// Option<LetterTileView.Model> maybeSelectedTile;

		// TODO: [Inject]
		readonly IRnd rnd = RandomAdapter.a(seed: 12345);
		readonly IInput input = new KeyboardInput();
		readonly Settings settings = new(timeToSelect: TimeSpan.FromSeconds(1));
		
		void Start()
		{
			foreach (var letter in lettersToSpawn)
			{
				var newLetter = Instantiate(letterTilePrefab, lettersParent);
				var position = RandomPos(rnd, placeBounds);
				var letterModel = new LetterTileView.Model(
					newLetter, letter: LetterValue.a(letter), position, settings, input
				);

				// letterModel.state
				// 	.Where(state => state == LetterTileView.LetterState.Selected)
				// 	.Subscribe(_ => maybeSelectedTile = letterModel);

				letters.Add(letterModel);
			}
		}

		static Vector3 RandomPos(IRnd rnd, Bounds bounds)
		{
			var offset = bounds.center - bounds.size * 0.5f;
			var x = offset.x + bounds.size.x * rnd.NextFloat() ;
			var y = offset.y + bounds.size.y * rnd.NextFloat();
			return new Vector3(x, y, 0);
		}

		void Update()
		{
			var ray = Camera.main.ScreenPointToRay(input.mousePositionRx.Value);
			if (!Physics.Raycast(ray, out var hit)) return;
			
			foreach (var letter in letters) {
				letter.Update();
			}
		}

		void OnDrawGizmos()
		{
			Gizmos.color = Color.green;
			Gizmos.DrawWireCube(center: placeBounds.center, size: placeBounds.size);
			Gizmos.color = Color.white;
		}
	}
}