using System;
using System.Collections.Generic;

namespace AIv2 {
	public class CommandFactory {
		private readonly Dictionary<Type, ICommand> commands;

		public CommandFactory(MapImplementation map) {
			commands = new Dictionary<Type, ICommand> {
				[typeof(MakeStep)] = new MakeStep(map),
				[typeof(TakeStuff)] = new TakeStuff(map),
				[typeof(LookAt)] = new LookAt(map),
				[typeof(Turn)] = new Turn(map),
				[typeof(GoTo)] = new GoTo(map),
			};
		}

		public ICommand GetCommand(int commandId) {
			return commandId switch {
				int id when MakeStep.IsFitToCommand(id) => commands[typeof(MakeStep)],
				int id when TakeStuff.IsFitToCommand(id) => commands[typeof(TakeStuff)],
				int id when LookAt.IsFitToCommand(id) => commands[typeof(LookAt)],
				int id when Turn.IsFitToCommand(id) => commands[typeof(Turn)],
				int id when GoTo.IsFitToCommand(id) => commands[typeof(GoTo)],
				_ => throw new Exception($"{commandId} is unknown command id"),
			};
			;
		}
	}
}