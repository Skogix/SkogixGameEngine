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
			World.MessageManager.Subscribe<CommandContainer, AttackCommand>(this, (container, command) => Console.WriteLine($"{container.ToString()} - {command.ToString()}"));
		}
		private void OnAttackCommand(CommandContainer commandContainer, AttackCommand attackCommand) {
			Console.WriteLine("Lägger till 5 skada pga någon anledning.");
		}
	}
}