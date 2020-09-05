#region
using System.Collections.Generic;
using ECS;
#endregion

namespace RogueLikeUI {
	public class AttackCommand : ICommand{
		private static int _idCount;
		public AttackCommand(Entity attacker, Entity defender) {
			Defender = defender;
			Attacker = attacker;
			IsCompleted = false;
			AttackDamage = attacker.GetComponent<AttackComponent>().AttackDamage;
			Description = $"|Attack| {attacker.GetHash} -> {defender.GetHash}";
		}
		public bool IsCompleted { get; set; }
		public int AttackDamage { get; set; }
		public Entity Defender { get; set; }
		public Entity Attacker { get; set; }
		public void Execute() {
			Defender.GetComponent<HealthComponent>().Health -= AttackDamage;
			Attacker.W.MessageManager.Publish(Attacker, new DamageDoneEvent(Attacker, Defender, AttackDamage));
		}
		public SortedDictionary<int, ICommand> Commands { get; }
		public void AddCommand(ICommand command) {
			Commands.Add(Next(), command);
		}
		private static int Next() => _idCount++;
		
		public string Description { get; }
	}
	public class DamageDoneEvent : IEvent{
		public string Description { get; }
		public DamageDoneEvent(Entity from, Entity to, int amount) {
			Description = $"{from.GetHash} attackerade {to.GetHash} för {amount} dmg.";
		}
	}
}