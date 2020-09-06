#region
using System;
using ECS;
using ECS.Interfaces;
using ECS.Systems;
using RogueLike.Components;
#endregion

namespace RogueLike.Systems {
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
}