using System.Collections.Generic;

namespace ECS {
	public interface ITemplate {
		List<Component> Components { get; set; }
	}
}