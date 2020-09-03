using System.Collections.Generic;
using ECS;

namespace CardGame.Systems
{
	public class TableSystem : EntitySystem, RunSystem, InitSystem
	{
		public static Table Table;
		public List<Table> Tables = new List<Table>();
		public void Run()
		{
		}

		public void Init()
		{
		}

		private void TrySit(Player player)
		{
			Table.Players.Add(player);
			player.Add(new Sitting(Table));
		}
	}
}