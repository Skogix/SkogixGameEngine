using ECS;

namespace RogueLikeUI {
	internal class Program {
		private static void Main(string[] args) {
			Skogix.Init();
			var eFactory = new EntityFactory();
			var attacker = eFactory.Get(new HealthComponent(100, 100), new AttackComponent(8));
			var defender = eFactory.Get(new HealthComponent(100, 100));
			Skogix.AddSystem(new ModSystem());
			Skogix.InitSystems();
			var commandManager = new CommandManager();
			var attackCommand = new AttackCommand(attacker, defender);
			commandManager.AddCommand<AttackCommand>(1, attackCommand);
			commandManager.AddCommand<AttackCommand>(2, attackCommand);
			commandManager.RunCommands();
		}
	}
}