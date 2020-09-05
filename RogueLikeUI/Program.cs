#region
using System;
using ECS;
#endregion

namespace RogueLikeUI {
	internal class Program {
		private static void Main(string[] args) {
			var w = new World();
			var attacker = w.EntityFactory.Get(new PlayerTemplate("Skogix"));
			var defender = w.EntityFactory.Get(new MonsterTemplate());
			w.AddSystem(new InputSystem(w));
			w.AddSystem(new ModSystem(w));
			w.InitSystems();
			//w.MessageManager.CommandManager.AddCommand(new CommandContainer());
			// w.MessageManager.CommandManager.AddCommand(new AttackCommand(attacker, defender));
			// w.MessageManager.CommandManager.RunCommands();
			w.Run();
		}
	}
	internal class CommandContainer { }
	internal class InputSystem : EntitySystem, IRunSystem {
		public InputSystem(World world) : base(world) {
			AddFilter(typeof(NameComponent));
		}
		public void Run() {
			foreach (var entity in Entities) {
				Console.WriteLine(entity.GetComponent<NameComponent>().Name);
			}
		}
	}
}