#region
using System;
using System.Collections.Generic;
using System.Text;
using ECS;
using static System.Console;
#endregion
public enum TileType {
	None = ' ', Floor = '.', Wall = '#',
}

namespace RogueLike.Systems {
	/*
	 *	1. If a cell is a wall and less than 3 cells in the Moore neighborhood are walls, the cell changes state to a floor.
   *  2. If a cell is a floor and greater than 4 cells in the Moore neighborhood are walls, the cell changes state to a wall.
   *  3. Otherwise, the cell remains in its current state.
	 *
	 *  1. Any live cell with fewer than two live neighbours dies, as if caused by underpopulation.
   *  2. Any live cell with two or three live neighbours lives on to the next generation.
   *  3. Any live cell with more than three live neighbours dies, as if by overpopulation.
	 *  4. Any dead cell with exactly three live neighbours becomes a live cell, as if by reproduction.

	 *	func (cells, föddaList, överlevandeList, i) {
	 *		loop(i) {
	 *			loop(x < höjd)
	 *				loop(y < bredd)
	 *					if(if edge) alive = true
	 *					else {
	 *						num = adda state av alla celler på utsidan
	 *						state = cells[höjd, bredd]
	 *						alive = (state = 0 && (överlevandeGrannar(i) || state = 1)
	 *					cells[bredd, höjd] = alive ? 1 : 0
	 */
	public class Tile {
		public Tile(TileType tileType, int x, int y) {
			TileType = tileType;
			X = x;
			Y = y;
		}
		public int X { get; }
		public int Y { get; }
		public TileType TileType { get;set; }
	}
	public class TileMap {
		public Tile[,] Tiles { get; }
		public TileMap() {
			
		public void BlankMap(TileMap tileMap) {
			for(var row = 0; row < World.MapWidth; row++)
				for(var column = 0; column < World.MapHeight; column++)
					tileMap.Tiles[column, row].TileType = TileType.None;
		}
		}
	}
	public class MapHandler {
		public MapHandler(TileMap tilemap, int birthLimit, int deathLimit, int percentOfMapWalls = 40) {
			ChanceToStartAlive = percentOfMapWalls;
			BirthLimit = birthLimit;
			DeathLimit = deathLimit;
		}
		private Random Random { get; } = new Random();
		private int ChanceToStartAlive { get; } // kör tills en procent av mappen är väggar 
		private int BirthLimit { get; }
		private int DeathLimit { get; }
		public void MakeCaverns(Tile tile) {
			for(var y = 0; y <= World.MapHeight - 1; y++)
				for(var x = 0; x <= World.MapWidth - 1; x++)
					tile.TileType = PlaceWalls(tile);
		}
		private TileType PlaceWalls(Tile tile) {
			var wallCounter = GetAdjacentWalls(tile, 1, 1);
			switch(tile.TileType) {
				case TileType.Wall when wallCounter >= 4: return TileType.Wall;
				case TileType.Wall when wallCounter < 2:  return TileType.Floor;
				case TileType.Wall:                       break;
				case TileType.None:                       break;
				case TileType.Floor:                      break;
				default:
					if(wallCounter >= 5) return TileType.Wall;
					break;
			}

			return TileType.Floor;
		}
		private int GetAdjacentWalls(Tile tile, int scopeX, int scopeY) {
			var startX = tile.X - scopeX;
			var startY = tile.Y - scopeY;
			var endX = tile.X + scopeX;
			var endY = tile.Y + scopeY;
			var iX = startX;
			var iY = startY;
			var wallCounter = 0;
			for(iY = startY; iY <= endY; iY++)
				for(iX = startX; iX <= endX; iX++)
					if((iX == tile.X && iY == tile.Y) == false)
						if(IsWall(tile, iX, iY))
							wallCounter += 1;
			return wallCounter;
		}
		private bool IsWall(Tile tile, int x, int y) {
			if(IsOutOfBounds(x, y)) return true;
			return tile.TileType switch {
				TileType.Wall  => true,
				TileType.Floor => false,
				TileType.None  => false,
			};
		}
		private bool IsOutOfBounds(int x, int y) => x < 0 || y < 0 || x > World.MapWidth - 1 || y > World.MapHeight - 1;
		public void PrintMap(TileMap tileMap) {
			Clear();
			Write(MapToString(tileMap));
		}
		private StringBuilder MapToString(TileMap tileMap) {
			var glyphs = new List<char> {'.', '#'};
			var sb = new StringBuilder();
			sb.Append($"Width: {World.MapWidth}, \tHeight: {World.MapHeight}, \t%Walls: {ChanceToStartAlive}\n");
			for(var row = 0; row < World.MapHeight; row++) {
				for(var col = 0; col < World.MapWidth; col++) {
					//sb.Append(glyphs[tileMap.Tiles[col, row]]);
					// todo fixa
				}

				sb.Append("\n");
			}

			return sb;
		}
		public TileMap DoSimulationStep(TileMap oldMap) {
			var newMap = new TileMap();
			// loopa genom hela mappen
			for(var x = 0; x < World.MapWidth; x++) {
				for(var y = 0; y < World.MapHeight; y++) {
					var wallCount = countAliveNeighbours(oldMap, x, y);
					// nya värden är från 0 och följer reglerna
					// om en cell lever men har för några grannar, död 
					if(oldMap.Tiles[x, y]
					         .TileType == TileType.Wall) {
						if(wallCount < DeathLimit)
							newMap.Tiles[x, y]
							      .TileType = TileType.Floor;
						else
							newMap.Tiles[x, y]
							      .TileType = TileType.Wall;
					} else { // annars, om den är död ska den respawna?
						if(wallCount > BirthLimit)
							newMap.Tiles[x, y]
							      .TileType = TileType.Wall;
						else
							newMap.Tiles[x, y]
							      .TileType = TileType.Floor;
					}
				}
			}

			return newMap;
		}
		/// <summary>
		///   Ger tillbaka antal celler i en "ring" omkring pointen som lever
		/// </summary>
		public int countAliveNeighbours(TileMap map, int x, int y) {
			var count = 0;
			for(var i = -1; i < 2; i++) {
				for(var j = -1; j < 2; j++) { // 
					var neighbourX = x + i;
					var neighbourY = y + i;
					if(i == 0 && j == 0) {} // kollar i mitten behövs inte

					if(IsOutOfBounds(x, y)) count++; // haxxloopar så behöver inte kolla ens
					else if(map.Tiles[x, y]
					           .TileType == TileType.Wall) count++; // annars en vanlig check
				}
			}

			return count;
		}
		public TileMap FillMapWithRandomGarbage(TileMap tileMap) {
			float chanceToStartAlive = 40;
			for(var x = 0; x < World.MapWidth; x++) {
				for(var y = 0; y < World.MapHeight; y++)
					if(Random.Next(1, 101) < chanceToStartAlive)
						tileMap.Tiles[x, y]
						       .TileType = TileType.Wall;
			}

			return tileMap;
		}
		private int RandomPercent(int percent) => percent >= Random.Next(1, 101)? 1: 0;
	}
}