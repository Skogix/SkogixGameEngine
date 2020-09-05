using ECS;

namespace RogueLikeUI {
	internal class Program {
		private static void Main(string[] args) {
			Skogix.Init();
			var eFactory = new EntityFactory();
			var attacker = eFactory.Get(new PlayerTemplate());
			var defender = eFactory.Get(new MonsterTemplate());
			Skogix.AddSystem(new ModSystem());
			Skogix.InitSystems();
			var commandManager = new CommandManager();
			commandManager.AddCommand<AttackCommand>(new AttackCommand(attacker, defender));
			commandManager.AddCommand<AttackCommand>(new AttackCommand(attacker, defender));
			commandManager.RunCommands();
		}
	}
}