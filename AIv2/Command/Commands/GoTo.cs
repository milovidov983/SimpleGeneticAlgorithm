using System;
using System.Collections.Generic;
using System.Text;

namespace AIv2 {
	public class GoTo : ICommand {
		private readonly MapImplementation map;
		private const int ID_OFFSET = 32;
		private const int MAX_COMMAND_ID = Settings.CODE_SIZE - 1;
		public GoTo(MapImplementation map) {
			this.map = map;
		}

		public void Execute(Bot bot) {
			int currentCommandId = bot.Brain.CurrentCommandId;
			bot.Brain.SetGotoCommandIndex(currentCommandId);
		}

		public static bool IsFitToCommand(int id) {
			return 0 + ID_OFFSET <= id && id <= MAX_COMMAND_ID;
		}

		private static readonly BotHandleStratagy handleStratagy
			= new GoToHandleWorldObjectStratagy();
		public BotHandleStratagy CreateHandleStratagy() {
			return handleStratagy;
		}
	}
}
