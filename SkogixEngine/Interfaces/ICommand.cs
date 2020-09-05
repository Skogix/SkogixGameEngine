namespace ECS {
	public interface ICommand : IMessage{
		void Execute();
	}
}