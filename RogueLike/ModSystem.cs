#region
using System;
using ECS;
using ECS.Commands;
#endregion

namespace RogueLikeUI {
	public class ModSystem : EntitySystem, InitSystem {
		public ModSystem(World world) : base(world) { }
		public void Init() {
			World.MessageManager.Subscribe<CommandContainer, AttackCommand>(this, OnAttackCommand);
		}
		private void OnAttackCommand(CommandContainer cc, AttackCommand c) {
			var command = new AttackModifier(c.Attacker, c.Defender, c.AttackDamage);
			CommandContainer container = cc;
			World.MessageManager.CommandManager.AddCommandContainer(container, command);
		}
	}
	internal class AttackModifier : ICommand {
		public Entity Attacker { get; }
		public Entity Defender { get; }
		public int Damage { get; set; }
		public AttackModifier(Entity attacker, Entity defender, int damage) {
			Attacker = attacker;
			Defender = defender;
			Damage = damage;
		}
		public string Description { get; set; }
		public void Execute() {
			Damage += 5;
			Description = $"Lägger till 5 skada lolz";
		}
	}
}