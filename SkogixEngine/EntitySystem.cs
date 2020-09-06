#region
using System;
using System.Collections.Generic;
using System.Linq;
#endregion

namespace ECS {
	public abstract class EntitySystem {
		private readonly List<Type> _filters;
		public readonly List<Entity> Entities;
		protected EntitySystem(World world) {
			Entities = new List<Entity>();
			_filters = new List<Type>();
			World = world;
			World.MessageManager.Subscribe<EngineEvent, ComponentAddedEvent>(this, OnComponentAdded);
			World.MessageManager.Subscribe<EngineEvent, ComponentRemovedEvent>(this, OnComponentRemoved);
		}
		protected EntitySystem(World world, params Type[] componentTypes) : this(world) {
			componentTypes.ToList().ForEach(AddFilter);
		}
		public World World { get; }
		internal void AddFilter<T>() { AddFilter(typeof(T)); }
		protected void AddFilter(Type componentType) {
			if (_filters.Contains(componentType) == false) _filters.Add(componentType);
		}
		private void OnComponentRemoved(EngineEvent engineEvent, ComponentRemovedEvent e) {
			if (EntityHasAllComponents(e.Entity, _filters) == false) Entities.Remove(e.Entity);
		}
		private bool EntityHasAllComponents(Entity entity, List<Type> componentTypes) {
			return entity.ContainsComponent(componentTypes);
		}
		private void OnComponentAdded(EngineEvent engineEvent, ComponentAddedEvent e) {
			if (EntityHasAllComponents(e.Entity, _filters) && Entities.Contains(e.Entity) == false)
				Entities.Add(e.Entity);
		}
	}
}