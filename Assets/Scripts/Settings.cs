using System;

namespace Tobi.Letters
{
	[GenConstructor]
	public partial class Settings
	{
		public readonly TimeSpan timeToSelect;
		public readonly TimeSpan timeToDrop;
	}
}