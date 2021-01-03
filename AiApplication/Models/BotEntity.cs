
using AiApplication.Helpers;
using AIv2;
using Otter.Core;
using Otter.Graphics;
using Otter.Graphics.Drawables;
using System;

namespace AiApplication {
	public class BotEntity : Entity, IBotEventObserver {
		private Image image;
		private readonly Color DEFAULT_COLOR = Color.Blue;

		public BotEntity(float x, float y) : base(x, y) {
			Create();
			Graphic.Color = DEFAULT_COLOR;
		}

		private void Create() {
			image = Image.CreateRectangle(ViewSettings.BOT_SIZE);
			AddGraphic(image);
			image.CenterOrigin();
		}

		public override void Update() {
			base.Update();
		}

		public void SetDead(Guid id, Position position) {
			RemoveSelf();
		}

		public void SetMove(Guid id, Position from, Position to) {
			var (XsceneCoordinatePx, YsceneCoordinatePx) = CoordsHelper.GetCoordinateFrom(to.X, to.Y);
			X = XsceneCoordinatePx;
			Y = YsceneCoordinatePx;
		}

		public void UpGeneration(int generation) {
			Graphic.Color = GetColor(generation);
		}

		private Color GetColor(int generation) {
			Color red = Color.Red;
			var r = (red.G + generation) % 255;
			red.SetColor(r, red.G, red.B);
			return red;
		}

		public void InitColor(int generation) {
			if(generation < 2) {
				Graphic.Color = DEFAULT_COLOR;
			}
			Graphic.Color = GetColor(generation);

		}
	}
}


