using System;
using System.Collections.Generic;
using System.Linq;

namespace ECS
{
	public sealed class Entity
	{
		private readonly Dictionary<Type, Component> _componentsByType;
		private readonly int _gen;
		private readonly int _id;
		private Container _container;

		private Entity(int id)
		{
			_id = id;
			_gen = 0;
			_container = null;
			_componentsByType = new Dictionary<Type, Component>();
		}

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

		public string Hash => $"{_id}-{_gen}";

		public void Add(Component component)
		{
			var componentType = component.GetType();
			var componentId = Skogix.GetComponentId(componentType);
			_componentsByType[componentType] = component;
		}
	}
}