using ECS.Interfaces;

namespace ECS.Systems {
	public abstract class ActionSystem : ISystem, IRunSystem {
		public abstract void Run();
	}
}