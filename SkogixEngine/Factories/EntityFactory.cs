#region
using System.Collections.Generic;
using System.Linq;
using ECS.Interfaces;
#endregion

namespace ECS.Factories {
	public class EntityFactory {
		private static int _idCount;
		public EntityFactory(World world) { W = world; }
		public World W { get; }
		private static int Next() { return _idCount++; }
		private Entity NewEntity() {
			var e = new Entity(Next(), W.EntityManager);
			W.EventManager.Publish(new EntityAddedEvent(e));
			return e;
		}
		private List<Component> CloneComponents(Entity sourceEntity) {
			return sourceEntity.EntityManager.ComponentsByType.Values.Select(c => c.Clone() as Component).ToList();
		}
		public Entity Get() { return NewEntity(); }
		public Entity Get(Component component) {
			var e = NewEntity();
			e.Add(component);
			return e;
		}
		public Entity Get(Entity sourceEntity) {
			var e = NewEntity();
			foreach (var c in CloneComponents(sourceEntity)) e.Add(c);
			return e;
		}
		public Entity Get(IEnumerable<Component> components) {
			var e = NewEntity();
			components.ToList().ForEach(e.Add);
			return e;
		}
		public Entity Get(ITemplate template) {
			var e = NewEntity();
			template.Components.ToList().ForEach(e.Add);
			return e;
		}
		public Entity Get(params Component[] components) {
			var e = NewEntity();
			components.ToList().ForEach(e.Add);
			return e;
		}
	}
}