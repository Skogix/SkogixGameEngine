#region
using System;
using ECS;
using ECS.Interfaces;
using ECS.Systems;
#endregion

namespace RogueLike {
	public class MapSystem : EntitySystem, IRunSystem, InitSystem {
		public const int Height = 10;
		public const int Width = 50;
		public MapSystem(World world) : base(world) { World = world; }
		internal World World { get; }
		public Entity[,] Tiles { get; set; }
		public void Init() {
			Tiles = new Entity[Width, Height];
			InitMap();
			Console.Clear();
		}
		public void Run() { }
		private void InitMap() {
			for (var x = 0; x < Width; x++)
			for (var y = 0; y < Height; y++)
				Tiles[x, y] = World.CreateEntity(new Drawable('.'), new Transform(x, y));
		}
	}
}