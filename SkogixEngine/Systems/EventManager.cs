#region
using System;
using System.Collections.Generic;
using System.Linq;
using ECS.Interfaces;
#endregion

namespace ECS.Systems {
	public class EventManager {
		private readonly List<Handler> Handlers = new List<Handler>();
		private Dictionary<int, object> _bus = new Dictionary<int, object>();
		internal EventManager(World world) { World = world; }
		internal World World { get; }
		internal void Subscribe<T>(object sub, Action<T> handler) { Handlers.Add(GetHandler<T>(sub, handler)); }
		internal void Publish<T>(object sender, T data) {
			if (data is IMessage iMessage) World.DebugSystem.Debug(iMessage.Message);
			foreach (var handler in Handlers.Where(h => h.Type == typeof(T)))
				if (handler.Action is Action<T> sendAction)
					sendAction(data);
		}
		internal void Publish<T>(T data) { Publish(null, data); }
		private static Handler GetHandler<T>(object sub, Delegate handler) {
			return new Handler {Action = handler, Type = typeof(T), Sender = new WeakReference(sub)};
		}
		private class Handler {
			public Delegate Action { get; set; }
			public Type Type { get; set; }
			public object Sender { get; set; }
		}

		internal void Push<T>(T data) where T : ICommand, new() {
			_bus.Add(0, data ?? new T());
		}
		internal IEnumerable<T> Pull<T>(Type type = default) where T: class, ICommand{
			var output = type != null ? _bus.Where(o => o.GetType() == type) : _bus.Where(o => o.GetType() == typeof(T));
			foreach(var o in output) yield return o.Value as T;
		}
	}
}