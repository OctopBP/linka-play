using System.Collections.Generic;
using UnityEngine;

namespace Tobi.Letters
{
	public class LetterLevelBootstrap : MonoBehaviour
	{
		[SerializeField] LetterTileView letterTilePrefab;
		[SerializeField] string lettersToSpawn;
		[SerializeField] Transform lettersParent;
		[SerializeField] Bounds placeBounds;

		List<LetterTileView> letters = new List<LetterTileView>();

		// TODO: [Inject]
		IRnd rnd = RandomAdapter.a(seed: 12345);
		
		void Start()
		{
			foreach (var letter in lettersToSpawn)
			{
				var newLetter = Instantiate(letterTilePrefab, lettersParent);
				newLetter.SetLetter(LetterValue.a(letter));
				newLetter.PlaceToRandomPos(rnd, bounds: placeBounds);
				letters.Add(newLetter);
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
