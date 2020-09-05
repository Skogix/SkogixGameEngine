using System;
using System.Collections.Generic;
using System.Linq;

namespace ECS {
	public abstract class EntitySystem {
		private readonly List<Type> _filters;
		private protected readonly List<Entity> Entities;
		public World World { get; private set; }
		protected EntitySystem() {
			Entities = new List<Entity>();
			_filters = new List<Type>();
			Hub.Sub<ComponentAddedEvent>(this, OnComponentAdded);
			Hub.Sub<ComponentRemovedEvent>(this, OnComponentRemoved);
		}
		protected EntitySystem(params Type[] componentTypes) : this() { componentTypes.ToList().ForEach(AddFilter); }
		internal void AddFilter<T>() { AddFilter(typeof(T)); }
		private void AddFilter(Type componentType) {
			if (_filters.Contains(componentType) == false) _filters.Add(componentType);
		}
		private void OnComponentRemoved(ComponentRemovedEvent e) {
			if (EntityHasAllComponents(e.Entity, _filters) == false) Entities.Remove(e.Entity);
		}
		private bool EntityHasAllComponents(Entity entity, List<Type> componentTypes) {
			return entity.ContainsComponent(componentTypes);
		}
		private void OnComponentAdded(ComponentAddedEvent e) {
			if (EntityHasAllComponents(e.Entity, _filters) && Entities.Contains(e.Entity) == false)
				Entities.Add(e.Entity);
		}
	}
	public interface IRunSystem {
		public void Run();
	}
	public interface INitSystem {
		public void Init();
	}
}