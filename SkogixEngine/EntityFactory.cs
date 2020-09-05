using System.Collections.Generic;
using System.Linq;

namespace ECS {
	public class EntityFactory {
		private static int _idCount;
		private static int Next() { return _idCount++; }
		private static Entity NewEntity() { return new Entity(Next()); }
		private void AddComponents(Entity entity, List<Component> components) { components.ForEach(entity.Add); }
		private List<Component> CloneComponents(Entity sourceEntity) {
			return sourceEntity._componentsByType.Values.Select(c => c.Clone() as Component).ToList();
		}
		public Entity Get() { return NewEntity(); }
		public Entity Get(Component component) {
			var e = NewEntity();
			e.Add(component);
			return e;
		}
		public Entity Get(Entity sourceEntity) {
			var e = NewEntity();
			CloneComponents(sourceEntity).ForEach(e.Add);
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
	internal static class Extensions { }
}