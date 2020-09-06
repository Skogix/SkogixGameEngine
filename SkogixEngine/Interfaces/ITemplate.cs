#region
using System.Collections.Generic;
#endregion

namespace ECS.Interfaces {
	public interface ITemplate {
		List<Component> Components { get; set; }
	}
}