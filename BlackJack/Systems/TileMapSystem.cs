using System.Linq;
using ECS;

namespace CardGame.Systems
{
	public class TileMapSystem : EntitySystem, RunSystem, InitSystem
	{
		public TileMap TileMap;
		public static int Height;
		public static int Width;
		
		private Transform GetTileTransform(in int x, in int y) => TileMap.Tiles[x, y].Get<Transform>();
		private Drawable GetTileDrawable(in int x, in int y) => TileMap.Tiles[x, y].Get<Drawable>();

		public TileMapSystem(int width, int height)
		{
			Height = height;
			Width = width;
		}
		
		public void Run()
		{
		}

		public void Init()
		{
			AddFilter(typeof(Transform));
			AddFilter(typeof(Drawable));
			
			Hub.Sub<ETileMap>(this, OnTileMapEvent);
			
			TileMap = CreateTileMap();
		}

		private void OnTileMapEvent(ETileMap e)
		{
			PrintOnTileMap(TileMap, e.ToString(), e.Transform.X, e.Transform.Y, e.IsPermanent);
		}

		private TileMap CreateTileMap()
		{
			TileMap = new TileMap(Width, Height);
			for (int x = 0; x < Width; x++)
			{
				for (int y = 0; y < Height; y++)
				{
					var tile = new Tile();
					if (x == 0 || x == Width - 1 || y == 0 || y == Height - 1)
					{
						tile.Get<Drawable>().Glyph= '#';
						tile.Get<Drawable>().Permanent = true;
					}

					tile.Get<Transform>().X = x;
					tile.Get<Transform>().Y = y;
					TileMap.Tiles[x, y] = tile;
					
					var drawable = GetTileDrawable(x, y);
					//Send.UiPrint(x,y,drawable.Glyph.ToString(), drawable.Color);
					// ToDo: Uppdatera härifrån
				}
			}

			return TileMap;
		}
		
		private void PrintOnTileMap(TileMap tileMap, string text, int x, int y, bool isPermanent = false)
		{
			var textArray = text.ToArray();
			var startX = x;
			for (int i = 0; i < textArray.Length; i++)
			{
				tileMap.Tiles[i+startX, y].Get<Drawable>().Glyph = textArray[i];
				if (isPermanent) GetTileDrawable(x, y).Permanent = true;
			}
		}
	}

	public class ETileMap : IEvent
	{
		public string Text;
		public Transform Transform;
		public bool IsPermanent = false;

		public ETileMap(Transform transform, string text, bool isPermanent)
		{
			Transform = transform;
			Text = text;
			IsPermanent = isPermanent;
		}
	}
}