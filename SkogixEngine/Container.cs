using System;
using System.Collections;
using System.Collections.Generic;

namespace ECS
{
	public class Container : IEnumerable<Entity>
	{
		private SortedDictionary<int, Entity> _entitiesById;

		public Container()
		{
			_entitiesById = new SortedDictionary<int, Entity>();
		}

		public IEnumerator<Entity> GetEnumerator()
		{
			throw new NotImplementedException();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
	}
}