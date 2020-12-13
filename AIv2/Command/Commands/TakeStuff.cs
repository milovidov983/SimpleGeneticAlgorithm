using System;
using System.Collections.Generic;
using System.Text;

namespace AIv2 {
	public class TakeStuff : ICommand {
		private readonly MapImplementation map;
		private const int ID_OFFSET = 8;

		public TakeStuff(MapImplementation map) {
			this.map = map;
		}

		public void Execute(Bot bot) {
			int currentCommandId = bot.Brain.CurrentCommandId;
			Position botCurrentPosition = bot.Position;
			var (x, y) = CommandDirectionIds.IdToCoordinate[currentCommandId - ID_OFFSET];
			var newPosition = new Position(
				x: botCurrentPosition.X + x,
				y: botCurrentPosition.Y + y);

			if (!newPosition.IsValid()) {
				return;
			}

			var worldObject = map.Get(newPosition);

			if(worldObject is Food || worldObject is Poison) {
				bot.Handle(worldObject);
				bot.SetNewPosition(newPosition);
			}
		}

		public static bool IsFitToCommand(int id) {
			return 0 + ID_OFFSET <= id && id <= 7 + ID_OFFSET;
		}

		private static readonly WorldObjectHandleStratagy handleStratagy
			= new TakeStuffBotHandleWorldObjectStratagy();
		public WorldObjectHandleStratagy CreateHandleStratagy() {
			return handleStratagy;
		}




	}
}
