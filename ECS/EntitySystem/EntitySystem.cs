#region

using System.Collections.Generic;
using ECS.Events;

#endregion

namespace ECS.EntitySystem
{
  public abstract class EntitySystem
  {
    /*
    protected internal EntitySystem(World world)
    {
      World = world;
      world.Sub<ComponentAdded>(OnComponentAdded);
    }
    */

    public List<Entity.Entity> Entities { get; internal set; } = new List<Entity.Entity>();

    protected internal World.World World { get; set; }
    internal List<string> IncludedFilters { get; set; } = new List<string>();

    internal void OnComponentAdded(ComponentAdded e)
    {
      if(EntityShouldBeAdded(e.Entity))
      {
        Entities.Add(e.Entity);
#if DEBUG
        //Console.WriteLine($"onComponentAdded från {GetType().Name}");
#endif
      }
    }

    private bool EntityShouldBeAdded(Entity.Entity entity)
    {
      if (Entities.Contains(entity)) return false;
      var output = true;
      foreach(var filter in IncludedFilters)
        if(entity.Components.Contains(filter) == false)
        {
          output = false;
          break;
        }

      return output;
    }

    protected void AddFilter<T>()
    {
      if(IncludedFilters.Contains(typeof(T).Name) == false) IncludedFilters.Add(typeof(T).Name);
    }

    public abstract void Init();
  }
}