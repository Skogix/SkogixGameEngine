using System;

namespace ECS
{
	public class ComponentAddedEvent
	{
		public Entity Entity;
		public Type ComponentType;

		public ComponentAddedEvent(Entity entity, Component component)
		{
			Entity = entity;
			ComponentType = component.GetType();
		}
	}
	
	public class ComponentRemovedEvent
	{
		public Entity Entity;
		public Type ComponentType;

		public ComponentRemovedEvent(Entity entity, Type componentType)
		{
			Entity = entity;
			ComponentType = componentType;
		}
	}
}