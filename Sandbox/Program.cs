using static ECS.Skogix;

namespace Sandbox
{
	internal class Program
	{
		private static void Main(string[] args)
		{
			Init();
			
			TestEntitySystem testEntitySystem = new TestEntitySystem();
		}
	}

	internal class TestEntitySystem : ECS.EntitySystem
	{
	}
}