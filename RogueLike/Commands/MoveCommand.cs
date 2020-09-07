#region
using ECS;
using ECS.Interfaces;
using RogueLike.Components;
#endregion

namespace RogueLike.Commands {
	public class MoveCommand: ICommand {
		public MoveCommand(Transform transform, Destination destination) {
			Transform = transform;
			Destination = destination;
		}
		public Transform Transform { get;set; }
		public Destination Destination { get;set; }
		public void Execute() {
			Transform.X += Destination.X;
			Transform.X += Destination.Y;
			IsExecuted = true;
		}
		public void UndoAction() {
			if(IsExecuted == false) return;
			Transform.X -= Destination.X;
			Transform.X -= Destination.Y;
		}
		public bool IsExecuted { get;set; }
	}
	public class MovementHandler: CommandHandler {
		/*
		private readonly List<ICommand> _commands;
		private ICommand _command;

		public MovementHandler() {
			_commands = new List<ICommand>();
		}
		public void SetCommand(ICommand command) => _command = command;
		public void Invoke() {
			_commands.Add(_command);
			_command.Execute();
		}
		public void UndoActions() {
			foreach (var command in Enumerable.Reverse(_commands)) {
				command.UndoAction();
			}
		}
	*/
	}
}