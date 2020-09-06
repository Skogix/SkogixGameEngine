#region
using ECS;
using ECS.Interfaces;
using ECS.Systems;
using RogueLike.Components;
#endregion

namespace RogueLike.Systems {
	public class MoveSystem : EntitySystem, IRunSystem, InitSystem {
		public MoveSystem(World world) : base(world) { World = world; }
		public World World { get; }
		public void Init() {
			AddFilter(typeof(Destination));
			AddFilter(typeof(Transform));
		}
		public void Run() {
			foreach (var entity in Entities) {
				var transform = entity.Get<Transform>();
				var destination = entity.Get<Destination>();
				transform.X = destination.X;
				transform.Y = destination.Y;
				this.Push(new RemoveComponentCommand(entity, destination));
			}
		}
		public class RemoveComponentCommand : ICommand {
			public readonly Component Component;
			public readonly Entity Entity;
			public RemoveComponentCommand(Entity entity, Component component) {
				Entity = entity;
				Component = component;
			}
			public void Execute() { Entity.RemoveComponent(Component); }
			public void UndoAction() { Entity.Add(Component); }
			public bool IsExecuted { get; set; }
		}
	}
}