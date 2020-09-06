#region
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
#endregion

namespace ECS {
	public class EntityManager {
		internal static readonly Dictionary<Type, int> ComponentIdByType = new Dictionary<Type, int>();
		internal static readonly List<Type> ComponentTypes = new List<Type>();
		internal readonly Dictionary<Type, Component> ComponentsByType = new Dictionary<Type, Component>();
		
		internal readonly List<Component> AllComponents = new List<Component>();
		internal readonly List<Entity> AllEntities = new List<Entity>();
		internal readonly Dictionary<string, Component> Map = new Dictionary<string, Component>();

		internal World World { get; }
		internal EntityManager(World world) {
			World = world;
			World.EventManager.Subscribe<ComponentRemovedEvent>(this, OnComponentRemoved);
			World.EventManager.Subscribe<ComponentAddedEvent>(this, OnComponentAdded);
			World.EventManager.Subscribe<EntityAddedEvent>(this, OnEntityAdded);
		}
		
		internal bool EntityHasAllComponents(Entity entity, List<Type> componentTypes) {
			return entity.EntityManager.ContainsComponents(componentTypes);
		}
		
		internal Component[] GetAllComponentsOfType<T>() where T : Component => AllComponents.Where(c => c.GetType() == typeof(T)).ToArray();
		internal List<Component> GetAllComponentsOnEntity(Entity entity) {
			var output = new List<Component>();
			foreach (var vp in Map)
				if (vp.Key.StartsWith(entity.GetHash))
					output.Add(vp.Value);

			return output;
		}
		public List<Entity> GetAllEntitiesWithComponent<T>() where T : Component {
			var returnList = new List<Entity>();
			foreach(var entity in AllEntities)
			{
				var component = GetComponent<T>(entity);
				if(component != null) returnList.Add(entity);
			}
			return returnList;
		}	
		
		internal void AddComponent(Entity entity, Component component) {
			var componentType = component.GetType();
			ComponentsByType[componentType] = component;
			World.EventManager.Publish(new ComponentAddedEvent(entity, component));
		}
		internal void DestroyEntity(Entity entity) {
			var removeKeys = new List<string>();
			foreach (var item in Map)
				if (item.Key.StartsWith(entity.GetHash)) {
					removeKeys.Add(item.Key);
					AllComponents.Remove(item.Value);
				}
			foreach (var removeKey in removeKeys) Map.Remove(removeKey);

		}
		internal T GetComponent<T>(Entity e) where T : Component { return e.EntityManager.ComponentsByType[typeof(T)] as T; }
		
		internal void RemoveComponent(Entity e, Component component) => RemoveComponent(e, component.GetType());
		internal void RemoveComponent(Entity entity, Type componentType) {
			var component = ComponentsByType.First(c => c.Key == componentType).Value;
			World.EventManager.Publish(new ComponentRemovedEvent(entity, component));
			ComponentsByType.Remove(componentType);
		}

		private void OnComponentAdded(ComponentAddedEvent e) {
			AllComponents.Add(e.Component);
			Map.Add($"{e.Entity.GetHash}-{e.Component.ToString()}", e.Component);
		}
		private void OnComponentRemoved(ComponentRemovedEvent e) {
			AllComponents.Remove(e.Component);
			Map.Add($"{e.Entity.GetHash}-{e.Component.ToString()}", e.Component);
		}
		private void OnEntityAdded(EntityAddedEvent e) { AllEntities.Add(e.Entity); }
	}
}