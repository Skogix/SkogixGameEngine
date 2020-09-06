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
		
		/*
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
		*/
		
		private readonly List<Handler> Handlers = new List<Handler>();
		public void Subscribe<TFilter, TData>(object sub, Action<TFilter, TData> handler) {
			Handlers.Add(GetHandler<TFilter, TData>(sub, handler));
		}
		public void Publish<TFilter, TData>(object sender, TData data, TFilter filter) {
			if (data is IMessage iMessage) World.DebugSystem.Debug(iMessage.Description);
			foreach (var handler in Handlers.Where(h => h.TypeData == typeof(TData)))
				if (handler.Action is Action<TFilter, TData> sendAction) {
					sendAction(filter, data);
				}
		}
		public void Publish<TFilter, TData>(TData data, TFilter filter) { Publish(null, data, filter); }
		public void Publish<TData>(TData data) => Publish(null, data, new AllFilterTag());
		
		private static Handler GetHandler<TFilter, TData>(object sub, Delegate handler) {
			return new Handler {Action = handler, TypeData = typeof(TData), TypeFilter = typeof(TFilter), Sender = new WeakReference(sub)};
		}
		private class Handler {
			public Type TypeFilter { get; set; }
			public Delegate Action { get; set; }
			public Type TypeData { get; set; }
			public object Sender { get; set; }
		}
	}
	public class AllFilterTag { }
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
		public void ExecuteCommands() {
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