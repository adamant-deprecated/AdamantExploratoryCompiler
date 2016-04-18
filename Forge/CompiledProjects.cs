using System.Collections.ObjectModel;

namespace Adamant.Exploratory.Forge
{
	public class CompiledProjects : KeyedCollection<string, CompiledProject>
	{
		protected override string GetKeyForItem(CompiledProject item)
		{
			return item.Name;
		}
	}
}
