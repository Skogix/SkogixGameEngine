using ECS;

namespace CardGame.Events
{
	public enum GameAction
	{
		None,
		Stand,
		Hit,
	}
	public class GameEvent : IEvent
	{
		public Hand Hand;
		public Player Player;
		public GameAction GameAction;

		public GameEvent(Player player, Hand hand, GameAction gameAction = GameAction.None)
		{
			Player = player;
			Hand = hand;
		}
	}
}