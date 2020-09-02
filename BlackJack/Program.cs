using System;
using System.Collections.Generic;
using System.Linq;
using ECS;

namespace BlackJack
{
	class Program
	{
		static void Main(string[] args)
		{
			Skogix.Init();
			
			/*
			// old
			var shoeOld = CardFactoryOld.GetShoe(5);
			shoeOld.Cards.ForEach(c => c.Print());
			
			Skogix.Print("");
			Skogix.Print("");
			Skogix.Print("");
			
			*/
			// new

			var cardFilter = new Filter(typeof(Card));
			
			var deckFromFactory = CardFactory.GetDeck();

			foreach (var entity in cardFilter.Entities)
			{
				entity.Get<Card>().Print();
			}
		}
	}
}