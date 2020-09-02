namespace ECS
{
	public static class IdFactory<T>
	{
		private static int _idCount;

		public static int Next()
		{
			return _idCount++;
		}
	}
}