#region
using System;
using System.Linq;
using ECS;
using ECS.Interfaces;
using ECS.Systems;
#endregion

namespace RogueLike {
	public class DrawSystem : EntitySystem, IRunSystem, InitSystem {
		public DrawSystem(World world) : base(world) { World = world; }
		public World World { get; }
		public void Init() {
			AddFilter(typeof(Drawable));
			AddFilter(typeof(Transform));
			Console.CursorVisible = false;
		}
		public void Run() {
			foreach (var entity in Entities) {
				var drawable = entity.Get<Drawable>();
				var transform = entity.Get<Transform>();
				Console.SetCursorPosition(transform.X, transform.Y);
				Console.Write(drawable.Glyph);
			}

			var skogix = World.EntityManager.GetAllEntitiesWithComponent<Actor>().First();
			var t = skogix.Get<Transform>();
			Console.SetCursorPosition(t.X, t.Y);
			Console.Write(skogix.Get<Drawable>().Glyph);
		}
	}
}