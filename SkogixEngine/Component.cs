using System;

namespace ECS
{
	public abstract class Component : ICloneable
	{
		public object Clone() => MemberwiseClone();
	}
}