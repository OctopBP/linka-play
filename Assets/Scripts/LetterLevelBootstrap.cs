using System.Collections.Generic;
using Infrastructure.Input;
using Tobi.Letters.Infrastructure;
using UnityEngine;

namespace Tobi.Letters
{
	public class LetterLevelBootstrap : MonoBehaviour
	{
		[SerializeField] LetterTileView letterTilePrefab;
		[SerializeField] string lettersToSpawn;
		[SerializeField] Transform lettersParent;
		[SerializeField] Bounds placeBounds;

		List<LetterTileView.Model> letters = new();

		// TODO: [Inject]
		IRnd rnd = RandomAdapter.a(seed: 12345);
		IInput input = new KeyboardInput();
		
		void Start()
		{
			foreach (var letter in lettersToSpawn)
			{
				var newLetter = Instantiate(letterTilePrefab, lettersParent);
				var position = RandomPos(rnd, placeBounds);
				var letterModel = new LetterTileView.Model(newLetter, letter: LetterValue.a(letter), position);
				letters.Add(letterModel);
			}
		}
		
		public Vector3 RandomPos(IRnd rnd, Bounds bounds)
		{
			var offset = bounds.center - bounds.size * 0.5f;
			var x = offset.x + bounds.size.x * rnd.NextFloat() ;
			var y = offset.y + bounds.size.y * rnd.NextFloat();
			return new Vector3(x, y, 0);
		}

		void Update()
		{
			var ray = Camera.main.ScreenPointToRay(input.mousePosition);
			if (!Physics.Raycast(ray, out var hit)) return;

			foreach (var letter in letters) {
				var state = letter._backing.transform == hit.transform
					? LetterTileView.LetterState.Selected
					: LetterTileView.LetterState.Default;
				letter.SetState(state);
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
