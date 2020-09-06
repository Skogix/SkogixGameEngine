#region
using System.Collections.Generic;
using ECS;
using ECS.Commands;
#endregion

namespace RogueLikeUI {
	public class AttackCommand : ICommand{
		private readonly CommandContainer _commandContainer;
		private static int _idCount;
		public AttackCommand(CommandContainer commandContainer, Entity attacker, Entity defender) {
			_commandContainer = commandContainer;
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
			Attacker.W.MessageManager.Publish<IEvent, DamageDoneEvent>(new DamageDoneEvent(Attacker, Defender, AttackDamage));
		}
		public string Description { get; }
	}
	public class DamageDoneEvent : IEvent{
		public string Description { get; }
		public DamageDoneEvent(Entity from, Entity to, int amount) {
			Description = $"{from.GetHash} attackerade {to.GetHash} för {amount} dmg.";
		}
	}
}