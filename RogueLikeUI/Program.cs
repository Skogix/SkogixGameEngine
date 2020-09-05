#region
using ECS;
#endregion

namespace RogueLikeUI {
	internal class Program {
		private static void Main(string[] args) {
			var w = new World();
			w.Init();
			var attacker = w.EntityFactory.Get(new PlayerTemplate("Skogix"));
			var defender = w.EntityFactory.Get(new MonsterTemplate());
			w.AddSystem(new ModSystem(w));
			w.InitSystems();
			var commandManager = w.MessageManager.CommandManager;
			w.MessageManager.CommandManager.AddCommand<AttackCommand>(new AttackCommand(attacker, defender));
			commandManager.AddCommand<AttackCommand>(new AttackCommand(attacker, defender));
			commandManager.RunCommands();
		}
	}
}