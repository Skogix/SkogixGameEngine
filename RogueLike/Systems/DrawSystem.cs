#region
using System;
using ECS;
using ECS.Interfaces;
using ECS.Systems;
#endregion

namespace RogueLike {
	public class DrawSystem : EntitySystem, IRunSystem, InitSystem {
		public DrawSystem(World world) : base(world) { }
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
		}
	}
}