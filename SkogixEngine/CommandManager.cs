using System.Collections.Generic;
using ECS;

namespace RogueLikeUI {
	public class CommandManager {
		public CommandManager() { Commands = new Dictionary<int, ICommand>(); }
		public Dictionary<int, ICommand> Commands { get; }
		public void AddCommand<T>(int id, ICommand command) where T : class, ICommand {
			Hub.Pub(command as T);
			Commands.Add(id, command);
		}
		public void RunCommands() {
			foreach (var command in Commands.Values) command.Execute();
		}
	}
	public interface ICommand {
		void Execute();
	}
}