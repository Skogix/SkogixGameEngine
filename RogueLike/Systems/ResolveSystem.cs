using ECS;
using ECS.Interfaces;
using ECS.Systems;

namespace RogueLike.Systems {
	public class ResolveSystem : EntitySystem,IRunSystem,InitSystem{
		public ResolveSystem(World world): base(world) {}
		public void Run() {
			this.PullAll();
		}
		public void Init() {
			
		}
	}
}