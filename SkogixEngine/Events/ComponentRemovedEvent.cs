#region
using System;
#endregion

namespace ECS {
	internal class ComponentRemovedEvent : IEvent {
		internal Type ComponentType;
		internal Entity Entity;
		internal ComponentRemovedEvent(Entity entity, Type componentType) {
			Entity = entity;
			ComponentType = componentType;
		}
		public string Description { get; }
	}
}