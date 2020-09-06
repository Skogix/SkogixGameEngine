#region
using System.Collections.Generic;
#endregion

namespace ECS {
	public class Entity {
		private readonly int _gen;
		private readonly int _id;
		public List<Component> tmpComponents;
		protected internal Entity(int id, EntityManager entityManager) {
			_id = id;
			_gen = 0;
			EntityManager = entityManager;
			tmpComponents = EntityManager.GetAllComponentsOnEntity(this);
		}
		protected internal EntityManager EntityManager { get; }
		public string GetHash => $"{_id}-{_gen}";
	}
}