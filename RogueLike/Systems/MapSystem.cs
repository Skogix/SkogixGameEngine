using ECS;
using ECS.Interfaces;
using ECS.Systems;

namespace RogueLike {
	public class MapSystem : EntitySystem, IRunSystem, InitSystem{
		internal World World { get; }
		public Entity[] Tiles { get; set; }
		public const int Height = 50;
		public const int Width = 50;
		
		public MapSystem(World world) : base(world) { World = world; }
		public void Run() {
			
		}
		public void Init() {
			Tiles = new Entity[Height*Width];
			InitMap();
		}
		private void InitMap() {
			for (int x = 0, y = 0; x < Tiles.Length; x++) {
				Tiles[x] = World.CreateEntity(new Drawable('.'), new Transform(x, y));
			}
		}
	}
}