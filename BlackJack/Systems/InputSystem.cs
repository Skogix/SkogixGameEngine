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
		}
		public void Run()
		{
			foreach (var entity in Entities)
			{
				var key = entity.Get<Input>().Key = ReadKey(true).KeyChar;
				switch (char.ToUpper(key))
				{
					case '*':
						Send.UiPrint(3,3,"Skogix", ConsoleColor.Blue);
						break;
					case 'S':
						Send.Table(TableCommand.Sit, entity);
						break;
				}
			}
		}

	}
}