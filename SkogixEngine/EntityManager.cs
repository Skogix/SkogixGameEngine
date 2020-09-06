#region
using System;
using System.Collections.Generic;
using System.Linq;
#endregion

namespace ECS {
	public class EntityManager {
		public readonly List<Component> AllComponents = new List<Component>();
		private readonly List<Entity> AllEntities = new List<Entity>();
		public readonly Dictionary<string, Component> Map = new Dictionary<string, Component>();
		public readonly List<Tuple<Entity, Component>> Tuples = new List<Tuple<Entity, Component>>();
		public EntityManager(World world) {
			World = world;
			World.EventManager.Subscribe<ComponentRemovedEvent>(this, OnComponentRemoved);
			World.EventManager.Subscribe<ComponentAddedEvent>(this, OnComponentAdded);
			World.EventManager.Subscribe<EntityAddedEvent>(this, OnEntityAdded);
		}
		public World World { get; }
		public string EntityComponentKey(Entity e, Type ct) { return $"{e.GetHash}-{ct.Name}"; }
		//public T Get<T>(Entity e) where T : Component => GetAllComponentsOnEntity(e).FirstOrDefault(c => c.GetType() == typeof(T)) as T;
		public T Get<T>(Entity e) where T : Component {
			return e.tmpComponents.Find(c => c.GetType() == typeof(T)) as T;
		}
		public List<Entity> GetAllEntitiesWithComponent<T>() where T : Component {
			var output = new List<Entity>();
			Tuples.Where(t => t.Item2.GetType() == typeof(T)).ToList().ForEach(t => output.Add(t.Item1));
			return output;
		}
		public void AddComponent(Entity e, Component c) { World.EventManager.Publish(new ComponentAddedEvent(e, c)); }
		public void RemoveComponent(Entity e, Component c) {
			World.EventManager.Publish(new ComponentRemovedEvent(e, c));
		}
		public List<Component> GetAllComponentsOnEntity(Entity e) {
			var output = new List<Component>();
			foreach (var vp in Map)
				if (vp.Key.StartsWith(e.GetHash))
					output.Add(vp.Value);
			return output;
		}
		public bool Has<T>(Entity e) { return Map.ContainsKey(EntityComponentKey(e, typeof(T))); }
		public bool Has(Entity e, IEnumerable<Type> ts) {
			foreach (var type in ts)
				if (Map.ContainsKey(EntityComponentKey(e, type)) == false)
					return false;
			return true;
		}
		public Component[] GetAllComponentsOfType<T>() where T : Component {
			return AllComponents.Where(c => c.GetType() == typeof(T)).ToArray();
		}
		/*
		internal void DestroyEntity(Entity entity) {
			var removeKeys = new List<string>();
			foreach (var item in Map)
				if (item.Key.StartsWith(entity.GetHash)) {
					removeKeys.Add(item.Key);
					AllComponents.Remove(item.Value);
				}
			foreach (var removeKey in removeKeys) Map.Remove(removeKey);
		}
		*/
		private void OnComponentAdded(ComponentAddedEvent e) {
			AllComponents.Add(e.Component);
			Tuples.Add(new Tuple<Entity, Component>(e.Entity, e.Component));
			Map.Add(EntityComponentKey(e.Entity, e.Component.GetType()), e.Component);
			e.Entity.tmpComponents.Add(e.Component);
		}
		private void OnComponentRemoved(ComponentRemovedEvent e) {
			AllComponents.Remove(e.Component);
			Tuples.Remove(new Tuple<Entity, Component>(e.Entity, e.Component));
			Map.Remove(EntityComponentKey(e.Entity, e.Component.GetType()));
			e.Entity.tmpComponents.Remove(e.Component);
		}
		private void OnEntityAdded(EntityAddedEvent e) { AllEntities.Add(e.Entity); }
	}
}