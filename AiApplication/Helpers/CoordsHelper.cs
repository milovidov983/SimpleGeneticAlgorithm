using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AiApplication.Helpers {
	public static class CoordsHelper {

		private static readonly int offset = Settings.BOT_SIZE / 2;
		public static (float XsceneCoordinatePx, float YsceneCoordinatePx) GetCoordinateFrom(int x, int y) {
			return (XsceneCoordinatePx: x * Settings.BOT_SIZE - offset, YsceneCoordinatePx: y * Settings.BOT_SIZE - offset);
		}
	}
}
