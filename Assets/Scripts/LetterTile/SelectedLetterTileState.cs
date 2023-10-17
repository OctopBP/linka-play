using DG.Tweening;
using Tobi.Letters.Extensions;
using UniRx;
using UnityEngine;

namespace Tobi.Letters
{
	[GenConstructor]
	public partial class SelectedLetterTileState : IState
	{
		readonly LetterTileView letterTile;
		readonly ReactiveProperty<Vector2> mousePosition;
		
		public void OnEnter()
		{
			letterTile.transform.DOLocalMoveZ(-0.5f, .2f);
			mousePosition.Subscribe(pos =>
			{
				var targetPosition = pos.WithZ(letterTile.transform.localPosition.z);
				letterTile.transform.DOLocalMove(targetPosition, 0.5f);
			});
		}

		public void Update() { }

		public void OnExit()
		{
			letterTile.transform.DOLocalMoveZ(0, .2f); 
		}
	}
}