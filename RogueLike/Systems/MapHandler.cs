/*#region
using System;
using System.Collections.Generic;
using System.Text;
using RogueLike.Systems;
using static System.Console;
#endregion
public enum TileType {
	None = ' ', Floor = '.', Wall = '#',
	Treasure,
}

namespace RogueLike.Systems {
	public class Tile {
		public Tile(TileType tileType = TileType.Floor, int x = 0, int y = 0) {
			TileType = tileType;
			X = x;
			Y = y;
		}
		public int X { get; }
		public int Y { get; }
		public TileType TileType { get;set; }
		public ConsoleColor Color { get;set; }
	}
	public class TileMap {
		public TileMap() {
			for(var x = 0; x < Skogix.MapWidth; x++) {
				for(var y = 0; y < Skogix.MapHeight; y++) Tiles[x, y] = new Tile(TileType.Floor, x, y);
			}
		}
		public Tile[,] Tiles { get; } = new Tile[Skogix.MapWidth, Skogix.MapHeight];
	}
}

public static class Skogix {
	public static int MapHeight = 20;
	public static int MapWidth = 20;
}
public class MapHandler {
	public MapHandler(int starvingDeath, int overPopDeath, int chanceToStartAlive = 50) {
		ChanceToStartAlive = chanceToStartAlive;
		StarvingDeath = starvingDeath;
		OverPopDeath = overPopDeath;
	}
	private Random Random { get; } = new Random();
	private int ChanceToStartAlive { get; } // kör tills en procent av mappen är väggar 
	//private TileMap TileMap { get; }
	private int StarvingDeath { get; }
	private int OverPopDeath { get; }
	public TileMap MakeCaverns(TileMap TileMap) {
		for(var y = 0; y <= Skogix.MapHeight - 1; y++)
			for(var x = 0; x <= Skogix.MapWidth - 1; x++)
				TileMap.Tiles[x, y].TileType = PlaceWalls(TileMap.Tiles[x, y]);
		return TileMap;
	}
	public TileType PlaceWalls(Tile tile) {
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
	public TileMap BlankMap(TileMap tileMap) {
		for(var x = 0; x < Skogix.MapWidth; x++)
			for(var y = 0; y < Skogix.MapHeight; y++)
				if(Random.Next(1, 101) < ChanceToStartAlive)
					tileMap.Tiles[x, y] = new Tile(TileType.Wall, x, y);
		return tileMap;
	}
	public void PlaceTreasure(TileMap tileMap) {
		var treasureHiddenLimit = 5;
		for(var x = 0; x < Skogix.MapWidth; x++) {
			for(var y = 0; y < Skogix.MapHeight; y++)
				if(tileMap.Tiles[x, y].TileType != TileType.Wall) {
					var wallCounter = countAliveNeighbours(tileMap, x, y);
					if(wallCounter >= treasureHiddenLimit) tileMap.Tiles[x, y].TileType = TileType.Treasure;
				}
		}
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
	private bool IsOutOfBounds(int x, int y) => x < 0 || y < 0 || x > Skogix.MapWidth - 1 || y > Skogix.MapHeight - 1;
	public TileMap PrintMap(TileMap tileMap) {
		Clear();
		Write(MapToString(tileMap));
		return tileMap;
	}
	private StringBuilder MapToString(TileMap TileMap) {
		var glyphs = new List<char> {'.', '#'};
		var sb = new StringBuilder();
		sb.Append($"Width: {Skogix.MapWidth}, \tHeight: {Skogix.MapHeight}, \t%Walls: {ChanceToStartAlive}\n");
		for(var y = 0; y < Skogix.MapHeight - 1; y++) {
			for(var x = 0; x < Skogix.MapWidth - 1; x++) {
				var glyph = TileMap.Tiles[x, y].TileType switch {
					TileType.Wall     => '#',
					TileType.Floor    => '.',
					TileType.Treasure => 'o',
					TileType.None     => ' ',
					_                 => ' ',
				};
				sb.Append(glyph);
				// todo fixa
			}

			sb.Append("\n");
		}

		return sb;
	}
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
	 #1#
	public TileMap Simulate(TileMap oldMap) {
		var newMap = new TileMap();
		for(var x = 0; x < Skogix.MapWidth; x++) {
			for(var y = 0; y < Skogix.MapHeight; y++) {
				var counter = countAliveNeighbours(oldMap, x, y);
				var newMapTile = newMap.Tiles[x, y];
				var isAlive = newMapTile.TileType == TileType.Wall;
				/*
				if(x == 0 || y == 0 || x == Skogix.MapWidth - 2 || y == Skogix.MapHeight - 2) {
					newMapTile.TileType = TileType.Wall;
					newMapTile.Color = ConsoleColor.Black;
				} else if(counter > 4) newMapTile.TileType = TileType.Wall;
				else if(counter < 1) newMapTile.TileType = TileType.Wall;
				else newMapTile.TileType = TileType.Floor;

				#1#
				if(x == 0 || y == 0 || x == Skogix.MapWidth - 2 || y == Skogix.MapHeight - 2) {
					newMapTile.TileType = TileType.Wall;
					newMapTile.Color = ConsoleColor.Black;
				} else if(isAlive && counter < 3) {
					newMapTile.TileType = TileType.Floor;
					newMapTile.Color = ConsoleColor.White;
				} else if(!isAlive && counter > 5) {
					newMapTile.TileType = TileType.Wall;
					newMapTile.Color = ConsoleColor.Black;
				} else if(isAlive == false && counter == 3) {
					newMapTile.TileType = TileType.Wall;
					newMapTile.Color = ConsoleColor.Red;
				} else if(Random.Next(1, 101) > 50) {
					newMapTile.TileType = TileType.Floor;
					newMapTile.Color = ConsoleColor.Black;
				} else {
					newMapTile.TileType = TileType.Wall;
					newMapTile.Color = ConsoleColor.Black;
				}
			}
		}

		return newMap;
	}
	// public TileMap DoSimulationStep(TileMap TileMap) {
	// 	var newMap = new TileMap();
	// 	// loopa genom hela mappen
	// 	for(var x = 0; x < TileMap.Tiles.GetLength(0); x++) {
	// 		for(var y = 0; y < TileMap.Tiles.GetLength(1); y++) {
	// 			var wallCount = countAliveNeighbours(TileMap, x, y);
	// 			// nya värden är från 0 och följer reglerna
	// 			// om en cell lever men har för några grannar, död 
	// 			if(TileMap.Tiles[x, y].TileType == TileType.Wall) {  												// om du lever
	// 				if(wallCount < OverPopDeath) newMap.Tiles[x, y].TileType = TileType.Floor; // om du har 4+ dö
	// 				else newMap.Tiles[x, y].TileType = TileType.Wall; 												// annars lev vidare
	// 			} else { 																																		// om du är död
	// 				if(wallCount > StarvingDeath) newMap.Tiles[x, y].TileType = TileType.Wall; // lev igen på 2+
	// 				else newMap.Tiles[x, y].TileType = TileType.Floor;  										// annars död vidare
	// 			}
	// 		}
	// 	}
	//
	// 	return newMap;
	// }
	/// <summary>
	///   Ger tillbaka antal celler i en "ring" omkring pointen som lever
	/// </summary>
	public int countAliveNeighbours(TileMap map, int x, int y) {
		var count = 0;
		for(var i = -1; i < 2; i++) {
			for(var j = -1; j < 2; j++) { // 
				var neighbourX = x + i;
				var neighbourY = y + j;
				if(i == 0 && j == 0) {} // kollar i mitten behövs inte
				else if(neighbourX < 0 || neighbourY < 0 || neighbourX >= Skogix.MapWidth || neighbourY >= Skogix.MapHeight) count += 1;
				else if(map.Tiles[neighbourX, neighbourY].TileType == TileType.Wall) count += 1;
			}
		}

		return count;
	}
	public TileMap FillMapWithRandomGarbage(TileMap tileMap) {
		for(var x = 0; x < Skogix.MapWidth; x++) {
			for(var y = 0; y < Skogix.MapHeight; y++)
				if(x == 0 || y == 0 || x == 8 || y == 8) tileMap.Tiles[x, y].TileType = TileType.Wall;
				else {
					if(Random.Next(1, 101) < ChanceToStartAlive) tileMap.Tiles[x, y].TileType = TileType.Wall;
				}
		}

		return tileMap;
	}
	private int RandomPercent(int percent) => percent >= Random.Next(1, 101)? 1: 0;
}*/

