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
		public void Subscribe<T1, T2>(object sub, Action<T1, T2> handler) {
			Handlers.Add(GetHandler<T1, T2>(sub, handler));
		}
		public void Publish<T1, T2>(object sender, T2 data, T1 filter) {
			if (data is IMessage iMessage) World.DebugSystem.Debug(iMessage.Description);
			foreach (var handler in Handlers.Where(h => h.Type == typeof(T2)))
				if (handler.Action is Action<T1, T2> sendAction) {
					sendAction(filter, data);
				}
		}
		public void Publish<T1, T2>(T2 data, T1 filter) { Publish<T1, T2>(null, data, filter); }
		public void Publish<T2>(T2 data) => Publish(null, data, new AllFilter());
		
		private static Handler GetHandler<T1, T2>(object sub, Delegate handler) {
			return new Handler {Action = handler, Type = typeof(T2), Filter = typeof(T1), Sender = new WeakReference(sub)};
		}
		private class Handler {
			public Type Filter { get; set; }
			public Delegate Action { get; set; }
			public Type Type { get; set; }
			public object Sender { get; set; }
		}
	}
	public class AllFilter { }
	public class CommandManager{
		private static int IdCount { get; set; }
		private List<CommandContainer> CommandContainers = new List<CommandContainer>();
		private MessageManager MessageManager { get; }
		internal static int Next() { return IdCount++; }
		
		public CommandManager(MessageManager messageManager){
			MessageManager = messageManager;
		}
		public void AddCommandContainer<T1, T2>(T1 container, T2 command) 
			where T1: CommandContainer
			where T2: class, ICommand
			
		{
			container.AddItem(command);
			MessageManager.Publish<T1, T2>(command, container);
			CommandContainers.Add(container);
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