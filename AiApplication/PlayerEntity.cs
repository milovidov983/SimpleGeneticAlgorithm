
using AiApplication.Helpers;
using AIv2;
using Otter.Core;
using Otter.Graphics;
using Otter.Graphics.Drawables;
using System;

namespace AiApplication {
	public class PlayerEntity : Entity, IBotEventObserver {

		/// <summary>
		/// The current move speed of the Entity.
		/// </summary>
		public float MoveSpeed;
		private Image image;

		/// <summary>
		/// The move speed for when the Entity is moving slowly.
		/// </summary>
		public const float MoveSpeedSlow = 5;
		/// <summary>
		/// The move speed for when the Entity is moving quickly.
		/// </summary>
		public const float MoveSpeedFast = 10;

		public PlayerEntity(float x, float y, Color color) : base(x, y) {
			Create();
			Graphic.Color = color;
			// Assign the initial move speed to be the slow speed.
			MoveSpeed = MoveSpeedSlow;
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


	}
}


