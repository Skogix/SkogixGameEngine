#region
using System;
#endregion

namespace SandBox {
	internal class Program {
		public static int width = 20;
		public static int height = 90;
		private static void Main(string[] args) {
			var mapGenerator = new MapGenerator(width, height, 40);
			var map1 = mapGenerator.RandomMap(new bool[width, height]);
			PrintBoolArray(map1);
			var map2 = mapGenerator.SmoothMap(map1);
			PrintBoolArray(map2);
			var map3 = mapGenerator.SmoothMap(map2);
			PrintBoolArray(map3);
			var map4 = mapGenerator.SmoothMap(map3);
			PrintBoolArray(map4);
			var map5 = mapGenerator.SmoothMap(map4);
			PrintBoolArray(map5);
		}
		private static void PrintBoolArray(bool[,] huhu) {
			for(var x = 0; x < width; x++) {
				for(var y = 0; y < height; y++) Console.Write(huhu[x, y]? "#": ".");
				Console.WriteLine();
			}

			Console.WriteLine();
		}
	}
}