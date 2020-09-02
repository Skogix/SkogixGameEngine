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
}