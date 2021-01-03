using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AIv2 {
	public class LogItem {
		public Position Position { get; set; }
		public Position Cursor { get; set; }
		public int HealthScore { get; set; }
		public int Generation { get; set; }
		public int Direction { get; set; }
		public int CommandId { get; set; }
		public string CommandName { get; set; }
		public Guid Id { get; set; }

		public override string ToString() {
		return $"Id:{string.Join("",Id.ToString().Take(4))}, " +
				$"Pos:{Position}, " +
				$"Cur:{Cursor}, " +
				$"Health:{HealthScore}, " +
				$"Gen:{Generation}, " +
				$"CmdId:{CommandId}, " +
				$"CmdName:{CommandName}, " +
				$"Dir:{Direction}";
		}
	}

	public class Logger {
		public static Logger Instance;
		public Queue<LogItem> logs = new Queue<LogItem>();

		static Logger() {
			Instance = new Logger();
		}

		public void Print() {
			foreach(var item in logs) {
				Console.WriteLine(item);
			}
			if (logs.Count > 0) {
				Console.WriteLine($"total elements: {logs.Count}");
			}
		}
	}

	public class LogItemFactory {
		public static LogItem CreateFrom(Bot bot, int commandId, string commandName) {
			return new LogItem {
				Cursor = bot.Cursor,
				Direction = bot.Direction,
				Generation = bot.Generation,
				HealthScore = bot.HealthScore,
				Position = bot.Position,
				CommandId = commandId,
				CommandName = commandName,
				Id = bot.Id
			};
		}
	}
}
