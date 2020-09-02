using System;
using System.Collections.Generic;
using System.Drawing;
using ECS;

namespace BlackJack
{
	public enum ESuit
	{
		Hearts,
		Clubs,
		Diamonds,
		Spades,
	}
	public enum ECardValue
	{
		Ace,
		Two,
		Three,
		Four,
		Five,
		Six,
		Seven,
		Eight,
		Nine,
		Ten,
		Jack,
		Queen,
		King,
	}

	public class Suit : Component
	{
		public char Glyph;
		public ESuit ESuit;
		public ConsoleColor Color;

		public Suit(ESuit eSuit, char glyph, ConsoleColor color)
		{
			ESuit = eSuit;
			Glyph = glyph;
			Color = color;
		}

		public string Print => Glyph.ToString();
	}

	public class CardValue : Component
	{
		public char Glyph;
		public ECardValue ECardValue;

		public CardValue(ECardValue eCardValue, char glyph)
		{
			ECardValue = eCardValue;
			Glyph = glyph;
		}

		public string Print => Glyph.ToString();
	}
	public class Card : Component
	{
		public Suit Suit;
		public CardValue CardValue;

		public Card(Suit suit, CardValue cardValue)
		{
			Suit = suit;
			CardValue = cardValue;
		}

		public void Print()
		{
			Console.ForegroundColor = Suit.Color;
			Console.Write($"{Suit.Glyph}{CardValue.Glyph}");
			Console.ForegroundColor = default;
		}
	}
	public class Deck : Component
	{
		internal List<Card> Cards = new List<Card>();
	}
	public class Shoe : Component
	{
		public List<Card> Cards = new List<Card>();
	}
	public class Hand : Component
	{
		public List<Card> Cards = new List<Card>();
	}
	public class Dealer : Component{}
	public class Player : Component{}
	
}