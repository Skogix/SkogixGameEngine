#region
using System.Collections.Generic;
using ECS.Interfaces;
#endregion

namespace ECS {
	public class CommandHandler: ICommandHandler {
		public CommandHandler() => _commands = new List<ICommand>();
		public List<ICommand> _commands { get;set; }
		public ICommand _command { get;set; }
		public void SetCommand(ICommand command) => _command = command;
		public void Invoke() {
			_commands.Add(_command);
			_command.Execute();
		}
	}
}