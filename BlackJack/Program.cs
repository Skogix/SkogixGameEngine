using System;
using System.Linq;
using ECS;

namespace BlackJack
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.Clear();
			var shoe = CardFactory.GetShoe(5);
			foreach (var card in shoe.Cards)
			{
				card.Print();
			}
		}
	}
}