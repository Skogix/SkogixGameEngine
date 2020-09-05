using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using ECS;

namespace RogueLikeUI {
	class Program {
		static void Main(string[] args) {
			Skogix.Init();
			var attacker = Entity.New();
			var defender = Entity.New();
			
			attacker.Add(new HealthComponent());
			attacker.Add(new AttackComponent());
			defender.Add(new HealthComponent());
			
			Skogix.AddSystem(new ModSystem());
			Skogix.InitSystems();
			
			var commandManager = new CommandManager();
			
			var attackCommand = new AttackCommand(attacker, defender, 5);
			commandManager.AddCommand<AttackCommand>(attackCommand);
			
			commandManager.RunCommands();
			
			
		}
	}

	public class ModSystem : EntitySystem, InitSystem{
		public void Init() {
			Hub.Sub<AttackCommand>(this, OnAttackCommand);
		}
		private void OnAttackCommand(AttackCommand c) {
				Console.WriteLine($"Lägger till 5 skada pga någon anledning.");
				c.AttackDamage += 5;
		}
	}
	
	internal sealed class AttackComponent : Component {
		public int AttackDamage { get; set; }
	}
	internal sealed class HealthComponent : Component {
		public int Health { get; set; } = 100;
		public int MaxHealth { get; set; } = 100;
	}
	public interface ICommand {
		void Execute();
	}
	public class AttackCommand : ICommand {
		public AttackCommand(Entity attacker, Entity defender, int attackDamage) {
			Defender = defender;
			Attacker = attacker;
			AttackDamage = attackDamage;
			IsCompleted = false;
		}
		public bool IsCompleted { get; set; }
		public int AttackDamage { get; set; }
		public Entity Defender { get; set; }
		public Entity Attacker { get; set; }
		
		public void Execute() {
			Defender.Get<HealthComponent>().Health -= AttackDamage;
			// send event med vad som har hänt
			Console.WriteLine($"{Attacker.Hash} attackerade {Defender.Hash} för {AttackDamage} skada.");
			Console.WriteLine($"{Defender.Hash} har nu {Defender.Get<HealthComponent>().Health} hp.");
		}
	}

	public class CommandManager{
		public CommandManager() {
			Commands = new Dictionary<int, ICommand>();
		}
		public Dictionary<int, ICommand> Commands { get; private set; }

	public void AddCommand<T>(ICommand command) where T: class, ICommand{
		Hub.Pub(command as T);
		Commands.Add(1, command);
	}
	public void RunCommands() {
		foreach (var command in Commands.Values) {
			command.Execute();
		}
	}
	}

}