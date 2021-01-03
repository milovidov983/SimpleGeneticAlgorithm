using AIv2;
using Otter.Core;
using Otter.Graphics;
using Otter.Graphics.Drawables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AiApplication.Models {
	public class CommonEntity : Entity, IWorldObjectsEventObserver {
		private Image image;
		private readonly int width;
		private readonly int height;

		public CommonEntity(float x, float y, int width = ViewSettings.BOT_SIZE, int height = ViewSettings.BOT_SIZE) : base(x, y) {
			Create();
			Graphic.Color = Color.Grey;
			this.width = width;
			this.height = height;
		}

		private void Create() {
			// Create a rectangle image.
			image = Image.CreateRectangle(width, height);
			// Add the rectangle graphic to the Entity.
			AddGraphic(image);
			// Center the image's origin.
			image.CenterOrigin();
		}

		public override void Update() {
			base.Update();
		}


		internal void InitColor(AIv2.WorldObject item) {
			Graphic.Color = item switch {
				Food _ => Color.Green,
				Poison _ => Color.Black,
				Wall _ => Color.Cyan,
				_ => Color.Grey
			};
			Graphic.Color.ByteA = 64;
		}

		public void SetDead(Guid id, Position position) {
			RemoveSelf();
		}
	}
}
