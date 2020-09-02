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

		private void OnComponentRemoved(ComponentRemovedEvent e)
		{
			if (EntityHasAllComponents(e.Entity, _componentTypes) == false) Entities.Remove(e.Entity);
		}

		private bool EntityHasAllComponents(Entity entity, List<Type> componentTypes) => entity.Contains(componentTypes);

		public void AddFilter<T>() => _componentTypes.Add(typeof(T));

		private void OnComponentAdded(ComponentAddedEvent e)
		{
			if(EntityHasAllComponents(e.Entity, _componentTypes)) Entities.Add(e.Entity);
		}
	}
}