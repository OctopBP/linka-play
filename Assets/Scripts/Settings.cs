using System;

namespace Tobi.Letters
{
	[GenConstructor]
	public partial class Settings
	{
		[PublicAccessor] TimeSpan timeToSelect;
	}
}