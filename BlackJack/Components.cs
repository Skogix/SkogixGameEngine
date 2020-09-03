using System;
using System.Collections.Generic;
using ECS;

namespace CardGame
{
	public sealed class Hand : Component, ICardContainer
	{
		public List<Entity> Cards { get; set; } = new List<Entity>();
	}

	public sealed class Card : Entity
	{
		public void Print()
		{
			Console.ForegroundColor = Get<Suit>().Color;
			Console.Write(Get<Suit>().Glyph);
			Console.ForegroundColor = ConsoleColor.White;
			Console.Write(Get<Value>().Glyph);
		}
		public Card(Suit suit, Value value)
		{
			Add(suit);
			Add(value);
		}
	}
	public sealed class Suit : Component
	{
		public Suit(string glyph, ConsoleColor color)
		{
			Glyph = glyph;
			Color = color;
		}

		public string Glyph { get; private set; }
		public ConsoleColor Color { get; private set; }
	}
	public sealed class Value : Component
	{
		public Value(string glyph, int value1, int value2 = 0)
		{
			Glyph = glyph;
			Value1 = value1;
			Value2 = value2;
		}

		public string Glyph { get; private set; }
		public int Value1 { get; set; }
		public int Value2 { get; set; }
	}
	public sealed class Deck : Entity
	{
		public List<Card> Cards { get; set; } = new List<Card>();

		public void Print()
		{
			foreach (var entity in Cards)
			{
				entity.Print();
			}
		}
	}

	public sealed class Actor : Component
	{
		public char Action { get; set; }
	}
	public sealed class Input : Component
	{
		public char Key { get; set; }
	}

	public sealed class Transform : Component
	{
		public int X;
		public int Y;

		public Transform(int x = 0, int y = 0)
		{
			X = x;
			Y = y;
		}
	}

	public sealed class Drawable : Component
	{
		public bool Permanent = false;
		public char Glyph = ' ';
		public ConsoleColor Color = ConsoleColor.White;
	}

	public sealed class Sitting : Component
	{
		public Table Table;

		public Sitting(Table table)
		{
			Table = table;
		}
	}

	public class Player : Entity
	{
		public Player()
		{
			Add(new Hand());
			Add(new Actor());
			Add(new Input());
		}
	}
	public class Dealer : Entity
	{
		public Dealer()
		{
			Add(new Hand());
			Add(new Actor());
		}
	}

	public class Table : Entity
	{
		public List<Player> Players { get; set; }
		public Dealer Dealer;
		public Deck Deck;

		public Table(Dealer dealer, Deck deck)
		{
			Dealer = dealer;
			Deck = deck;
		}

	}

	public class Tile : Entity
	{
		public Tile()
		{
			Add(new Drawable());
			Add(new Transform());
		}
	}
	public class TileMap : Entity
	{
		public Tile[,] Tiles;
		public int Width;
		public int Height;
		public TileMap(int width, int height)
		{
			Width = width;
			Height = height;
			Tiles = new Tile[Width,Height];
		}
	}
	
	public interface ICardContainer
	{
		List<Entity> Cards { get; set; }
	}
	public interface ICard
	{
		public Suit Suit { get; set; }
		public Value Value { get; set; }
	}
}