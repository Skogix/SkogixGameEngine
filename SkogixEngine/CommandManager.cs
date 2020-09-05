using System.Collections.Generic;
using ECS;

namespace RogueLikeUI {
	public class CommandManager {
		public World W { get; private set; }
		internal CommandManager(World world) {
			Commands = new Dictionary<int, ICommand>();
			W = world;
		}
		internal static int IdCount { get; private set; }
		internal Dictionary<int, ICommand> Commands { get; }
		internal static int Next() { return IdCount++; }
		public void AddCommand<T>(ICommand command) where T : class, ICommand {
			W.Hub.Pub(command as T);
			Commands.Add(Next(), command);
		}
		public void RunCommands() {
			foreach (var command in Commands.Values) command.Execute();
		}
	}
	public interface ICommand {
		void Execute();
	}
}