#region
using System;
using ECS;
#endregion

namespace RogueLikeUI {
	public class ModSystem : EntitySystem, InitSystem {
		public ModSystem(World world) : base(world) { }
		public void Init() {
			World.MessageManager.Subscribe<AttackCommand>(this, OnAttackCommand); 
			World.MessageManager.Subscribe<EntityAddedEvent>(this, e => Console.WriteLine("LöSER EVENT"));
		}
		private void OnAttackCommand(AttackCommand c) {
			Console.WriteLine("Lägger till 5 skada pga någon anledning.");
			c.AttackDamage += 5;
		}
	}
}