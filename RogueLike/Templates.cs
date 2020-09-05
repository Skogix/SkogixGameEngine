using System.Collections.Generic;
using ECS;

namespace RogueLikeUI {
	public class PlayerTemplate : ITemplate {
		public PlayerTemplate(string name) {
			Components = new List<Component> {
				new AttackComponent(10), 
				new HealthComponent(100, 100),
				new NameComponent(name),
			};
		}
		public List<Component> Components { get; set; }
	}
	public class NameComponent : Component {
		public string Name { get; set; }
		public NameComponent(string name) { Name = name; }
	}
	public class MonsterTemplate : ITemplate {
		public MonsterTemplate() {
			Components = new List<Component> {new AttackComponent(5), new HealthComponent(25, 25)};
		}
		public List<Component> Components { get; set; }
	}
}