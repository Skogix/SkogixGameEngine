#region
using System;
using System.Collections.Generic;
using System.Linq;
using ECS.Interfaces;
#endregion

namespace ECS.Systems {
	public abstract class EntitySystem : ISystem {
		private readonly List<Type> _filters;
		protected readonly List<Entity> Entities;
		protected EntitySystem(World world) {
			Entities = new List<Entity>();
			_filters = new List<Type>();
			World = world;
			World.EventManager.Subscribe<ComponentRemovedEvent>(this, OnComponentRemoved);
			World.EventManager.Subscribe<ComponentAddedEvent>(this, OnComponentAdded);
		}
		internal World World { get; }
		protected internal void AddFilter(Type componentType) {
			if (_filters.Contains(componentType) == false) _filters.Add(componentType);
			var huhu = new List<Entity>();
			foreach (var filter in _filters) {
				foreach (var entity in Entities) {
					if (entity.tmpComponents.Exists(c => c.GetType() == filter) == false) {
						huhu.Add(entity);
					} 
				}
			}

			foreach (var entity in huhu) {
				Entities.Remove(entity);
			}
		}
		private void OnComponentRemoved(ComponentRemovedEvent e) {
			if (World.EntityManager.Has(e.Entity, _filters) == false) Entities.Remove(e.Entity);
		}
		private void OnComponentAdded(ComponentAddedEvent e) {
			if (World.EntityManager.Has(e.Entity, _filters) && Entities.Contains(e.Entity) == false)
				Entities.Add(e.Entity);
		}
	}
}