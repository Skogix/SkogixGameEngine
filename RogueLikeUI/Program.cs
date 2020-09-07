#region
using System;
using RogueLike.Systems;
#endregion

namespace RogueLikeUI {
	internal class Program {
		private static void Main(string[] args) {
			var tileMap = new TileMap();
			var mapHandler = new MapHandler(tileMap, 3, 4, 50);
			mapHandler.BlankMap(tileMap);
			mapHandler.FillMapWithRandomGarbage(tileMap);
			while(true) {
				var key = Console.ReadKey(true)
				                 .KeyChar;
				if(key != 's') continue;
				tileMap = mapHandler.DoSimulationStep(tileMap);
				mapHandler.PrintMap(tileMap);
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