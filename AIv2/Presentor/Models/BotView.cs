using AIv2;
using System;

namespace AiLib.Presentor {
	public class BotView : IBotEventObserver {
		public Guid Id { get; init; }
		public int PositionX { get; set; }
		public int PositionY { get; set; }
		public int CursorX { get; set; }
		public int CursorY { get; set; }
		public BotState State { get; set; }
		public BotPropertiesView Properties { get; set; }

		public BotView(Guid id, Position position) {
			Id = id;
			PositionX = position.X;
			PositionY = position.Y;
			State = BotState.New;
		}


		public void SetDead(Guid id, Position position) {
			State = BotState.Dead;
		}

		public void SetMove(Guid id, Position from, Position to) {
			PositionX = to.X;
			PositionY = to.Y;
		}

		public void UpGeneration(int gen) {
			throw new NotImplementedException();
		}
	}
}
