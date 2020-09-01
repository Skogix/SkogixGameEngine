#region

using System.Collections.Generic;
using ECS.Component;
using ECS.Entity;
using ECS.World;
using RogueLike.Data.Components;
using RogueLike.Interfaces;

#endregion

namespace RogueLike.Data
{
  public class Skogix : Entity, IPrototype
  {
    public Inventory Inventory;

    public void Init(World world)
    {
      this.Get<Player>();
      this.Get<Transform>().X = 2;
      this.Get<Transform>().Y = 2;
      this.Get<Drawable>().Glyph = '@';
      this.Get<Actor>();
      Inventory = world.GetPrototypeEntity<Inventory>();
    }
  }

  public class Monster : Entity, IPrototype
  {
    public void Init(World world)
    {
      this.Get<Transform>().X = 5;
      this.Get<Transform>().Y = 5;
      this.Get<Drawable>().Glyph = 'M';
      this.Get<Walkable>().Is = false;
    }
  }

  public class Inventory : Entity, IPrototype
  {
    public List<IITem> Items = new List<IITem>();

    public void Init(World world) { }
  }
}