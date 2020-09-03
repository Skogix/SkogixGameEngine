using System;
using System.Collections.Generic;
using System.Linq;

namespace ECS
{
	public class Entity
	{
		public readonly Dictionary<Type, Component> _componentsByType;
		private readonly int _gen;
		private readonly int _id;

		// --------------- ctor
		public static Entity New() => Skogix.EntityIdFactory.Get();
		private Entity(int id)
		{
			_id = id;
			_gen = 0;
			_componentsByType = new Dictionary<Type, Component>();
		}

		public Entity(Entity sourceEntity) : this(sourceEntity._componentsByType.Values.Select(c => c.Clone() as Component)){}
		public Entity() : this(Skogix.EntityIdFactory.Next()) {}
		public Entity(IEnumerable<Component> components) : this(Skogix.EntityIdFactory.Next()) => components.ToList().ForEach(Add);
		public Entity(Component component) : this(Skogix.EntityIdFactory.Next()) => Add(component);
		public Entity(ITemplate template) : this(template.Components){}
		
		
		public bool Contains(Type componentType) => _componentsByType.ContainsKey(componentType);
		public bool Contains(IEnumerable<Type> componentTypes) => componentTypes.All(_componentsByType.ContainsKey);
		public bool Contains(params Type[] componentTypes) => Contains(componentTypes as IEnumerable<Type>);

		// --------------- api
		public string Hash => $"{_id}-{_gen}";

		public string Info => $"Hash: {Hash} \nComponents ({_componentsByType.Count})\nName: {GetType().Name}";

		public void Add(Component component)
		{
			var componentType = component.GetType();
			var componentId = Skogix.GetComponentId(componentType);
			_componentsByType[componentType] = component;
			Hub.Pub(this, new ComponentAddedEvent(this, component));
		}

		public static Entity FromPrototype(Entity prototype) => new Entity(prototype);
		public static Entity FromTemplate(ITemplate template) => new Entity(template);

		public T Get<T>() where T : Component => _componentsByType[typeof(T)] as T;
		
		public void Remove(Component component) => Remove(component.GetType());
		public void Remove<T>() => Remove(typeof(T));
		public void Remove(Type componentType)
		{
			_componentsByType.Remove(componentType);
			Hub.Pub(this, new ComponentRemovedEvent(this, componentType));
		}

		public void Add(IEnumerable<Component> components) => components.ToList().ForEach(Add);

		public void Add(params Component[] components) => components.ToList().ForEach(Add);
	}
}