#region
using System.Collections.Generic;
using ECS;
using ECS.Interfaces;
using RogueLike.Components;
#endregion

namespace RogueLike {
	public class PlayerTemplate: ITemplate {
		public PlayerTemplate(string name) => Components = new List<Component> {
			new Health(100, 100), new NameComponent(name), new Actor(), new Transform(4, 4),
			new Drawable('@'),
		};
		public List<Component> Components { get;set; }
	}
	public class WallTile: ITemplate {
		public WallTile(int x, int y) => Components = new List<Component> {
			new Drawable('#'),
			new Transform(x,y),
		};
		public List<Component> Components { get;set; }
	}
	public class FloorTile: ITemplate {
		public FloorTile(int x, int y) => Components = new List<Component> {
			new Drawable('.'),
			new Transform(x,y),
		};
		public List<Component> Components { get;set; }
	}

	
	public class NameComponent: Component {
		public NameComponent(string name) => Name = name;
		public string Name { get;set; }
	}
	public class MonsterTemplate: ITemplate {
		public MonsterTemplate(string name) => Components = new List<Component> {new Health(25, 25), new NameComponent(name)};
		public List<Component> Components { get;set; }
	}
}