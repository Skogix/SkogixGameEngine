using System;
using System.Threading;
using ECS;
using RogueLike;
using RogueLike.Components;
using RogueLike.Systems;
using SandBox;

namespace RogueLikeUI {
	internal class Program {
		private static void Main(string[] args) {
			int width = 15;
			int height = 80;
			var mapGenerator = new MapGenerator(width, height, 32);
			var map1 = mapGenerator.RandomMap(new bool[width, height]);
			//PrintBoolArray(map1);
			var map2 = mapGenerator.SmoothMap(map1);
			//PrintBoolArray(map2);
			var map = mapGenerator.SmoothMap(map2);
			//PrintBoolArray(map);
			
			void PrintBoolArray(bool[,] huhu) {
				for(var x = 0; x < width; x++) {
					for(var y = 0; y < height; y++) Console.Write(huhu[x, y]? "#": ".");
					Console.WriteLine();
				}

				Console.WriteLine();
			}


			
			var w = new World();
			w.AddSystem(new TileSystem(w, width, height, map));
			w.AddSystem(new InputSystem(w));
			w.AddSystem(new MoveSystem(w));
			w.AddSystem(new ResolveSystem(w));
			w.AddSystem(new DrawSystem(w));
			w.InitSystems();
			


			var skogix = w.CreateEntity(new PlayerTemplate("Skogix"));
			Console.Clear();
			while(true) {
				w.Run();
			}

		}
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