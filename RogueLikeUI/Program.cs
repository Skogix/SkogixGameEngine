#region
using System;
using RogueLike.Systems;
#endregion

namespace RogueLikeUI {
	internal class Program {
		private static void Main(string[] args) {
			var tileMap = new TileMap();
			var mapHandler = new MapHandler(tileMap, 1,3, 22);
			mapHandler.BlankMap(tileMap);
			tileMap = mapHandler.PrintMap(tileMap);
			//mapHandler.FillMapWithRandomGarbage(tileMap);
			while(true) {
				//Console.ReadKey(true);
				for(var i = 0; i < 10; i++) {
					tileMap = mapHandler.PrintMap(tileMap);
					tileMap = mapHandler.DoSimulationStep(tileMap);
					if(i > 7) mapHandler.PlaceTreasure(tileMap);
				}
				//mapHandler.MakeCaverns(tileMap);
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