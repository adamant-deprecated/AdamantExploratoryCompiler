using System.Collections.ObjectModel;

namespace Adamant.Exploratory.Forge
{
	public class BuiltPackages : KeyedCollection<string, BuiltPackage>
	{
		protected override string GetKeyForItem(BuiltPackage item)
		{
			return item.Name;
		}
	}
}
