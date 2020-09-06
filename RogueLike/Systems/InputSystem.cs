#region
using System;
using ECS;
using ECS.Interfaces;
using ECS.Systems;
#endregion

namespace RogueLike {
	public class InputSystem : EntitySystem, IRunSystem {
		public InputSystem(World world) : base(world) {
			AddFilter(typeof(Actor));
			AddFilter(typeof(Transform));
		}
		public void Run() {
			foreach (var entity in Entities) {
				var key = Console.ReadKey().KeyChar;
				var transform = entity.Get<Transform>();
				var destinationComponent = key switch {
					',' => new Destination(transform.X + 0, transform.Y - 1),
					'a' => new Destination(transform.X - 1, transform.Y + 0),
					'e' => new Destination(transform.X + 1, transform.Y + 0),
					'o' => new Destination(transform.X + 0, transform.Y + 1),
					_ => new Destination(transform.X + 0, transform.Y + 0),
				};
				entity.Add(destinationComponent);
			}
		}
	}
	public class MoveCommand : ICommand {
		public MoveCommand(Transform transform, int x, int y) {
			Transform = transform;
			X = x;
			Y = y;
		}
		public Transform Transform { get; set; }
		public int X { get; set; }
		public int Y { get; set; }
		public void Execute() {
			Transform.X += X;
			Transform.Y += Y;
		}
		public bool IsExecuted { get; set; }
	}
}