using System.Collections;

namespace ECS {
	public class Entity {
		private readonly int _gen;
		private readonly int _id;
		protected internal Entity(int id, EntityManager entityManager) {
			_id = id;
			_gen = 0;
			EntityManager = entityManager;
		}
		protected internal EntityManager EntityManager { get; }
		public string GetHash => $"{_id}-{_gen}";
	}
}