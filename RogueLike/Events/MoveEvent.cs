#region
using ECS;
using ECS.Interfaces;
using RogueLike.Components;
#endregion

namespace RogueLike.Events {
	public class MoveEvent: IEvent {
		public Destination Destination;
		public Entity Entity;
		public Transform Transform;
		public MoveEvent(Entity entity, Transform transform, Destination destination) {
			Entity = entity;
			Transform = transform;
			Destination = destination;
		}
		public string Message { get;set; }
	}
}