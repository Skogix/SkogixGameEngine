#region
using ECS.Interfaces;
#endregion

namespace RogueLike.Events {
	public class PrintEvent : IEvent {
		public readonly char Glyph;
		public readonly int X;
		public readonly int Y;
		public PrintEvent(in int x, in int y, char glyph) {
			X = x;
			Y = y;
			Glyph = glyph;
			Message = $"Print: {X},{Y}:{Glyph}";
		}
		public string Message { get; set; }
	}
}