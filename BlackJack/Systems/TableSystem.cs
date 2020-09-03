using System;
using System.Collections.Generic;
using System.Linq;
using ECS;

namespace CardGame.Systems
{
	public enum TableCommand
	{
		None,
		Sit,
	}
	public class TableEvent : IEvent
	{
		public TableCommand TableCommand;
		public Entity Entity;
		public TableEvent(TableCommand tableCommand)
		{
			TableCommand = tableCommand;
		}

		public TableEvent(TableCommand tableCommand, Entity entity)
		{
			TableCommand = tableCommand;
			Entity = entity;
		}
	}
	public class TableSystem : EntitySystem, RunSystem, InitSystem
	{
		public List<Table> Tables = new List<Table>();
		public void Run()
		{
			Bus.Pull<TableEvent>().ToList().ForEach(OnTableEvent);
		}

		public void Init()
		{
			Hub.Sub<TableEvent>(this, OnTableEvent);
		}

		private void OnTableEvent(TableEvent e)
		{
			switch (e.TableCommand)
			{
				case TableCommand.None:
					break;
				case TableCommand.Sit:
					TrySit(e.Entity);
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}
		}

		private void TrySit(Entity entity)
		{
		}
	}
}