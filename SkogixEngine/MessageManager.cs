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
		
		private readonly List<object> Bus = new List<object>();
		public void Push<T>(T data) { Bus.Add(data); }
		public IEnumerable<T> Pull<T>(Type type = default) where T : class {
			var output = from o in type != null
				             ? Bus.Where(o => o.GetType() == type)
				             : Bus.Where(o => o.GetType() == typeof(T))
			             select o as T;
			Bus.RemoveAll(o => o.GetType() == typeof(T));
			return output;
		}
		
		private readonly List<Handler> Handlers = new List<Handler>();
		public void Subscribe<T>(object sub, Action<T> handler) {
			Handlers.Add(GetHandler<T>(sub, handler));
		}
		public void Publish<T>(object sender, T data) where T: IMessage {
			World.DebugSystem.Debug(data.Description);
			foreach (var handler in Handlers.Where(h => h.Type == typeof(T)))
				if (handler.Action is Action<T> sendAction) {
					sendAction(data);
				}
		}
		public void Publish<T>(T data = default) where T : IMessage { Publish(null, data); }
		
		private static Handler GetHandler<T>(object sub, Delegate handler) {
			return new Handler {Action = handler, Type = typeof(T), Sender = new WeakReference(sub)};
		}
		private class Handler {
			public Delegate Action { get; set; }
			public Type Type { get; set; }
			public object Sender { get; set; }
		}
	}
	public class CommandManager{
		private static int IdCount { get; set; }
		private Dictionary<int, ICommand> AllCommands { get; } = new Dictionary<int, ICommand>();
		private MessageManager MessageManager { get; }
		internal static int Next() { return IdCount++; }
		
		public CommandManager(MessageManager messageManager) { MessageManager = messageManager; }
		public void AddCommand(ICommand command){
			MessageManager.Publish(command);
			AllCommands.Add(Next(), command);
		}
		public void RunCommands() {
			foreach (ICommand command in AllCommands.Values) {
				command.Execute();
			}
		}
	}
	public class EventManager {
		public MessageManager MessageManager;
		public EventManager(MessageManager messageManager) { MessageManager = messageManager; }
	}
}