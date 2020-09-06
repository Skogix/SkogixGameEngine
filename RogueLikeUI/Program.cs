#region
using System;
using System.Collections.Generic;
using ECS;
using ECS.Commands;
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
			var commandContainer = new CommandContainer();
			var command = new AttackCommand(commandContainer, attacker, defender);
			w.MessageManager.CommandManager.AddCommandContainer(commandContainer, command);
			// w.MessageManager.CommandManager.AddCommand(new AttackCommand(attacker, defender));
			w.MessageManager.CommandManager.RunCommands();
			w.Run();
			//aaaa
		}
	}
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