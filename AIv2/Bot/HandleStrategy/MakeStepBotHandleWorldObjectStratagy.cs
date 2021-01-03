using System;

namespace AIv2 {
	public class MakeStepBotHandleWorldObjectStratagy : BotHandleStratagy {
		public override void Handle(WorldObject worldObject, Bot bot) {
			switch (worldObject) {
				case Food _:
					bot.HealthScore += 5;
					worldObject.SetDead(bot.Position);
					break;
				case Poison _:
					bot.HealthScore -= 5;
					worldObject.SetDead(bot.Position);
					break;
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
