using System;

namespace Game.Letters
{
	[GenConstructor]
	public partial class Settings
	{
		public readonly TimeSpan timeToSelect;
		public readonly TimeSpan timeToDrop;
	}
}