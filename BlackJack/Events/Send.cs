using System;
using CardGame.Systems;
using ECS;

namespace CardGame.Events
{
	public static class Send
	{
		public static void Table(TableCommand tableCommand) => Hub.Pub(new TableEvent(tableCommand));
		public static void Table(TableCommand tableCommand, Entity entity) => Hub.Pub(new TableEvent(tableCommand, entity));
		public static void UiClearScreen() => Hub.Pub<Ui>(UiCommand.ClearScreen);
		public static void UiSetUiPositionText(UiPosition uiPosition, string message) => Hub.Pub(new Ui(UiCommand.SetUiPositionText, uiPosition, message));
		public static void UiPrint(int x, int y, string message, ConsoleColor color) => Hub.Pub(new Ui(UiCommand.Print, x, y, message, color: color));

	}
}