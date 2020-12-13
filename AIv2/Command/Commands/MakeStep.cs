using System;
using System.Collections.Generic;
using System.Text;

namespace AIv2 {
	public class MakeStep : ICommand {
		private readonly MapImplementation map;

		public MakeStep(MapImplementation map) {
			this.map = map;
		}

		public void Execute(Bot bot) {
			int currentCommandId = bot.Brain.CurrentCommandId;
			Position botCurrentPosition = bot.Position;
			var (x, y) = CommandDirectionIds.IdToCoordinate[currentCommandId];
			var newPosition = new Position(
				x: botCurrentPosition.X + x,
				y: botCurrentPosition.Y + y);

			if (!newPosition.IsValid()) {
				return;
			}

			var worldObject = map.Get(newPosition);

			if(IsBotCanMoveTo(worldObject)) {
				bot.Handle(worldObject);

				bot.SetNewPosition(newPosition);
			}
		}

		private bool IsBotCanMoveTo(WorldObject worldObject) {
			switch (worldObject) {
				case Wall _:
				case BotObject _:
					return false;
				default:
					return true;
			}
		}

		public static bool IsFitToCommand(int id) {
			return 0 <= id && id <= 7;
		}

		private static readonly WorldObjectHandleStratagy handleStratagy 
			= new MakeStepBotHandleWorldObjectStratagy();
		public WorldObjectHandleStratagy CreateHandleStratagy() {
			return handleStratagy;
		}
	}
}
