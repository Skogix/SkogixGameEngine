using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using ECS;

namespace BlackJack
{
	public sealed class Card : Component
	{
		public void Print()
		{
			Console.ForegroundColor = Suit.Color;
			Console.Write(Suit.Glyph);
			Console.ForegroundColor = ConsoleColor.White;
			Console.Write(Value.Glyph);
		}
		public Suit Suit;
		public Value Value;

		public Card(Suit suit, Value value)
		{
			Suit = suit;
			Value = value;
		}
	}

	public sealed class Suit : Component
	{
		public Suit(string glyph, ConsoleColor color)
		{
			Glyph = glyph;
			Color = color;
		}

		public string Glyph { get; set; }
		public ConsoleColor Color { get; set; }
	}
	public sealed class Value : Component
	{
		public Value(string glyph, int value1, int value2 = 0)
		{
			Glyph = glyph;
			Value1 = value1;
			Value2 = value2;
		}

		public string Glyph { get; set; }
		public int Value1 { get; set; }
		public int Value2 { get; set; }
	}
}