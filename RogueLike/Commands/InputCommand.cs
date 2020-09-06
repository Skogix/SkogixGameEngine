using System;
using ECS;
using ECS.Interfaces;
using RogueLike.Components;
using RogueLike.Contracts;

namespace RogueLike.Commands {
	public class InputCommand : ICommand{
		public InputCommand(Entity entity, Actions action, Destination destination) {
			Entity = entity;
			Action = action;
			Destination = destination;
		}
		public void Execute() {
			Console.WriteLine("INPUTCOMMAND KÖRS!");
		}
		public void UndoAction() {
			Console.WriteLine("UNDO KÖÖÖRS!");
		}
		public bool IsExecuted { get; set; }
		public Actions Action { get; set; }
		public Destination Destination { get; set; }
		public Entity Entity { get; set; }
	}
}