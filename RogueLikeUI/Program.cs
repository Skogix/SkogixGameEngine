#region
using RogueLike.Systems;
#endregion

namespace RogueLikeUI {
	internal class Program {
		private static void Main(string[] args) {
			var tileMap = new TileMap();
			var mapHandler = new MapHandler(2, 3, 5);
			tileMap = mapHandler.FillMapWithRandomGarbage(tileMap);
			tileMap = mapHandler.PrintMap(tileMap);
			while(true) {
				tileMap = mapHandler.PrintMap(tileMap);
				tileMap = mapHandler.Simulate(tileMap);
			}

			//mapHandler.FillMap2();
			// mapHandler.FillMapWithGarbage();
			// mapHandler.MakeCaverns();
			/*
			 
			var w = new World();
			w.AddSystem(new InputSystem(w));
			w.AddSystem(new MoveSystem(w));
			w.AddSystem(new DrawSystem(w));
			w.InitSystems();
			var skogix = w.CreateEntity(new PlayerTemplate("Skogix"));
			while (true) w.Run();
		*/
		}
	}
}