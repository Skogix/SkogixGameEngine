using System;
using ECS.Component;
using ECS.Entity;
using ECS.EntitySystem;

namespace ECS.World
{
  public abstract class World
  {
    public World()
    {
      Hub = new Hub.Hub(this);
      EntityManager = new EntityManager(this);
      ComponentManager = new ComponentManager(this);
      EntitySystemManager = new EntitySystemManager(this);
    }

    internal Hub.Hub Hub { get; }
    internal EntityManager EntityManager { get; }
    internal ComponentManager ComponentManager { get; }
    internal EntitySystemManager EntitySystemManager { get; }

    public void Run()
    {
      EntitySystemManager.RunAllSystems();
    }

    public void PrintWorldInfo()
    {
      Console.WriteLine($"Entities: {EntityManager.Entities.Count}");
      Console.WriteLine($"Components: {ComponentManager.ComponentsCount}");
      Console.WriteLine($"Systems: {EntitySystemManager.Systems.Count}");
    }
  }
}