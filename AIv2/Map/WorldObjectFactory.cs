using AIv2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AiLib {


	public class WorldObjectFactory {
		private readonly WorldObject type;

		public WorldObjectFactory(WorldObject type) {
			this.type = type;
		}

		public WorldObject Create() {
			return type switch {
				Food _ => new Food(),
				Poison _ => new Poison(),
				_ => throw new Exception(nameof(type))
			};

		}
	}
}
