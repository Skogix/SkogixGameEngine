using System.Collections.Generic;
using ECS;

namespace RogueLikeUI {
	public class PlayerTemplate : ITemplate {
		public List<Component> Components { get; set; }
		public PlayerTemplate() {
			Components = new List<Component> {
				new AttackComponent(10), 
				new HealthComponent(100, 100)
			};
		}
	}
	public class MonsterTemplate : ITemplate {
		public List<Component> Components { get; set; }
		public MonsterTemplate() {
			Components = new List<Component> {
				new AttackComponent(5), 
				new HealthComponent(25, 25)
			};
		}
	}
}