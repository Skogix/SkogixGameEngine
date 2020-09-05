using System;
using ECS;

namespace RogueLikeUI {
	public class ModSystem : EntitySystem, INitSystem {
		public void Init() { Hub.Sub<AttackCommand>(this, OnAttackCommand); }
		private void OnAttackCommand(AttackCommand c) {
			Console.WriteLine("Lägger till 5 skada pga någon anledning.");
			c.AttackDamage += 5;
		}
	}
}