using System;
using System.Collections.Generic;
using System.Linq;

namespace ECS {
	public class Entity {
		public readonly Dictionary<Type, Component> _componentsByType;
		private readonly int _gen;
		private readonly int _id;
		internal Entity(int id) {
			_id = id;
			_gen = 0;
			_componentsByType = new Dictionary<Type, Component>();
		}
		public string Hash => $"{_id}-{_gen}";
		public string Info => $"Hash: {Hash} \nComponents ({_componentsByType.Count})\nName: {GetType().Name}";
		public bool Contains(Type componentType) { return _componentsByType.ContainsKey(componentType); }
		public bool Contains(IEnumerable<Type> componentTypes) {
			return componentTypes.All(_componentsByType.ContainsKey);
		}
		public bool Contains(params Type[] componentTypes) { return Contains(componentTypes as IEnumerable<Type>); }
		public void Add(Component component) {
			var componentType = component.GetType();
			var componentId = Skogix.GetComponentId(componentType);
			_componentsByType[componentType] = component;
			Hub.Pub(this, new ComponentAddedEvent(this, component));
		}
		public T Get<T>() where T : Component { return _componentsByType[typeof(T)] as T; }
		public void Remove(Component component) { Remove(component.GetType()); }
		public void Remove<T>() { Remove(typeof(T)); }
		public void Remove(Type componentType) {
			_componentsByType.Remove(componentType);
			Hub.Pub(this, new ComponentRemovedEvent(this, componentType));
		}
		public void Add(IEnumerable<Component> components) { components.ToList().ForEach(Add); }
		public void Add(params Component[] components) { components.ToList().ForEach(Add); }
	}
}