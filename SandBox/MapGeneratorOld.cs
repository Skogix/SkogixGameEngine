#region
using System;
using System.Text;
using System.Threading;
#endregion

namespace ECS {
	// public class Map {
	// 	private bool isTrue = true;
	// 	public static bool operator ==(Map a, bool b) => a != null && a.isTrue == b;
	// 	public static bool operator !=(Map a, bool b) => !(a == b);
	// }
	public class MapGeneratorOld {
		public MapGeneratorOld(int width, int height, int spawnChance, char trueGlyph, char falseGlyph) {
			Width = width;
			Height = height;
			SpawnChance = spawnChance;
			TrueGlyph = trueGlyph;
			FalseGlyph = falseGlyph;
		}
		public Random Random => new Random();
		public int NeighbourCount { get;set; }
		public int Width { get;set; }
		public int Height { get;set; }
		public int SpawnChance { get;set; }
		public char TrueGlyph { get;set; }
		public char FalseGlyph { get;set; }
		private void Print(bool[,] map) {
			Console.Clear();
			var i = 0;
			foreach(var b in map) {
				i++;
				if(i % Width == 0) Console.WriteLine();
				if(b) Console.Write('#');
				else Console.Write('.');
			}
		}
		public bool[,] SmoothOutNoise(bool[,] map) {
			var agent = (bool[,])map.Clone();
			// todo flytta eller ändra så det inte blir dubbelt
			for(var x = 0; x < Width; x++) {
				for(var y = 0; y < Height; y++) {
					NeighbourCount = CountNeighbours(agent, x, y);
					map[x, y] = GetCellularAutomata(agent[x, y], NeighbourCount);
				}
			}

			return map;
		}
		public int CountNeighbours(bool[,] agent, int x, int y) {
			var count = 0;
			for(var i = -1; i < 2; i++) {
				for(var j = -1; j < 2; j++) {
					var neighbourX = x + i;
					var neighbourY = y + j;
					if(neighbourX < 0 || neighbourY < 0 || neighbourX >= Width || neighbourY >= Height) count = count + 1;
					else if(agent[x, y]) count++;
				}
			}

			return count;
		}
		public bool GetCellularAutomata(bool tileState, int neighbourCount) => tileState switch {
			true when neighbourCount > 3   => true,
			true when neighbourCount < 2   => false,
			false when neighbourCount == 3 => true,
			true when neighbourCount == 3  => true,
			_                              => false,
		};
		public bool[,] FillMapWithRandomGarbage(bool[,] map) {
			for(var x = 0; x < Width; x++) {
				for(var y = 0; y < Height; y++)
					if(x == 0 || x == Width - 1 || y == 0 || y == Height - 1) map[x, y] = true;
					else map[x, y] = Random.Next(1, 101) < SpawnChance;
			}

			return map;
		}
		public string MapToString(bool[,] map) {
			Console.Clear();
			var sb = new StringBuilder();
			sb.Append($"Width: {Width}, \tHeight: {Height}, \t%Walls: {SpawnChance}\n");
			for(var y = 0; y < Height - 1; y++) {
				for(var x = 0; x < Width - 1; x++) //sb.Append(Map[x, y]?'#':'.');
					sb.Append(map[x, y]? '#': '.');
				sb.Append(Environment.NewLine);
			}

			Thread.Sleep(50);
			Console.WriteLine("Gånger: ");
			return sb.ToString();
		}
	}
}