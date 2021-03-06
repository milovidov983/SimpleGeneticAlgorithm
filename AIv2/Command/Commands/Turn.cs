﻿using System;
using System.Collections.Generic;
using System.Text;

namespace AIv2 {
	public class Turn : ICommand {
		private readonly MapImplementation map;
		private const int ID_OFFSET = 24;

		public Turn(MapImplementation map) {
			this.map = map;
		}

		public void Execute(Bot bot) {
			int currentCommandId = bot.Brain.CurrentCommandId;
			bot.Direction = currentCommandId - ID_OFFSET;
		}

		public static bool IsFitToCommand(int id) {
			return 0 + ID_OFFSET <= id && id <= 7 + ID_OFFSET;
		}

		private static readonly WorldObjectHandleStratagy handleStratagy
			= new TurnBotHandleWorldObjectStratagy();
		public WorldObjectHandleStratagy CreateHandleStratagy() {
			return handleStratagy;
		}

	}
}
