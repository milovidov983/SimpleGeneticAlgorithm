using System;

namespace AIv2 {
	public class LookAtBotHandleWorldObjectStratagy : BotHandleStratagy {
		public override void Handle(WorldObject worldObject, Bot bot) {
			switch (worldObject) {
				case Food _:
				case Poison _:
				case Wall _:
				case BotObject _:
				case Empty _:
					break;
				default:
					throw new Exception("Unknown type of world object");
			}
		}
	}
}
