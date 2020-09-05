#region
using System;
using System.Collections.Generic;
using System.Linq;
using ECS.Commands;
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
		public void Publish<T>(object sender, T data) {
			if (data is IMessage iMessage) World.DebugSystem.Debug(iMessage.Description);
			foreach (var handler in Handlers.Where(h => h.Type == typeof(T)))
				if (handler.Action is Action<T> sendAction) {
					sendAction(data);
				}
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
	public class CommandManager{
		private static int IdCount { get; set; }
		private List<CommandContainer> CommandContainers = new List<CommandContainer>();
		private MessageManager MessageManager { get; }
		internal static int Next() { return IdCount++; }
		
		public CommandManager(MessageManager messageManager){
			MessageManager = messageManager;
		}
		public void AddCommandContainer<T>(CommandContainer command) where T: class, ICommand{
			MessageManager.Publish<T>(command as T);
			CommandContainers.Add(command);
		}
		public void RunCommands() {
			foreach (CommandContainer commandContainer in CommandContainers) {
				foreach (var command in commandContainer.Items) {
					command.Execute();
				}
			}
		}
	}
	public class EventManager {
		public MessageManager MessageManager;
		public EventManager(MessageManager messageManager) { MessageManager = messageManager; }
	}
}