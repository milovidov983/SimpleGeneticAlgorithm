using System;

namespace AIv2 {
	public class TakeStuffBotHandleWorldObjectStratagy : BotHandleStratagy {
		public override void Handle(WorldObject worldObject, Bot bot) {
			switch (worldObject) {
				case Food _:
					bot.HealthScore += 5;
					worldObject.SetDead(bot.Cursor);
					break;
				case Poison _:
					bot.HealthScore += GetProbability() ? 5 : 0;
					worldObject.SetDead(bot.Cursor);
					break;
				case Wall _:
				case BotObject _:
				case Empty _:
					break;
				default:
					throw new Exception("Unknown type of world object");
			}
		}

		private static bool GetProbability() {
			return (new Random()).Next(1, 2) % 2 == 0;
		}
	}
}
