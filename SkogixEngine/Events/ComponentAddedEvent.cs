using System;

namespace ECS {
	public class ComponentAddedEvent : IEvent {
		public Type ComponentType;
		public Entity Entity;
		public ComponentAddedEvent(Entity entity, Component component) {
			Entity = entity;
			ComponentType = component.GetType();
			Description = $"Added {component.ToString()} to {entity.GetHash}";
		}
		public string Description { get; }
	}
}