using CardGame.Events;
using ECS;

namespace CardGame.Systems
{
	public class DealerSystem : EntitySystem, RunSystem, InitSystem
	{
		public void Run()
		{
		}

		public void Init()
		{
			Hub.Sub<GameEvent>(this, OnGameEvent);
		}

		private void OnGameEvent(GameEvent e)
		{
			
		}
	}
}