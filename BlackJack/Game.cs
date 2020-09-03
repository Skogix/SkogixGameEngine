using System;
using CardGame.Systems;
using ECS;
using static ECS.Skogix;

namespace CardGame
{
	public static class Game
	{
		public static void Run(int width, int height)
		{
			Console.CursorVisible = false;
			Init();
			
			AddSystem(new TableSystem());
			AddSystem(new InputSystem());
			AddSystem(new DealerSystem());
			AddSystem(new GameSystem());
			AddSystem(new TileMapSystem(width, height));
			AddSystem(new UiSystem());
			
			InitSystems();
			
			var player = new Player();
			var dealer = new Dealer();
			var deck = new Deck();
			var table = new Table(dealer, deck);

			while (true)
			{
				Skogix.Run();
			}
		}
	}

	
}