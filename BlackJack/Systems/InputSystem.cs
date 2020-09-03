using System;
using CardGame.Events;
using ECS;
using static System.Console;

namespace CardGame.Systems
{
	public class InputSystem : EntitySystem, InitSystem, RunSystem
	{
		public void Init()
		{
			AddFilter<Input>();
			AddFilter<Player>();
		}
		public void Run()
		{
			foreach (var entity in Entities)
			{
				var key = entity.Get<Input>().Key = ReadKey(true).KeyChar;
				switch (char.ToUpper(key))
				{
					case '*': Console.WriteLine("Huhuhu");
						break;
					case 'S':
						break;
				}
			}
		}

	}
}