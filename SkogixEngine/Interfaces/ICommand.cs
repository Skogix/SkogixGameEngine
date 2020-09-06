namespace ECS.Interfaces {
	public interface ICommand {
		public void Execute();
		bool IsExecuted { get; set; }
	}
}