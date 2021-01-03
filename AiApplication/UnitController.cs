using AiApplication.Models;
using AiLib.Shared;
using AIv2;
using Otter.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AiApplication {
	public class UnitController {

		private readonly Scene scene;
		private List<Entity> all = new List<Entity>();
		private EntityViewFactory entityFactory = EntityViewFactory.Instance;
		public UnitController(Scene scene, MapImplementation map) {
			this.scene = scene;

			map.AddedToMapEvent += AddEntity;
		}

		private void AddEntity(IItem item) {
			switch (item) {
				case Bot b:{
					var entity = entityFactory.Create(b);
					scene.Add(entity);
					all.Add(entity);
					break;
				}
				case Food f: {
					var entity = entityFactory.Create(f);
					scene.Add(entity);
					all.Add(entity);
					break;
				}
				case Poison p: {
					var entity = entityFactory.Create(p);
					scene.Add(entity);
					all.Add(entity);
					break;
				}
				case Wall w: {
					var entity = entityFactory.Create(w);
					scene.Add(entity);
					all.Add(entity);
					break;
				}
				default:
					throw new Exception(nameof(item));
			}
		}

		public void ClearEntities() {
			all.ForEach(x => x.RemoveSelf());
			all.Clear();
		}
	}
}
