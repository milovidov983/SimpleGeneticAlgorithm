using System;
using System.Collections.Generic;
using System.Text;

namespace AIv2 {
	public abstract class WorldObject {
	}
	public class BotObject : WorldObject {
		public override string ToString() {
			return $"B";
		}
	}
	public class Wall : WorldObject {
		public override string ToString() {
			return $"W";
		}

	}
	public class Food : WorldObject {
		public override string ToString() {
			return $"F";
		}
	}
	public class Poison : WorldObject {
		public override string ToString() {
			return $"P";
		}
	}
	public class Empty : WorldObject {
		public override string ToString() {
			return $".";
		}
	}
}
