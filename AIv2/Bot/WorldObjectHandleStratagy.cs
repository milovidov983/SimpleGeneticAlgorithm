using System;
using System.Collections.Generic;
using System.Text;

namespace AIv2 {
	public abstract class WorldObjectHandleStratagy {
		public abstract void Handle(WorldObject worldObject, Bot bot);
	}

	public class MakeStepBotHandleWorldObjectStratagy : WorldObjectHandleStratagy {
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

	public class TakeStuffBotHandleWorldObjectStratagy : WorldObjectHandleStratagy {
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

	public class LookAtBotHandleWorldObjectStratagy : WorldObjectHandleStratagy {
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

	public class TurnBotHandleWorldObjectStratagy : WorldObjectHandleStratagy {
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

	public class GoToHandleWorldObjectStratagy : WorldObjectHandleStratagy {
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
