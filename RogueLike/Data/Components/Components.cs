using ECS.Component;

namespace RogueLike.Data.Components
{
  public enum GameAction
  {
    None,
    Move,
    Attack,
  }

  public class Player : Component { }

  public class Actor : Component
  {
    public GameAction Action;
  }

  public class Health : Component
  {
    public int Current;
    public int Max;
  }

  public class Attack : Component
  {
    public int Damage;
    public int HitChance;
  }

  public class Defense : Component
  {
    public int Armor;
    public int DodgeChance;
  }
  
  internal class Drawable
  {
    public char Glyph = '.';
  }
  public class Transform
  {
    public int Z { get; set; }
    public int X = 1;
    public int Y = 1;
  }
}