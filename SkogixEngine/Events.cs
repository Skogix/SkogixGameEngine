#region
using System;
#endregion

namespace ECS {
	internal class ComponentAddedEvent : IEvent {
		internal Type ComponentType;
		internal Entity Entity;
		internal ComponentAddedEvent(Entity entity, Component component) {
			Entity = entity;
			ComponentType = component.GetType();
		}
	}
	internal class ComponentRemovedEvent : IEvent {
		internal Type ComponentType;
		internal Entity Entity;
		internal ComponentRemovedEvent(Entity entity, Type componentType) {
			Entity = entity;
			ComponentType = componentType;
		}
	}
	public interface IEvent { }
}