using System;
using System.Collections.Generic;
using System.Diagnostics;
using ECS;

namespace BlackJack
{
	public enum ECardColor
	{
		Hearts,
		Spades,
		Diamonds,
		Clubs,
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
	public static class CardFactory
	{
		public static Suit GetSuitComponent(ECardColor eCardColor)
		{
			switch (eCardColor)
			{
				case ECardColor.Hearts: return new Suit("♥", ConsoleColor.Red);
				case ECardColor.Spades: return new Suit("♠", ConsoleColor.Gray);
				case ECardColor.Diamonds: return new Suit("♦", ConsoleColor.Red);
				case ECardColor.Clubs: return new Suit("♣", ConsoleColor.Gray);
				default:
					throw new ArgumentOutOfRangeException(nameof(eCardColor), eCardColor, null);
			}
		}
		public static Value GetValueComponent(ECardValue eCardValue)
		{
			switch (eCardValue)
			{
				case ECardValue.Ace: return new Value("A", 1, 14);
				case ECardValue.Two: return new Value("2", 2);
				case ECardValue.Three: return new Value("3", 3);
				case ECardValue.Four: return new Value("4", 4);
				case ECardValue.Five: return new Value("5", 5);
				case ECardValue.Six: return new Value("6", 6);
				case ECardValue.Seven: return new Value("7", 7);
				case ECardValue.Eight: return new Value("8", 8);
				case ECardValue.Nine: return new Value("9", 9);
				case ECardValue.Ten: return new Value("10", 10);
				case ECardValue.Jack: return new Value("J", 11);
				case ECardValue.Queen: return new Value("Q", 12);
				case ECardValue.King: return new Value("K", 13);
				default:
					throw new ArgumentOutOfRangeException(nameof(eCardValue), eCardValue, null);
			}
		}
		public static Entity GetCard(ECardColor eCardColor, ECardValue eCardValue)
		{
			var entity = new Entity();
			var card = new Card(GetSuitComponent(eCardColor), GetValueComponent(eCardValue));
			entity.Add(card);
			return entity;
		}

		public static List<Entity> GetDeck()
		{
			var output = new List<Entity>();
			foreach (ECardColor color in Enum.GetValues(typeof(ECardColor)))
			{
				foreach (ECardValue value in Enum.GetValues(typeof(ECardValue)))
				{
					output.Add(GetCard(color, value));
				}
			}
			return output;
		}
	}
}