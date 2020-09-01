#region

using ECS.Entity;
using ECS.World;
using RogueLike.Interfaces;

#endregion

namespace RogueLike.Data
{
  public class Items
  {
    public class Sword : IPrototype, IWeapon, IITem
    {
      public int Damage { get; set; }
      public string Name { get; set; }
      
      public Sword(string name, int damage)
      {
        Damage = damage;
        Name = name;
      }


      
      public void Init(World world)
      {
      }
    }
  }

  public interface IWeapon : IITem
  {
    public int Damage { get; set; }
  }
}