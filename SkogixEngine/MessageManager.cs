#region
using System;
using System.Collections.Generic;
using System.Linq;
#endregion

namespace ECS {
	public class MessageManager {
		public MessageManager(World world) {
			World = world;
			CommandManager = new CommandManager(this);
			EventManager = new EventManager(this);
		}
		public World World { get; }
		public CommandManager CommandManager { get; }
		public EventManager EventManager { get; }
	}
	public class CommandManager {
		private readonly List<object> Bus = new List<object>();
		public CommandManager(MessageManager messageManager) { MessageManager = messageManager; }
		internal static int IdCount { get; private set; }
		internal Dictionary<int, ICommand> Commands { get; } = new Dictionary<int, ICommand>();
		public MessageManager MessageManager { get; }
		internal static int Next() { return IdCount++; }
		public void Push<T>(T data) { Bus.Add(data); }
		public IEnumerable<T> Pull<T>(Type type = default) where T : class {
			var output = from o in type != null
				             ? Bus.Where(o => o.GetType() == type)
				             : Bus.Where(o => o.GetType() == typeof(T))
			             select o as T;
			Bus.RemoveAll(o => o.GetType() == typeof(T));
			return output;
		}
		public void AddCommand<T>(ICommand command) where T : class, ICommand {
			MessageManager.EventManager.Publish(command as T);
			Commands.Add(Next(), command);
		}
		public void RunCommands() {
			foreach (var command in Commands.Values) command.Execute();
		}
	}
	public class EventManager {
		private readonly List<Handler> Handlers = new List<Handler>();
		public MessageManager MessageManager;
		public EventManager(MessageManager messageManager) { MessageManager = messageManager; }
		public void Subscribe<T>(object sub, Action<T> handler) { Handlers.Add(GetHandler<T>(sub, handler)); }
		public void Publish<T>(object sender, T data = default) {
			foreach (var handler in Handlers.Where(h => h.Type == typeof(T)))
				if (handler.Action is Action<T> sendAction)
					sendAction(data);
		}
		public void Publish<T>(T data = default) { Publish(null, data); }
		private static Handler GetHandler<T>(object sub, Delegate handler) {
			return new Handler {Action = handler, Type = typeof(T), Sender = new WeakReference(sub)};
		}
		private class Handler {
			public Delegate Action { get; set; }
			public Type Type { get; set; }
			public object Sender { get; set; }
		}
	}
	public interface ICommand {
		void Execute();
	}
}