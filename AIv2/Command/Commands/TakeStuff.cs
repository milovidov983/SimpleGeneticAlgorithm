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
			var cursor = bot.Cursor;
			if(cursor is null) {
				return;
			}

			var worldObject = map.Get(cursor);
			bot.Handle(worldObject);
		}

		public static bool IsFitToCommand(int id) {
			return 0 + ID_OFFSET <= id && id <= 7 + ID_OFFSET;
		}

		private static readonly BotHandleStratagy handleStratagy
			= new TakeStuffBotHandleWorldObjectStratagy();
		public BotHandleStratagy CreateHandleStratagy() {
			return handleStratagy;
		}




	}
}
