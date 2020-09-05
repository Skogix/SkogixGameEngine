namespace ECS {
	public class EntityAddedEvent : IEvent {
		public EntityAddedEvent(Entity entity) {
			Entity = entity;
			Description = $"Created {entity.GetHash}";
		}
		public readonly Entity Entity;
		public string Description { get; }
	}
}