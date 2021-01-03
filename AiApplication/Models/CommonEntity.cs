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

		public CommonEntity(float x, float y) : base(x, y) {
			Create();
			Graphic.Color = Color.Grey;
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


		internal void InitColor(AIv2.WorldObject item) {
			Graphic.Color = item switch {
				Food _ => Color.Green,
				Poison _ => Color.Black,
				_ => Color.Grey
			};
			Graphic.Color.ByteA = 64;
		}

		public void SetDead(Guid id, Position position) {
			RemoveSelf();
		}
	}
}
