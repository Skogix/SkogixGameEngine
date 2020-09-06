#region
using System;
using System.Collections.Generic;
using System.Linq;
using ECS.Interfaces;
#endregion

namespace ECS.Systems {
	public abstract class ComponentSystem : ISystem {
		private readonly List<Type> _filters;
		protected ComponentSystem(World world) {
			_filters = new List<Type>();
			World = world;
		}
		protected IEnumerable<IEnumerable<Component>> Components =>
			_filters.Select(type => World.EntityManager.AllComponents.Where(c => c.GetType() == type));
		private World World { get; }
		protected internal void AddFilter(Type componentType) {
			if (_filters.Contains(componentType) == false) _filters.Add(componentType);
		}
	}
}