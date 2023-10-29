namespace Game.Letters
{
	[GenConstructor]
	public partial class LetterValue
	{
		public readonly string value;

		public static LetterValue a(char character) => new(character.ToString());
	}
}