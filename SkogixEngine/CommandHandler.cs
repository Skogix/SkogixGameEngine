using System.Collections.Generic;
using ECS.Interfaces;

namespace ECS {
	public class CommandHandler : ICommandHandler {
		
		public List<ICommand> _commands { get; set; }
		public ICommand _command { get; set; }

		public CommandHandler() {
			_commands = new List<ICommand>();
		}
		public void SetCommand(ICommand command) => _command = command;
		public void Invoke() {
			_commands.Add(_command);
			_command.Execute();
		}
	}
}