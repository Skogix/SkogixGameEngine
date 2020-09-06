using System.Collections.Generic;
using ECS.Interfaces;

namespace ECS {
	public interface ICommandHandler {
		List<ICommand> _commands { get; set; }
		ICommand _command { get; set; }
		void SetCommand(ICommand command);
		void Invoke();
	}
}