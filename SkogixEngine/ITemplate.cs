using System.Collections.Generic;

namespace ECS
{
	public interface ITemplate
	{
		string Name { get; }
		IEnumerable<Component> Components();
	}
}