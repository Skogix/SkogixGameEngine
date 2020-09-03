using System;
using System.Collections.Generic;
using System.Linq;

namespace ECS
{
	public abstract class EntitySystem
	{
		protected readonly List<Entity> Entities;
		private List<Type> _filters;

		public EntitySystem()
		{
			Entities = new List<Entity>();
			_filters = new List<Type>();
			Hub.Sub<ComponentAddedEvent>(this, OnComponentAdded);
			Hub.Sub<ComponentRemovedEvent>(this, OnComponentRemoved);
		}

		public EntitySystem(params Type[] componentTypes) : this() => componentTypes.ToList().ForEach(AddFilter);
		
		public void AddFilter<T>() => AddFilter(typeof(T));

		public void AddFilter(Type componentType)
		{
			if (_filters.Contains(componentType) == false) _filters.Add(componentType);
		}

		private void OnComponentRemoved(ComponentRemovedEvent e)
		{
			if (EntityHasAllComponents(e.Entity, _filters) == false) Entities.Remove(e.Entity);
		}

		private bool EntityHasAllComponents(Entity entity, List<Type> componentTypes) => entity.Contains(componentTypes);


		private void OnComponentAdded(ComponentAddedEvent e)
		{
			if(EntityHasAllComponents(e.Entity, _filters) && Entities.Contains(e.Entity) == false) Entities.Add(e.Entity);
		}
	}

	public interface RunSystem
	{
		public void Run();
	}


	public interface InitSystem
	{
		public void Init();
	}
}