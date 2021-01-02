using AiApplication.Helpers;
using AiLib.Presentor;
using AIv2;
using Otter.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AiApplication.Models {

	public class BotViewFactory {
		public static BotViewFactory Instance = new BotViewFactory();

		private readonly Random rand = new Random((int)DateTime.UtcNow.Ticks);

	

		public PlayerEntity Create(Bot bot) {
			var convertedCoords = CoordsHelper.GetCoordinateFrom(bot.Position.X, bot.Position.Y);
			var entity = new PlayerEntity(
				convertedCoords.XsceneCoordinatePx,
				convertedCoords.YsceneCoordinatePx, GetRandColor());

			bot.AddSub(entity);
			return entity;
		}


		private Color GetRandColor() {
			float h = rand.Next(0, 360);
			float s = rand.Next(1, 1000) / 1000f;
			float v = rand.Next(1, 1000) / 1000f;
			var color = Color.FromHSV(h, s, v, 1);
			return color;

		}

	}
}
