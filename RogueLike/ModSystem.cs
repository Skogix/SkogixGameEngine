#region
using System;
using ECS;
#endregion

namespace RogueLikeUI {
	public class ModSystem : EntitySystem, INitSystem {
		public ModSystem(World world) : base(world) { }
		public void Init() { World.MessageManager.EventManager.Subscribe<AttackCommand>(this, OnAttackCommand); }
		private void OnAttackCommand(AttackCommand c) {
			Console.WriteLine("Lägger till 5 skada pga någon anledning.");
			c.AttackDamage += 5;
		}
	}
}