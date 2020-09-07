#region
using System;
#endregion

namespace ECS {
	public abstract class Component: ICloneable {
		public object Clone() => MemberwiseClone();
		public override string ToString() => $"[{GetType().Name}]";
	}
}