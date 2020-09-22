using ECS;
using ECS.Interfaces;
using ECS.Systems;
using RogueLike.Commands;

namespace RogueLike.Systems {
	public class TileSystem: EntitySystem, IRunSystem, InitSystem {
		public int Width { get;set; }
		public int Height { get;set; }
		public bool[,] Map { get;private set; }
		public World World { get;set; }
		public TileSystem(World world, int width, int height, bool[,] map): base(world) {
			World = world;
			Width = width;
			Height = height;
			Map = map;
		}
		public void Run() {
			
		}
		public void Init() {
			for(int x = 0; x < Width; x++) {
				for(int y = 0; y < Height; y++) {
					if(Map[x, y])
						World.CreateEntity(new WallTile(y, x));
					else
						World.CreateEntity(new FloorTile(y, x));
				}
			}
			
			this.Sub<MoveCommand>();
		}
	}
}