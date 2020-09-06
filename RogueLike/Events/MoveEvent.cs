using System.ComponentModel.Design;
using ECS;
using ECS.Interfaces;
using RogueLike.Components;

namespace RogueLike.Events {
	public class MoveEvent : IEvent{
		public string Message { get; set; }
		public Transform Transform;
		public Destination Destination;
		public Entity Entity;
		
		public MoveEvent(Entity entity, Transform transform, Destination destination) {
			Entity = entity;
			Transform = transform;
			Destination = destination;
		}
		
	}
}