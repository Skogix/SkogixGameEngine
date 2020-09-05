#region
using ECS;
#endregion

namespace RogueLikeUI {
	public class HealthComponent : Component {
		public HealthComponent(int health, int maxHealth) {
			Health = health;
			MaxHealth = maxHealth;
		}
		public int Health { get; set; }
		public int MaxHealth { get; set; }
	}
	public class AttackComponent : Component {
		public AttackComponent(int attackDamage) { AttackDamage = attackDamage; }
		public int AttackDamage { get; set; }
	}
}