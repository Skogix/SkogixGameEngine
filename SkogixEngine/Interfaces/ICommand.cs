namespace ECS.Interfaces {
	public interface ICommand {
		public void Execute();
		public void UndoAction();
		bool IsExecuted { get; set; }
	}
}