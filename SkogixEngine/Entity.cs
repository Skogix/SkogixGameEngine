using System;
using System.Collections.Generic;
using System.Linq;

namespace ECS
{
	public sealed class Entity
	{
		public readonly Dictionary<Type, Component> _componentsByType;
		private readonly int _gen;
		private readonly int _id;
		private Container _container;

		// --------------- ctor
		private Entity(int id)
		{
			_id = id;
			_gen = 0;
			_container = null;
			_componentsByType = new Dictionary<Type, Component>();
		}

		public Entity(Entity sourceEntity) : this(sourceEntity._componentsByType.Values.Select(c => c.Clone() as Component)){}
		public Entity() : this(IdFactory<Entity>.Next())
		{
		}
		public Entity(IEnumerable<Component> components) : this(IdFactory<Entity>.Next())
		{
			components.ToList().ForEach(Add);
		}
		public Entity(Component component) : this(IdFactory<Entity>.Next())
		{
			Add(component);
		}

		// --------------- api
		public string Hash => $"{_id}-{_gen}";

		public void Add(Component component)
		{
			var componentType = component.GetType();
			var componentId = Skogix.GetComponentId(componentType);
			_componentsByType[componentType] = component;
		}

		public static Entity FromPrototype(Entity prototype) => new Entity(prototype);
		public T Get<T>() where T : Component => _componentsByType[typeof(T)] as T;
		public void Remove(Component component)
		{
			var componentType = component.GetType();
			var componentId = Skogix.GetComponentId(componentType);
			_componentsByType.Remove(componentType);
		}
	}
}