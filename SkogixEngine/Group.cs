#region
using System.Collections.Generic;
#endregion

namespace ECS {
	internal class Group: EntityManager {
		private readonly SortedDictionary<int, Entity> _entitiesById = new SortedDictionary<int, Entity>();
		internal Group(World world): base(world) {}
	}
}