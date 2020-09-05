#region
using System;
using ECS;
#endregion

namespace RogueLikeUI {
	public class AttackCommand : ICommand {
		public AttackCommand(Entity attacker, Entity defender) {
			Defender = defender;
			Attacker = attacker;
			IsCompleted = false;
			AttackDamage = attacker.GetComponent<AttackComponent>().AttackDamage;
		}
		public bool IsCompleted { get; set; }
		public int AttackDamage { get; set; }
		public Entity Defender { get; set; }
		public Entity Attacker { get; set; }
		public void Execute() {
			Defender.GetComponent<HealthComponent>().Health -= AttackDamage;
			Console.WriteLine($"{Attacker.GetHash} attackerade {Defender.GetHash} för {AttackDamage} skada.");
			Console.WriteLine($"{Defender.GetHash} har nu {Defender.GetComponent<HealthComponent>().Health} hp.");
		}
	}
}