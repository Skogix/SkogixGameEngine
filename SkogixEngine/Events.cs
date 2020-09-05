using System;

namespace ECS {
	public class ComponentAddedEvent : IEvent {
		public Type ComponentType;
		public Entity Entity;
		public ComponentAddedEvent(Entity entity, Component component) {
			Entity = entity;
			ComponentType = component.GetType();
		}
	}
	public class ComponentRemovedEvent : IEvent {
		public Type ComponentType;
		public Entity Entity;
		public ComponentRemovedEvent(Entity entity, Type componentType) {
			Entity = entity;
			ComponentType = componentType;
		}
	}
	public interface IEvent { }
}