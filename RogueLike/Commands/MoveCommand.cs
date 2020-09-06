using ECS.Interfaces;
using RogueLike.Components;

namespace RogueLike.Commands {
	public class MoveCommand : ICommand {
		public Transform Transform { get; set; }
		public Destination Destination { get; set; }
		public MoveCommand(Transform transform, Destination destination) {
			Transform = transform;
			Destination = destination;
		}
		public void Execute() {
			Transform.X += Destination.X;
			Transform.X += Destination.Y;
		}
		public bool IsExecuted { get; set; }
	}
}