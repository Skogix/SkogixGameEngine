using System;
using ECS;

namespace RogueLikeUI {
	public class AttackCommand : ICommand {
		public AttackCommand(Entity attacker, Entity defender) {
			Defender = defender;
			Attacker = attacker;
			IsCompleted = false;
			AttackDamage = attacker.Get<AttackComponent>().AttackDamage;
		}
		public bool IsCompleted { get; set; }
		public int AttackDamage { get; set; }
		public Entity Defender { get; set; }
		public Entity Attacker { get; set; }
		public void Execute() {
			Defender.Get<HealthComponent>().Health -= AttackDamage;
			Console.WriteLine($"{Attacker.Hash} attackerade {Defender.Hash} för {AttackDamage} skada.");
			Console.WriteLine($"{Defender.Hash} har nu {Defender.Get<HealthComponent>().Health} hp.");
		}
	}
}