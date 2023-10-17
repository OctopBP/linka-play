using Tobi.Letters.Infrastructure;
using UniRx;

namespace Tobi.Letters
{
	public class LetterStateMachine
	{
		IState currentState;
		
		readonly HoveredLetterTileState hoveredState; 
		readonly DefaultLetterTileState defaultState;
		readonly SelectedLetterTileState selectedState;

		readonly ReactiveProperty<bool> isHovered = new(false);
		
		public LetterStateMachine(LetterTileView.Model model, Settings settings, IInput input)
		{
			hoveredState = new(this, model._backing, settings, isHovered);
			defaultState = new();
			selectedState = new(model._backing, input.mousePositionRx);

			currentState = defaultState;
		}

		public void Update(bool isHovered)
		{
			this.isHovered.Value = isHovered;
			currentState.Update();
		}

		public void ChangeState(IState newState)
		{
			currentState.OnExit();
			currentState = newState;
			currentState.OnEnter();
		}
	}
}