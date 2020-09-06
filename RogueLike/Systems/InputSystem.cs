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
				var key = Console.ReadKey(true).KeyChar;
				var transform = entity.Get<Transform>();
				var newMoveCommand = key switch {
					',' => new MoveCommand(transform, 0, -1),
					'a' => new MoveCommand(transform, -1, 0),
					'e' => new MoveCommand(transform, +1, 0),
					'o' => new MoveCommand(transform, 0, +1),
					_ => new MoveCommand(transform, 0, 0),
				};
				entity.Add(newMoveCommand);
			}
		}
	}
	public class MoveCommand : Component, ICommand {
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
	}
}