#region
using System;
using ECS;
using ECS.Interfaces;
using ECS.Systems;
using RogueLike.Commands;
using RogueLike.Components;
#endregion

namespace RogueLike.Systems {
	public class InputSystem: EntitySystem, IRunSystem {
		public InputSystem(World world): base(world) {
			AddFilter(typeof(Actor));
			AddFilter(typeof(Transform));
		}
		public void Run() {
			foreach(var entity in Entities) {
				var key = Console.ReadKey(true)
				                 .KeyChar;
				var t = entity.Get<Transform>();
				var command = key switch {
					',' => new MoveCommand(t, new Destination(0, -1)),
					'o' => new MoveCommand(t, new Destination(0, +1)),
					'a' => new MoveCommand(t, new Destination(-1, 0)),
					'e' => new MoveCommand(t, new Destination(+1, 0)),
					_   => new MoveCommand(t, new Destination(0, 0)),
				};
				this.Push(command);
				this.Pub(command);
			}
		}
	}
}