using System.Linq;

namespace AIv2 {
	public class CommandIndex {
		private const int MAX_COMMAND_ID = Settings.CODE_SIZE - 1;
		private int _value;
		private int Value { 
			get => _value;
			set {
				_value = value;
				if (MAX_COMMAND_ID >= _value || _value < 0) {
					_value = 0;
				}
			}
		}

		public CommandIndex(int value) {
			Value = value;
		}

		public static implicit operator int(CommandIndex v) => v.Value;
		public static implicit operator CommandIndex(int v) => new CommandIndex(v);
		public static CommandIndex operator +(CommandIndex a, int b) {
			a.Value += b;
			return a;
		}

		public static CommandIndex operator ++(CommandIndex a) { 
			a.Value++;
			return a;
		}
		public static CommandIndex operator --(CommandIndex a) {
			a.Value--;
			return a;
		}

	}

	public class Brain {
		private CommandIndex currentCommandIndex = new CommandIndex(default);
		public int[] Code { get; set; } = new int[Settings.CODE_SIZE];
		public int CurrentCommandId { get => Code[currentCommandIndex]; }

		public void SetGotoCommandIndex(int increaseValue) {
			int current = currentCommandIndex;

			current += increaseValue;

			if(current > Settings.MAX_COMMAND_ID) {
				currentCommandIndex = current - Settings.MAX_COMMAND_ID;
			} else {
				currentCommandIndex = current;
			}
		}

		public int GetNextCommand() {
			var commandId = Code[currentCommandIndex++];
			return commandId;
		}

		public void Init(int[] inidata) {
			Code = inidata.ToArray();
		}
	}
}