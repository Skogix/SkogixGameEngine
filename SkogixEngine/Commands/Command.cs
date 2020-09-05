using System.Collections.Generic;

namespace ECS.Commands {
	public class Command{
		public List<ICommand> Commands { get; private set; } = new List<ICommand>();
		public Command(ICommand command) {
			Commands.Add(command);
		}
	}
}