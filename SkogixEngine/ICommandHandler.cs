#region
using System.Collections.Generic;
using ECS.Interfaces;
#endregion

namespace ECS {
	public interface ICommandHandler {
		List<ICommand> _commands { get;set; }
		ICommand _command { get;set; }
		void SetCommand(ICommand command);
		void Invoke();
	}
}