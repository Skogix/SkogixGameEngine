#region

using System;
using System.Collections.Generic;
using System.Linq;

#endregion

namespace ECS.Hub
{
  public class Hub
  {
    private readonly List<object> _bus = new List<object>();
    private readonly List<Handler> _handlers = new List<Handler>();

    // ReSharper disable once NotAccessedField.Local
    private World.World _world;

    public Hub(World.World world)
    {
      _world = world;
    }

    internal void Publish<T>(T data = default)
    {
      foreach(var handler in _handlers.Where(handler => handler.Type == typeof(T)))
        if(handler.Action is Action<T> sendAction)
          sendAction(data);
    }

    internal void Subscribe<T>(object sub, Action<T> handler)
    {
      _handlers.Add(GetHandler<T>(sub, handler));
    }

    private Handler GetHandler<T>(object sub, Delegate handler)
    {
      return new Handler
      {
        Action = handler, // faktiska delegatet
        Type = typeof(T),
        Sender = new WeakReference(sub), // håller en referens till sender, både för referens och GC
      };
    }

    internal void UnSubscribe<T>(object sub, Action<T> handler = null)
    {
      var handlers = _handlers.Where(h =>
          h.Sender.Target != null && 
          (h.Sender.IsAlive || h.Sender.Target.Equals(sub)) && 
          h.Type == typeof(T))
          .ToList();
      foreach(var h in handlers) _handlers.Remove(h);
    }

    internal void Push<T>(T data) where T : new()
    {
      // dat boxing though
      _bus.Add(data ?? new T());
    }

    internal IEnumerable<T> Pull<T>(Type type = default)
    {
      var output = type != null ? _bus.Where(o => o.GetType() == type) : _bus.Where(o => o.GetType() == typeof(T));
      foreach(var o in output) yield return (T) o;
    }
  }

  internal class Handler
  {
    public Delegate Action { get; set; }
    public Type Type { get; set; }
    public WeakReference Sender { get; set; }
  }
}