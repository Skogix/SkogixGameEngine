using System;
using System.Collections.Generic;

namespace ECS
{
	public interface ITemplate
	{
		String Name { get; }
		List<Component> Components { get; set; }
	}
}