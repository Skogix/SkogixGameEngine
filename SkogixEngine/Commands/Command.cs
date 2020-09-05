using System.Collections.Generic;

namespace ECS.Commands {
	
	public class CommandContainer{
		public CommandContainer() {
			Items = new List<ICommand>();
		}
		public void AddItem(ICommand item) {
			Items.Add(item);
		}
		public List<ICommand> Items { get; set; }
	}
}