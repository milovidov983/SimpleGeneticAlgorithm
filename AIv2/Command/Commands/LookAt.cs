using System;
using System.Collections.Generic;
using System.Text;

namespace AIv2 {
	public class LookAt : ICommand {
		private readonly MapImplementation map;
		private const int ID_OFFSET = 16;

		public LookAt(MapImplementation map) {
			this.map = map;
		}

		public void Execute(Bot bot) {
			int currentCommandId = bot.Brain.CurrentCommandId;
			Position botCurrentPosition = bot.Position;
			var (x, y) = CommandDirectionIds.IdToCoordinate[currentCommandId - ID_OFFSET];
			var newPosition = new Position(
				x: botCurrentPosition.X + x,
				y: botCurrentPosition.Y + y);
			if (newPosition.IsValid()) {
				var worldObject = map.Get(newPosition);
				bot.Handle(worldObject);
			}
		}

		public static bool IsFitToCommand(int id) {
			return 0 + ID_OFFSET <= id && id <= 7 + ID_OFFSET;
		}

		private static readonly WorldObjectHandleStratagy handleStratagy
			= new LookAtBotHandleWorldObjectStratagy();
		public WorldObjectHandleStratagy CreateHandleStratagy() {
			return handleStratagy;
		}



	}
}
