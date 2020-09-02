using System;
using System.Drawing;
using System.Linq;

namespace BlackJack
{
	internal static class CardFactory
	{
		public static Suit GetSuit(ESuit eSuit)
		{
			switch (eSuit)
			{
				case ESuit.Hearts:
					return new Suit(eSuit, '♥', ConsoleColor.Red);
				case ESuit.Diamonds:
					return new Suit(eSuit, '♦', ConsoleColor.Red);
				case ESuit.Clubs:
					return new Suit(eSuit, '♣', ConsoleColor.White);
				case ESuit.Spades:
					return new Suit(eSuit, '♠', ConsoleColor.White);
				default:
					throw new ArgumentOutOfRangeException(nameof(eSuit), eSuit, null);
			}
		}
		public static CardValue GetCardValue(ECardValue eCardValue)
		{
			switch (eCardValue)
			{
				case ECardValue.Ace:
					return new CardValue(eCardValue, 'A');
				case ECardValue.Two:
					return new CardValue(eCardValue, '2');
				case ECardValue.Three:
					return new CardValue(eCardValue, '3');
				case ECardValue.Four:
					return new CardValue(eCardValue, '4');
				case ECardValue.Five:
					return new CardValue(eCardValue, '5');
				case ECardValue.Six:
					return new CardValue(eCardValue, '6');
				case ECardValue.Seven:
					return new CardValue(eCardValue, '7');
				case ECardValue.Eight:
					return new CardValue(eCardValue, '8');
				case ECardValue.Nine:
					return new CardValue(eCardValue, '9');
				case ECardValue.Ten:
					return new CardValue(eCardValue, 'T');
				case ECardValue.Jack:
					return new CardValue(eCardValue, 'J');
				case ECardValue.Queen:
					return new CardValue(eCardValue, 'Q');
				case ECardValue.King:
					return new CardValue(eCardValue, 'K');
				default:
					throw new ArgumentOutOfRangeException(nameof(eCardValue), eCardValue, null);
			}
		}
		public static Card GetCard(ESuit eSuit, ECardValue eCardValue)
		{
			var output = new Card(GetSuit(eSuit), GetCardValue(eCardValue));
			return output;
		}
		public static Deck GetDeck()
		{
			var output = new Deck();
			foreach (var cardValue in Enum.GetValues(typeof(ECardValue)).Cast<ECardValue>())
				foreach (var suit in Enum.GetValues(typeof(ESuit)).Cast<ESuit>())
					output.Cards.Add(GetCard(suit, cardValue));
			return output;
		}

		public static Shoe GetShoe(int deckAmount)
		{
			var output = new Shoe();
			for (int i = 0; i < deckAmount; i++)
				output.Cards.AddRange(GetDeck().Cards);
			return output;
		}
	}
}