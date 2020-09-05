#region
using System;
using ECS;
using ECS.Commands;
#endregion

namespace RogueLikeUI {
	public class ModSystem : EntitySystem, InitSystem {
		public ModSystem(World world) : base(world) { }
		public void Init() {
			World.MessageManager.Subscribe<CommandContainer>(this, OnAttackCommand); 
			World.MessageManager.Subscribe<CommandContainer>(this, e => Console.WriteLine("LöSER EVENT"));
		}
		private void OnAttackCommand(CommandContainer commandContainer) {
			Console.WriteLine("Lägger till 5 skada pga någon anledning.");
		}
	}
}