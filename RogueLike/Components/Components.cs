#region
using ECS;
#endregion

namespace RogueLike {
	public class Health : Component {
		public Health(int health, int maxHealth) {
			Hp = health;
			MaxHp = maxHealth;
		}
		public int Hp { get; set; }
		public int MaxHp { get; set; }
	}
	public class Actor : Component {
		public char Key { get; set; }
	}
	public class Transform : Component {
		public Transform(int x, int y) {
			X = x;
			Y = y;
		}
		public int X { get; set; }
		public int Y { get; set; }
	}
	public class Drawable : Component {
		public Drawable(char glyph) => Glyph = glyph;
		public char Glyph { get; set; }
	}
}