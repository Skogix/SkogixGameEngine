namespace ECS.World
{
  public class WorldFactory
  {
    public static T CreateWorld<T>() where T : World, new()
    {
      var output = new T();
      return output;
    }
  }
}