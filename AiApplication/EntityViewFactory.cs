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

	public class EntityViewFactory {
		public static EntityViewFactory Instance = new EntityViewFactory();

	
		public BotEntity Create(Bot bot) {
			var convertedCoords = CoordsHelper.GetCoordinateFrom(bot.Position.X, bot.Position.Y);
			var entity = new BotEntity(
				convertedCoords.XsceneCoordinatePx,
				convertedCoords.YsceneCoordinatePx);
			entity.InitColor(bot.Generation);
			bot.AddSububscriptions(entity);
			return entity;
		}

		public CommonEntity Create(WorldObject item) {
			var convertedCoords = CoordsHelper.GetCoordinateFrom(item.Position.X, item.Position.Y);
			var entity = new CommonEntity(
				convertedCoords.XsceneCoordinatePx,
				convertedCoords.YsceneCoordinatePx);
			entity.InitColor(item);
			item.AddSubscription(entity);
			return entity;
		}
	}
}
