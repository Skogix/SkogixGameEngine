namespace ECS.Interfaces {
	public interface ICommand {
		bool IsExecuted { get;set; }
		public void Execute();
		public void UndoAction();
	}
}