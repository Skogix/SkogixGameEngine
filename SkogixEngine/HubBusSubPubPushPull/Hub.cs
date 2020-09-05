using System;
using System.Collections.Generic;
using System.Linq;

namespace ECS {
	public class Hub {
		private static readonly List<Handler> Handlers = new List<Handler>();
		public World World { get; private set; }
		public Hub(World world) {
			World = world;
		}
		public static void Sub<T>(object sub, Action<T> handler) { Handlers.Add(GetHandler<T>(sub, handler)); }
		public void Pub<T>(object sender, T data = default) {
			foreach (var handler in Handlers.Where(h => h.Type == typeof(T)))
				if (handler.Action is Action<T> sendAction)
					sendAction(data);
		}
		public void Pub<T>(T data = default) { Pub(null, data); }
		private static Handler GetHandler<T>(object sub, Delegate handler) {
			return new Handler {Action = handler, Type = typeof(T), Sender = new WeakReference(sub)};
		}
		private class Handler {
			public Delegate Action { get; set; }
			public Type Type { get; set; }
			public object Sender { get; set; }
		}
	}
}