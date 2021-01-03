
using AiApplication.Helpers;
using AIv2;
using Otter.Core;
using Otter.Graphics;
using Otter.Graphics.Drawables;
using System;

namespace AiApplication {
	public class BotEntity : Entity, IBotEventObserver {
		private Image image;

		public BotEntity(float x, float y) : base(x, y) {
			Create();
			Graphic.Color = Color.Blue;
		}

		private void Create() {
			// Create a rectangle image.
			image = Image.CreateRectangle(Settings.BOT_SIZE);
			// Add the rectangle graphic to the Entity.
			AddGraphic(image);
			// Center the image's origin.
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
				return;
			}
			Graphic.Color = GetColor(generation);

		}
	}
}


