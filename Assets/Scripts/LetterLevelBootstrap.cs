using System;
using System.Collections.Generic;
using Infrastructure.Input;
using Tobi.Letters.Infrastructure;
using UniRx;
using UnityEngine;
using static LanguageExt.Prelude;

namespace Tobi.Letters
{
	public class LetterLevelBootstrap : MonoBehaviour
	{
		[SerializeField] LetterTileView letterTilePrefab;
		[SerializeField] string lettersToSpawn;
		[SerializeField] Transform lettersParent;
		[SerializeField] Bounds placeBounds;

		readonly List<LetterTileView.Model> letters = new();

		// TODO: [Inject]
		readonly IRnd rnd = RandomAdapter.a(seed: 12345);
		readonly IInput input = new KeyboardInput();
		readonly Settings settings = new(timeToSelect: TimeSpan.FromSeconds(1));
		
		void Start()
		{
			foreach (var letter in lettersToSpawn)
			{
				var newLetter = Instantiate(letterTilePrefab, lettersParent);
				var position = RandomPos();
				var letterModel = new LetterTileView.Model(
					newLetter, letter: LetterValue.a(letter), position, settings, input
				);
				letters.Add(letterModel);
			}
			
			input.mousePositionRx.Subscribe(mousePosition =>
			{
				var ray = Camera.main.ScreenPointToRay(mousePosition);
				if (!Physics.Raycast(ray, out var hit)) return;
				
				foreach (var letter in letters)
				{
					var isHovered = hit.transform == letter._backing.transform;
					letter.Update(maybeHitPoint: isHovered ? hit.point : None);
				}
			});
		}

		Vector3 RandomPos()
		{
			var offset = placeBounds.center - placeBounds.size * 0.5f;
			var x = offset.x + placeBounds.size.x * rnd.NextFloat() ;
			var y = offset.y + placeBounds.size.y * rnd.NextFloat();
			return new(x, y, 0);
		}

		void Update()
		{
			// TODO: Move to input service
			input.Update();	
		}

		void OnDrawGizmos()
		{
			Gizmos.color = Color.green;
			Gizmos.DrawWireCube(center: placeBounds.center, size: placeBounds.size);
			Gizmos.color = Color.white;
		}
	}
}