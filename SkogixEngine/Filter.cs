using System;
using System.Collections.Generic;
using System.Linq;

namespace ECS
{
	public class Filter
	{
		public List<Entity> Entities;
		private List<Type> _componentTypes;

		public Filter()
		{
			Entities = new List<Entity>();
			_componentTypes = new List<Type>();
			Hub.Sub<ComponentAddedEvent>(this, OnComponentAdded);
			Hub.Sub<ComponentRemovedEvent>(this, OnComponentRemoved);
		}

		public Filter(params Type[] componentTypes) : this() => componentTypes.ToList().ForEach(AddFilter);
		
		public void AddFilter<T>() => AddFilter(typeof(T));

		public void AddFilter(Type componentType)
		{
			if (_componentTypes.Contains(componentType) == false) _componentTypes.Add(componentType);
		}

		private void OnComponentRemoved(ComponentRemovedEvent e)
		{
			if (EntityHasAllComponents(e.Entity, _componentTypes) == false) Entities.Remove(e.Entity);
		}

		private bool EntityHasAllComponents(Entity entity, List<Type> componentTypes) => entity.Contains(componentTypes);


		private void OnComponentAdded(ComponentAddedEvent e)
		{
			if(EntityHasAllComponents(e.Entity, _componentTypes) && Entities.Contains(e.Entity) == false) Entities.Add(e.Entity);
		}
	}
}