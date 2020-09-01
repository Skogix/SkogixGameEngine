namespace ECS.Events
{
  public class Debug
  {
    public Debug(string message)
    {
      Message = ":::" + message;
    }

    public string Message { get; set; }
  }
}