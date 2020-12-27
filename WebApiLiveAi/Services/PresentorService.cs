using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiLiveAi.Services {
	public class Presentation {
		public Presentation(RawData data) {
			Data = data;
		}

		public RawData Data { get; }
	}
	public class RawData { }


	public delegate void CommonDataUpdate(Presentation presentation);
	public class PresentorService : IPresentorService {

		public event CommonDataUpdate update;

		public void Update(RawData data) {
			update.Invoke(new Presentation(data));
		}
	}


	public interface IPresentorService {
		void Update(RawData data);
	}
}
