#region
using System;
using System.Linq;
using ECS;
using ECS.Interfaces;
using ECS.Systems;
using RogueLike.Components;
using RogueLike.Events;
#endregion

namespace RogueLike.Systems {
	public class DrawSystem : EntitySystem, IRunSystem, InitSystem {
		public DrawSystem(World world) : base(world) { World = world; }
		public World World { get; }
		public void Init() {
			AddFilter(typeof(Drawable));
			AddFilter(typeof(Transform));
			Console.CursorVisible = false;
			this.Sub<MoveEvent>(this, OnMoveEvent);
		}
		private void OnMoveEvent(MoveEvent e) {
			if (e.Destination.X != 0 && e.Destination.Y != 0) {
				Print(e.Transform.X, e.Transform.Y, e.Entity.Get<Drawable>().Glyph);
			} 
		}
		private void Print(in int x, in int y, in char glyph) {
#if RELEASE
			Console.SetCursorPosition(x, y);
			Console.Write(glyph);
#endif
		}
		public void Run() {
			
			foreach (var entity in Entities) {
				/*
				var drawable = entity.Get<Drawable>();
				var transform = entity.Get<Transform>();
				Console.SetCursorPosition(transform.X, transform.Y);
				Console.Write(drawable.Glyph);
			*/
			}

			var skogix = World.EntityManager.GetAllEntitiesWithComponent<Actor>().First();
			var t = skogix.Get<Transform>();
			Console.SetCursorPosition(t.X, t.Y);
			Console.Write(skogix.Get<Drawable>().Glyph);
		}
	}
}