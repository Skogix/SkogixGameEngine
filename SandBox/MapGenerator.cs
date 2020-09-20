#region
using System;
#endregion

namespace SandBox {
	public class MapGenerator {
		private readonly int _height;
		private readonly Random _random = new Random();
		private readonly int _spawnChance;
		private readonly int _width;
		public MapGenerator(int width, int height, int spawnChance) {
			_width = width;
			_height = height;
			_spawnChance = spawnChance;
		}
		public bool[,] SmoothMap(bool[,] oldMap) {
			var newMap = new bool[_width, _height];
			for(var x = 0; x < _width; x++) {
				for(var y = 0; y < _height; y++) {
					var aliveNeighbours = CountAliveNeighbours(oldMap, x, y);
					// om en cell lever men har för få grannar, döda
					if(oldMap[x, y]) {
						if(aliveNeighbours < 3) newMap[x, y] = false;
						else newMap[x, y] = true;
					} 
					else {
						// annars om den är död, har den grannar nog för att leva igen?
						if(aliveNeighbours > 4) newMap[x, y] = true;
						else newMap[x, y] = false;
					}
				}
			}

			return newMap;
		}
		private int CountAliveNeighbours(bool[,] map, int x, int y) {
			var count = 0;
			for(var i = -1; i < 2; i++) {
				for(var j = -1; j < 2; j++) {
					var neighbour_x = x + i;
					var neighbour_y = y + j;
					if(i == 0 && j == 0) {
						// om vi kollar 0,0 så är vi på oss själva så gör ingenting
					}
					// vi kollar utanför mappen så räkna som granne
					else if(neighbour_x < 0 || neighbour_y < 0 || neighbour_x >= _width || neighbour_y >= _height) count += 1;
					// annars, kolla bara om grannen är true
					else if(map[neighbour_x, neighbour_y]) count += 1;
				}
			}

			return count;
		}
		public bool[,] RandomMap(bool[,] map) {
			var newMap = new bool[_width, _height];
			for(var x = 0; x < _width; x++) {
				for(var y = 0; y < _height; y++)
					if(x == 0 || y == 0 || x == _width - 1 || y == _height - 1) newMap[x, y] = true;
					else newMap[x, y] = GetRandomBoolBySpawnChance();
			}

			return newMap;
		}
		private bool GetRandomBoolBySpawnChance() => _random.Next(1, 101) < _spawnChance;
	}
}